using System.Collections;
using System.Collections.Generic;
using UnityEngine;//unity le c# ile arasındaki bağlantıyı sağlamak ve unity'e ait komutları aktif hale getirmek için bu kütüphane tanımlandı.
using UnityEngine.UI; //kullanıcı arayüzündeki bileşenleri tanımlamak için (image,text) bu kütüphaneyi tanumladım.
using UnityEngine.SceneManagement; //sahne tasarımıyla alakalı kodların yazılabilmesi ve tanımlı olması için bu kütüphaneyi tanımladım.

/*          ************************** ***************  *            **************      ***************               
 *                      *              *             *  *            *                   *             * 
 *                      *              *             *  *            *                   *             *
 *                      *              *             *  *            *                   *             *
 *                      *              *             *  *            ***************     ***************
 *                      *              *             *  *            *             *     *             *
 *                      *              *             *  *            *             *     *             *
 *                      *              ***************  *********    ***************     *             *
 */
public class karakterkontrol1 : MonoBehaviour // MonoBehaviour unity scriptinin türetildiği temel sınıftır.
{
    public Sprite[] beklemeAnim;//beklemeanim adında bekleme animasyonlarını oluşturacak spriteleri tutan sprite bileşenli bir dizi tanımladım ve ardından animasyonların oluşması için ve dışardan inspector ekranında sprite eklememiz için public olarak tanımladım.Böylece dışardan istediğimiz spriteleri ekleyebileceğim.
    public Sprite[] ziplamaAnim;//beklemeanim dizisinde yapılan işlemlerin birebir aynısı yapan bir sprite bileşenli bir dizi tanımladım.
    public Sprite[] yurumeAnim;//beklemeanim dizisinde yapılan işlemlerin birebir aynısı yapan bir sprite bileşenli bir dizi tanımladım.
    public Text canText; //UI game ekranında Can bilgisini göstermek adına  bir Text adında bir component tanımladım adına canText verdim.
    public Text AltinText;//altın bilgisini vermek için bir text adında component tanımladım ve adına AltinText verdim.
    public Image SiyahArkaPlan; //game ekranında karakterimiz öldüğünde ekranın kararması için bir image adında bir component oluşturdum ve adına SiyahArkaplan koydum.
    public Text level; //oyunumuzda level bilgisi vermek için text adında bir component tanımladım ve adına level verdim.
    public Text gameover; //oyunda karakterimizin canı bittiğinde GAME OVER yazısını görmemiz için text adında bir component oluşturup adına gameover dedim.
    int can = 100; // int tipinde bir can değişkeni tuttum 100 değerini atadım.
   
    SpriteRenderer spriteRendere;//spriteRendere adında SpriteRenderer componenti tanımladım bu bileşenin görevi sahnede sprite'ı oluşturur ve sahnede  görsel olarak nasıl göründüğünü kontrol eder.
    int beklemeAnimSayac;//int tipinde bir beklemeanimSayac adında bir değişken tuttum amacı inspector ekranında sırasıyla animasyonumu oluşturacak spriteleri eklemek.
    int yurumeAnimSayac;//int tiğinde bir yurumeAnimSayac tanımlandı amacı yukarıdaki tanımlanan değişkenin göreviyle eşdeğerdir.
    int altinSayaci = 0; //altınlarımızın sayısını tutacak bir int tipinde altinSayaci adında bir değişken oluşturuldu ve değeri 0 olarak atandı.
    Rigidbody2D fizik;//gameobjectlere yerçekimi duyarlılığı kazandırmak için rigidbody2d adında bir component tanımladım.
    Vector3 vec;//vec adında konum tutacak bir vector3 nesnesi tanımladım.
    Vector3 kameraSonPos;//kameramızın son konumunu tutacak bir vector3 nesnesi tanımladım vector3 3 boyutlu uzayda konumu ve vektörleri ifade eder.
    Vector3 kameraIlkPos;//kameramızın ilk konumunu tutacak bir vector3 nesnesi tanımladım.
    float horizontal = 0;//karakterimize yatay olarak hareket ettirebilmemiz için int tipinde bir horizontal adında bir değişken tanımladım ve değerine sıfır atadım.
    float beklemeAnimZaman = 0;//bekleme animasyonları arasındaki süreyi belirlemek için bir beklemeanimzaman değişkeni oluturuldu. ve 0 olarak atandı.
    float yurumeAnimZaman = 0;//yurume animasyonları arasındaki süreyi belirlemek için bir değişken oluşturuldu.
    float siyahArkaPlanSayaci = 0;//
    bool birkereZipla = true;//karakterimizin zıplayıp zıplamadığını kontrol edecek bir bool tipinde bir birKereZipla adında bir değişken oluşturdum.Değerini true yaptım.
    float anaMenuyeDonZaman = 0;
    GameObject kamera;//oyunumuzdaki kamera objesinin yerini tespit etmek için bir obje oluşturdum.
    AudioSource[] sesler;// oyunumdaki sesleri ve müzikleri oluşturmak için bir audioSource adında dizi biçiminde bir component tanımladım.

   
   
    

