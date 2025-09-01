using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;//editor kodları oluşturuldu.
#endif

public class dusmanKontrol : MonoBehaviour
{
    public int resim;//
    GameObject[] gidilecekNoktalar;//düşmanın gideceği noktaları belirtmek için obje tanımlandı.
    bool aradakiMesafeyiBirKereAl = true;//
    Vector3 aradakiMesafe;//
    int aradakiMesafeSayaci = 0;
    bool ilerimiGerimi = true;//dusmanımızın noktalara ilerleyip ilerlemeyeceğini kontrol edecek bir  bool değişkeni oluşturuldu.
    GameObject karakter;//karakterin adında bir obje oluşturuldu.
    RaycastHit2D ray;//dusmanımızın bizi tanıyıp ateş etmesi için  karakkterimizin colliderine temas edecek bir ışın oluşturuldu.
   public LayerMask layermask;//dusmanımızın bizim karakterimizin collideri dışında hiçbir objenin collideri ile temas etmemesi için katman maskesi oluşturuldu.belirli fizik fonksiyonlarını dahil etmek için de kullanılır.
    int hiz = 5;

    public Sprite onTaraf;//dusmanımızn bizi görmesi durumunda oluşacak sprite ı oluşturmak için ve inspector ekranında kendi elimizle sprite ı girmemiz için  sprite sınıfından bir nesne oluşturuldu.
    public Sprite arkaTaraf;//dusmanımızın bizi görmemesi durumunda oluşacak sprite oluşturmak için ve inspector ekranında kendi elimizle sprite i girmemiz için sprite sınıfından bir nesne oluşturuldu.
    SpriteRenderer spriteRenderer;//dusmanın baslangıç sprite ını oluşrurmak için bir componemt oluşturulddu.
    public GameObject kursun;//kursun adında bir obje oluşturuldu.
    float atesZamani = 0;//kursunlar arasındaki zamanı ölçmek için tanımlandı.
    AudioSource b;//sesleri oluşturmak için tanımlandı.

    void Start()// bir kez çalışır.
    {
        gidilecekNoktalar = new GameObject[transform.childCount];//dusnanın gideceği noktalar belirlendi.
        karakter = GameObject.FindGameObjectWithTag("Player");//bizim karakterimizi bulması için tanımlandı.
        spriteRenderer = GetComponent<SpriteRenderer>();//sprite componenti oluşturuldu.
        b = GetComponent<AudioSource>();//ses componenti oluşturuldu.
        for (int i = 0; i < gidilecekNoktalar.Length; i++)//gidilecek noktaların belirlenmesi için bir döngü oluşturuldu.
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()//sürekli çalışır
    {
        beniGordumu();//beni gördumü fonksiyonunu çağır
        if (ray.collider.tag=="Player")//dusmanın bizi tanıması açısından oluşturulan ray yani ışın bizim karakterin colliderine temas ettiyse
        {
            hiz = 8;//dusmanın hızını arttır
            spriteRenderer.sprite = onTaraf;//dusmanın yüzünü bize doğru çevir.
           
            atesEt();//ateset fonksiyonunu çağır bize ateş etmesi için
        }
        else//bizi görmediyse
        {
            hiz = 4;//hızını yavaşlat
            spriteRenderer.sprite = arkaTaraf;//dusman arkasına dönsün.
        }


        noktalaraGit();//dusmanın noktalara gidecek fonksiyonu çağır.

    }
    void atesEt()//ateset fonksiyonu oluşturuldu.
    {
       
        atesZamani += Time.deltaTime;//iki frame arasındaki zaman ateszamanina eklendi.
        if (atesZamani>Random.Range(0.9f,1))//dusmanımızın bize rastgele ateş etmesi için bir şart oluşturuldu.
        {
            Instantiate(kursun,transform.position,Quaternion.identity);//objelerin oluşturulduğu hiyerarşi sekmesinde kursun objelerini oluştur dusmanın bizi görmesi durumunda
            atesZamani = 0;//iki frame arasındaki süreyi sabit kılmak için ateszamani 0 yapıldı.
            b.Play();//ateş etme sesi çal.
        }

    }
    void beniGordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;//karakterimizle dusman arasındaki mesafeyi bulmak için oluşturuldu.
        ray = Physics2D.Raycast(transform.position, rayYonum,1000,layermask);//dusmannın bizi tanıması için bir ışın oluşturuldu .ışının uzunluğu karakterle düsman arasındaki mesafe kadar oldu.bizim dışımızda başka colliderle temas etmemek için layermask belirtildi.
        Debug.DrawLine(transform.position,ray.point,Color.magenta);// dusmanın karakteri tanıması için scene ekranında bir çizgi(ışın) çizildi ve rengi magenta oldu.
    }
    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)//noktalar arasındaki mesafeyi bir kere almak için şart oluşturuldu.
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;//gidilecek nokta ile dusman arasında mesafe alınıp vectorun uzunluğu normalized yani 1 yapıldı.
            aradakiMesafeyiBirKereAl = false;//aradaki mesafeyi bir daha almaması için
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);//aradaki mesafe değişkeninde yapılan değişikliklerin birebir aynısı yapıldı.
        transform.position += aradakiMesafe * Time.deltaTime * hiz;//dusmanımızın noktalar arasındaki hızını belirtmek için kullanıldı.
        if (mesafe < 0.5f)//mesafe 0.5 ten küçük ise
        {
            aradakiMesafeyiBirKereAl = true;//aradaki mesafeyi bir kere al
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)//dusman son noktaya gelmişse
            {
                ilerimiGerimi = false;// ilerleme 
            }
            else if (aradakiMesafeSayaci == 0)//dusman ilk noktadaysa ilerle 
            {
                ilerimiGerimi = true;
            }
            if (ilerimiGerimi)//true ise ilerle 
            {
                aradakiMesafeSayaci++;
            }
            else//değilse geri git.
            {
                aradakiMesafeSayaci--;
            }

        }

    }
    public Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;//karakterimizle dusmanımız arasındaki konumu yani karakterimizin yönünü dusmanın tanıması için tanımlandı.

    }
#if UNITY_EDITOR//editor kodları yazıldı ön planda gözükmez.
    void OnDrawGizmos()//noktaların oluşması için tanımlandı.
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);//noktalar ve arasındaki çizgiler oluşturuldu.
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);

        }
    }
#endif
}


#if UNITY_EDITOR
[CustomEditor(typeof(dusmanKontrol))]
[System.Serializable]
class dusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        dusmanKontrol script = (dusmanKontrol)target;
        //objeleri üretmemiz için bir buton oluşturuldu.
        EditorGUILayout.Space();
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));//dusmanda  kullanılacak objeler belirtildi.
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }


}
#endif