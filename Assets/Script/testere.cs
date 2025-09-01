using System.Collections;
using System.Collections.Generic;
using UnityEngine;//unity komutlarını tanıması için bu kütüphaneyi tanımladık.
#if UNITY_EDITOR// burada bir editör kod sekmesi oluşturduk böylece hiçbir şekilde bu bölüme yazacağımız kodlarla oyun ekranına müdahale etmeyeceğiz sadece scene ekranına müdahale edeceğiz.
using UnityEditor;//unity editor e ait olan komutların tanımlı olması için bu kütüphaneyi çağırdık.
#endif//editor kodunu sona erdirdik.

public class testere : MonoBehaviour
{
    public int resim;
    GameObject [] gidilecekNoktalar;//testeremizin gideceği noktaları scene ekranında tanımlamak için bir gameobject nesnesi  oluşturuldu ve bir diziye atandı.
    bool aradakiMesafeyiBirKereAl = true;//testerenin iki nokta arasındaki mesafeyi 1 kez almak için değişkenin değeri TRUE olarak atandı.
    Vector3 aradakiMesafe;//gidilecek nokta ile testerenin konumu arasındaki farkı tanımlamak için bir int tipinde değişken oluşturuldu.
    int aradakiMesafeSayaci=0;//gidilecek noktayı dizi içinde belirtmek için tanımladık.
    bool ilerimiGerimi = true;//testeremizin ileri veya geri gideceğini kontrol etmek için tanımladık.
    
    void Start()// bir kez çalışır
    {
        gidilecekNoktalar = new GameObject[transform.childCount];//gidileceknoktalar nesnesinin altındaki alt objeler belirlendi.
        for (int i = 0; i < gidilecekNoktalar.Length; i++)//burada  0 dan gidilecek nokta sayısının uzunluğuna kadar döndürüldü
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;// oluşacak 0 indisli testerenin alt çocuğu gidileceknoktalar dizisine atandı.
            gidilecekNoktalar[i].transform.SetParent(transform.parent);//ve testere objesine alt katmanına eklendi.
        }
    }

   
    void FixedUpdate()//sürekli çalışır
    {
        transform.Rotate(0,0,5);//testerenin +z 5 birim döndürüldü
        noktalaraGit(); //testeremiz noktalara doğru ilerledi.
        
    }
    void noktalaraGit()//testerenin noktalara gitmesini sağlayacak komutlar buraya yazılır.
    {
        if (aradakiMesafeyiBirKereAl)//noktalar arasındaki mesafeyi bir kere alır
        {
            aradakiMesafe=(gidilecekNoktalar[aradakiMesafeSayaci].transform.position-transform.position).normalized;//gidilecek noktanın konumundan testerenin konumu çıkarılır ve normalized yani vektörün uzunluğunu 1 yapar.Böylece aradaki mesafe bulunmuş olur.
           aradakiMesafeyiBirKereAl = false;//aradaki mesafeyi tekrar almaması için değişkenin değeri false yapılır.
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);//ikinci bir yöntem olarak testerenin konumuyla gidilecek nokta arasındaki mesafeyi bulan başka bir fonksiyon
        transform.position += aradakiMesafe * Time.deltaTime * 10;//testerenin katettiği yol testerenin konumuna eklenir.
        if (mesafe<0.5f)//testere ve nokta arasındaki mesafe 0.5 ten küçük ise 
        {
            aradakiMesafeyiBirKereAl = true;//aradaki mesafeyi bir kere daha al
            if (aradakiMesafeSayaci==gidilecekNoktalar.Length-1)//testere son noktaya ulaşmışsa
            {
                ilerimiGerimi = false;//testere hareket etmez
            }
            else if (aradakiMesafeSayaci==0)//testere ilk noktadaysa
            {
                ilerimiGerimi = true;//ileriye git
            }
            if (ilerimiGerimi)//true ise ileri gider
            {
                aradakiMesafeSayaci++;//testere baştan sona doğru noktalara gider
            }
            else//değilse
            {
                aradakiMesafeSayaci--;//geriye doğru gider.
            }
       
        }
      
    }
#if UNITY_EDITOR//testerenin gideceği noktaların dizaynı burada yapılır ve hiçbir şekilde oyun ekranına müdahale etmez.
    void OnDrawGizmos()//testerenin gideceği herbir nokta gizmos dur ve bu özel fonksiyonda tanımlanır.
    {
        for (int i = 0; i < transform.childCount; i++)//burda testerenin alt çocuklarına ulaşıldı ve bir döngü oluşturuldu.0 indisten başladı.
        {
            Gizmos.color = Color.red;//testerinin gideceği noktaların rengi kırmızı yapıldı.
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);//testere objesinin i inci alt çocuğunun konumu ve genişliği 1 olarak ayarlanarak oluşturuldu.
        }
        for (int i = 0; i < transform.childCount-1; i++)//gidilecek noktalar arasındaki mesafe çizgileri noktaların toplamından 1 eksik olacak şekilde tanımlandı ve bir döngü oluşturuldu.
        {
            Gizmos.color = Color.blue;//noktalar arasındaki çizgilerin rengi mavi olarak belirlendi.
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);//testere objesinin i inci çocuğuyla i+1 inci çocuğunun konumları arasında çizgi oluşturuldu.

        }
    }
#endif//editor kodları sona erdi.
}


#if UNITY_EDITOR//editoe kodları tanımlandı
[CustomEditor(typeof(testere))]//testere objesine içine testerinin gideceği yeni noktalar oluşturmak için tanımlandı.
[System.Serializable]
class testereEditor :Editor//Editor sınıfından gerekli komutlar türetildi.
{
    public override void OnInspectorGUI()//herhangi bir sınıftan belli bir yöntemi çalıştırmak için override kullanıldı.
    {
        testere script = (testere)target;//testere adında bir script oluşturuldu.
        if (GUILayout.Button("ÜRET",GUILayout.MinWidth(100),GUILayout.Width(100)))//testere objesinin inspector tablosu altında ÜRET adında bir buton oluşturuldu genişliği 100 olarak ayarlandı.ve oluşturulduysa değer TRUE olarak döndürüldü.
        {
            GameObject yeniObjem = new GameObject();//yeni bir obje oluşturuldu.
            yeniObjem.transform.parent = script.transform;//yeni oluşturulan objeler testere objesinin alt objesi oldu
            yeniObjem.transform.position = script.transform.position;//yeni oluşturulan noktanın konumu testerenin konumuna eşitlendi.
            yeniObjem.name = script.transform.childCount.ToString();//yeniobjenin adı mevcut olan noktaların sayısına eşit oldu.
        }
    }


}
#endif//editör kodları işimiz bitti.