    void Start()//oyunumuzda objeler oluşturulmadan önce bir kez çalışır.
    {
        gameover.enabled = false;//gameover yazımız oyun başlarken pasif hale getirildi.
        Time.timeScale = 1;//oyunun akış hızımızı 1 olarak ayarladım.
        spriteRendere = GetComponent<SpriteRenderer>();//spriteRendere adında bir SpriteRenderer componenti oluşturuldu.
        fizik = GetComponent<Rigidbody2D>();//fizik adında bir rigidbody2d componenti oluşturuldu.
        sesler = GetComponents<AudioSource>();//sesler adında bir audioSource componenti oluşturuldu.
        sesler[0].Play();//oyun başlangıcında sesler dizisinden 0 indisine sahip ses çaldı.

       
        
        kamera = GameObject.FindGameObjectWithTag("MainCamera");//MainCamera tagine sahip objemizi bulmamız için tanımlandı ve kamera gameobjectine atandı.
        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("kacincilevel"))//oyunumuz kaçıncı sahnede olduğunu tespit etmek için kullandık
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);//üzerinde bulunduğumuz sahneden devam etmek için bu kodu kullandık
        }
     
        kameraIlkPos = kamera.transform.position - transform.position;//kameramızın ilk konumunu bulmamız için kameramızın bulunduğu konumdan karakterimizn bulunduğu konumu çıkardım.
        canText.text = "CAN: " + can; //oyun ekranında karakterin can miktarını göstermesi için text olarak ifade edildi.
        AltinText.text = "ALTIN:30- " + altinSayaci;//oyun ekranında altın miktarını göstermesi için tanımlandı.

    }
    void Update()//her frame de bir kez çalışır.
    {
        
        if (Input.GetKeyDown(KeyCode.Space))//karakterimizin zıplayıp zıplamadığını tespit etmemiz için bir if şartı tanımlandı ve space tuşuna basıp basmadığımız kontrol edildi.
        {
            if (birkereZipla)//Daha sonra  ilk başta true değeri alan birkerezipla değişkeni zıplamadığımız sürece bir kere çalışır.
            {
                sesler[4].Play();//zıplama sırasında zıplamaya ait olan 4 numaralı indisli ses çalar
                fizik.AddForce(new Vector2(0, 500));//karakterimiz zıplama sırasında y ekseni +500 yönünde bir güç uygular.
                birkereZipla = false;//havada tekrar zıplamasını önlemek için birkereZipla değişkeni false yapıldı ve yukarıdaki if bir kere çalıştı.

            }

        }
    }

    void FixedUpdate()//update methodundan daha sık çalışır fizik olayları ve hesaplamaları bu fonksiyondan hemen sonra çalışır.
    {

        karakterHareket();//karakterhareket fonksiyonumuzu çağırdık.
        Animasyon();//animasyon fonksiyonumuzu çağırdık.
        if (can<=0)//can 0 ve 0 dan küçük ise aşağıdaki komutları çalıştırır.
        {
            
            gameover.enabled = true;//canımız sıfırdan küçük veya eşit olduğunda gameover yazımızı aktif hale getirdik.
            Time.timeScale = 0.3f;//oyun akış hızımızı düşürerek 0.3 olarak ayarladık ve sahnelerimizi yavaşlattık.

            canText.enabled = false;//can gsöstergemizi pasif hale getirdik.
            AltinText.enabled = false;//altın göstergemizi pasif hale getirdik.
            level.enabled = false;//level göstergemizi pasif hale getirdik.
            siyahArkaPlanSayaci += 0.03f;//arka planın kararması için değişkenimizin değerini sürekli 0.03 arttırdık.
            SiyahArkaPlan.color = new Color(0,0,0, siyahArkaPlanSayaci);//arka planımız siyahlaştı.
            anaMenuyeDonZaman += Time.deltaTime;//time.deltatime iki frame arasındaki zamanı ifade eder.
            if (anaMenuyeDonZaman>1)//zamanımız bir saniyeden  büyükse bizi anamenu sahnesine yönlendirir.
            {
                SceneManager.LoadScene("anamenu");// anamenu ye yönlendirildi.
            }
        }
    }
    void LateUpdate()//tüm update methodlar çalıştıktan sonra çalışır ve her sahnede bir kez çalışır.kamerayla alakalı kodlar buraya yazılır
    {
        kameraKontrol();//kamera fonksiyonumuzu çağırdık.
    }
    void karakterHareket()//karakterhareket adında bir fonksiyon oluşturuldu.
    {
        horizontal = Input.GetAxisRaw("Horizontal");// A,D  ve sol ve sağ yön tuşlarına bastığımzda  yatay olarak bize konum verir.
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);//vec nesnesinde yatay ve dikey hızlarımızı belirledik.Ytay olarak hızımızı 10 ile çarptık mesela konum 1  birimde 10 la çarptığımız için 10 birim ötelenmiş olacak.
        fizik.velocity = vec;//vec nesnesi hız değişkeni fizik.velocity e atandı.
    }
    void OnCollisionEnter2D(Collision2D col)//herhangi bir gameobjenin colliderına temas ettiğimizde çalışacak kodlar buraya yazılır.
    {
        birkereZipla = true;//zemine temas ettiğinde zıplama olsyı aktif hale getirilmesi için birkerezipla değişkeni true yapıldı.

    }
    void OnTriggerEnter2D(Collider2D col)//bir obje bizim karakterimize temas ettiğinde çalışacak kodlar buraya yazılır.
    {
        if (col.gameObject.tag=="kursun")/*bizim karakterimize temas eden objenin tagi kursuna eşit ise veya temas ederseaşağıdaki komutlar çalıiır.
            ardından karakterimizin can kaybettiğini belirten 1 numaralı indise sahip ses çalar
            canımız 1 azalır
             karakterimizin canının güncel hali scene de gösterilir.*/
        {
         

            sesler[1].Play();
            can--;
            canText.text = "CAN: " + can;
        }
        if (col.gameObject.tag == "dusman")

            /*    karakterimize temas eden objenin tagi dusmana eşit ise veya temas ederse
             karakterimizin can kaybettiğini belirten 1 numaralı indise sahip ses çalar.
         canımız 10 azalır ve scene ekranında güncellenmiş hali gösterilir.
         
         */
        {
          
            sesler[1].Play();
            can -=10;

            if (can <= 0)  //canımız 0 dan küçük ve eşit ise aşağıdaki komutlar çalışır.
            {
                sesler[0].Stop();//oyunumuzun ana müziği durdurulur
                sesler[1].Play();//karakterimizin öldüğüne işaret olan ses çalar.
                can = 0;//canımız 0 yapılır.
              
                canText.text = "CAN: " + can;//ekranda canımızın güncellenmiş hali gösterilir.
            }
            else//yoksa
            {
                canText.text = "CAN: " + can;//ekranda canımız  gösterilir.
            }
        }
        if (col.gameObject.tag == "dusman1")//karakterimize temas eden objenin tagi dusman1'e eşit ise veya  temas ederse
        {

            sesler[1].Play();//karaktermizin can kaybettiğine işaret olan ses çalar.
            can -= 10;//canımız 10 eksilir.

            if (can <= 0)//yukarıdaki işlemin ayınısı olur
            {
                sesler[0].Stop();
                sesler[1].Play();
                can = 0;

                canText.text = "CAN: " + can;
            }
            else
            {
                canText.text = "CAN: " + can;
            }
        }

        if (col.gameObject.tag == "testere")//yukarıdaki işlemlerin birebir aynısı gerçekleşir.
        {
         
            


            sesler[1].Play();
            can -= 10;
            if (can<=0)
            {
                sesler[0].Stop();
                sesler[1].Play();
                can = 0;
            
                canText.text = "CAN: " + can;
            }
            else
            {
                canText.text = "CAN: " + can;
            }
         
        }
        
        if (col.gameObject.tag == "levelbitsin")//karakterimiz bölüm sonuna geldiğinde ilgili geçidin tagine eşit ise veya temas ederse
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);//bir sonraki bölüme yönlendirir
        }
        else if (col.gameObject.tag=="levelbitsin1")//karaktermizin son bölümde ilgili geçidin tagine eşit ise veya temas ederse
        {
            SceneManager.LoadScene(4);//bizi oyun sonu sahnesine yönlendirir.

        }
        if (col.gameObject.tag == "canver")//karakterimiz sandığa temas ettiğinde
        {
            sesler[3].Play();//sandığın açılış müziği çalar.
            if(can>=90)//canımız 90  dan büyük veya eşit ise amacı 90 dan sonra 10 can verdiğinde can değerimizin 100 büyük olmaması için bu şart tanımlandı.
            {
                can = 100;//canımız 100 yapıldı.
                canText.text = "CAN: " + can;//canımız ekranda güncellendi.
            }
            else//yoksa
            {
                can += 10;//canımız 10 arttırdı
                canText.text = "CAN: " + can;//canımız ekranda güncellendi.
            }
            col.GetComponent<BoxCollider2D>().enabled = false;//sandığımızın sahneden yok edilmesi için sandığın collider i pasif hale getirildi.
            col.GetComponent<canver>().enabled = true;//canver tagine sahip component aktif hale getirildi.
            Destroy(col.gameObject,3);//sandğımız ekrandan 3 saniye sonra yok edildi.
           
        }
        if (col.gameObject.tag == "altin")//karakterimiz altın'a temas ettiyse
        {


            sesler[2].Play();//altın sesini çal
            altinSayaci++;//altın göstergemizi 1 arttır.
            if (altinSayaci==30)//altınımız 30 a eşit ise
            {
                sesler[5].Play();//cam kazanma sesini çal
                if (can>=50)//canımız 50 ve 50 den büyükse can aşımına uğramamamız için if i tanımladık.
                {
                    can = 100;//canım 100 oldu.
                    canText.text = "CAN: " + can;//can göstergemizi güncelledik.
                }
                else//yoksa
                {
                    can += 50;//canımızı 50 arttırdık.
                    canText.text = "CAN: " + can;//canımızı güncelledik.
                }
                
                AltinText.text = "ALTIN:30- " + altinSayaci;//altın göstergemizi güncelledik.
                Destroy(col.gameObject);//altın objemize temas ettikten sonra yok ettik.
            }
            else//yoksa
            {
                AltinText.text = "ALTIN:30- " + altinSayaci;//altın sayacımızı güncelledik.
                Destroy(col.gameObject);//temas eden altını yok ettik.
            }
          
           

        }
        if (col.gameObject.tag == "su"||col.gameObject.tag=="dusme")//karakterimiz suya veya boşluğa düşerse
        {
            can = 0;//can 0 oldu
            sesler[0].Stop();//ana oyun müziğimiz durduruldu.
            sesler[1].Play();//karakterin ölüm sesi çaldı.




            AltinText.enabled = false;//altın göstergesi kapatıldı
            level.enabled = false;//level göstergesi kapatıldı.

      
         
        }
      

    }
    void kameraKontrol()//kamerayla alakalı kodlar bu fonksiyonda tanımlandı.kameranın bizi takip etmesi için oluşturuldu.
    {
        kameraSonPos = kameraIlkPos + transform.position;//kameranın ilk konumuna bizim konumumuz eklenerek kameranın bizim karakterimizin takip edilmesi sağlandı.ve kameranın son konumu bulundu.
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.08f);// lerp adında bir özel fonksiyon tanımlayarak kameramızın karakterimizi daha yumuşsk hareketlerle takip edilmesi sağlandı. ve kameranın konumuna atandı.

    }

    void Animasyon()//karakterimizin animasyon ile alakalı kodlar bu fonksiyona yazıldı.
    {
        if (birkereZipla)//karakterimizin zıplamıyorsa
        {
            if (horizontal == 0)//karakterimiz hareket etmiyorsa
            {
                beklemeAnimZaman += Time.deltaTime;//iki frame arasındaki süre bekleAnimZaman a atandı spriteler arasındaki süredir
                if (beklemeAnimZaman > 0.05f)//iki frame arasındaki süre 0.05 den büyükse
                {
                    spriteRendere.sprite = beklemeAnim[beklemeAnimSayac++];//bekleme ile alakalı spriteleri karakterimizde göster
                    if (beklemeAnimSayac == beklemeAnim.Length)//karakterimiz son sprite animasyonuna eşit ise yani uzunluğuna
                    {
                        beklemeAnimSayac = 0;//beklemeanimsayacı 0 ata amacı yukarıdaki if i tekrar çalıştırıp bekleme animasyonunu başa sarmak
                    }
                    beklemeAnimZaman = 0;//değişken 0 olarak atandı animasyonları verebilmek için
                }
            }
            else if (horizontal > 0)//karakterimiz hareket ediyorsa
            {
                yurumeAnimZaman += Time.deltaTime;//iki frame arasındaki süreyi yurumeanimzamana ekle
                if (yurumeAnimZaman > 0.03f)//iki frame arasındaki süre 0.03 den büyükse 
                {
                    spriteRendere.sprite = yurumeAnim[yurumeAnimSayac++];//yürüme animasyonları oynar veya karakterimize akset
                    if (yurumeAnimSayac == yurumeAnim.Length)//karakterimiz son yürüme sprite ına sahipse
                    {
                        yurumeAnimSayac = 0; //animasyonu başa sarmak için yurumranimzaman 0 olarak atandı.
                    }
                    yurumeAnimZaman = 0;// iki frame arasındaki süreyi ekletmek için 0 olarak atandı yoksa hızlı bir şekilde spritelar arası geçiş yapar ve animasyonları farkedemeyiz.

                }
                transform.localScale = new Vector3(1, 1, 1);//+x yönünde ilerlemesini sağlar
            }
            else if (horizontal < 0)//karakterimiz geriye doğru hareket ederse
            {

                yurumeAnimZaman += Time.deltaTime;//iki frame arasındaki süre tanımlandı.
                if (yurumeAnimZaman > 0.03f)//iki frame arasındaki süre 0.03 ten büyükse
                {
                    spriteRendere.sprite = yurumeAnim[yurumeAnimSayac++];//yürüme animasyonlarını göster
                    if (yurumeAnimSayac == yurumeAnim.Length)//karakterimiz son yürüme spriteina sahip ise
                    {
                        yurumeAnimSayac = 0;//animasyonu başa sar
                    }
                    yurumeAnimZaman = 0;//animasyonumuz aralıksız değişmemesi için 0 olarak atadık.

                }
                transform.localScale = new Vector3(-1, 1, 1);//karakterimizi -x yönüne döndürdük.
            }
        }

        else//karakterimiz zıplıyorsa
        {
            if (fizik.velocity.y > 0)//y eksenine doğru hızı 0 dan büyük ise
            {
                spriteRendere.sprite = ziplamaAnim[0];//0 imdisli ziplama sprite ı gösterildi.
            }
            else//y eksenine doğru hızı 0 ve sıfırdan küçük ise 
            {
                spriteRendere.sprite = ziplamaAnim[1];//1 indisli ziplama sprite ı gösterildi.
            }
            if (horizontal > 0)//zıplama anında karakterimiz +x yönünde hareket ediyorsa
            {
                transform.localScale = new Vector3(1, 1, 1);//karakterimizin yönünü +x e çevir
            }
            else if (horizontal < 0)//zıplama anında -x e hareket ediyorsa
            {
                transform.localScale = new Vector3(-1, 1, 1);//karakterimizin yönünü -x e çevir
            }
        }
    }
}
