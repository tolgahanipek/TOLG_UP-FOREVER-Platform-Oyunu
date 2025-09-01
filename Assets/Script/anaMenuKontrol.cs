using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anaMenuKontrol : MonoBehaviour
{

    AudioSource[] b;//Anamenuyle alakalı seslerin oluşturulması için tanımlandı.
    GameObject leveller, kilitler,kilitlerAcik;//gameobje nesnesi oluşturuldu.
    void Start()// bir kez çalışır.
    {
       
        leveller = GameObject.Find("leveller");//leveller adındaki obje bulunup leveller nesnesine atıldı.
        kilitler = GameObject.Find("kilitler");//kilitler adındaki obje bulunup leveller nesnesine atıldı.
        kilitlerAcik = GameObject.Find("kilitler Acik");//yukarıdakiyle aynı işlemi yapar.
        b = GetComponents<AudioSource>();//AudioSource componenti oluşturuldu.b nesnesine atıldı.
        for (int i = 0; i < leveller.transform.childCount; i++)//leveller objesinin çocukları yani leveller objesine ait olan level butonlarını pasif hale getirmek için ilk başta anamenu ekranında pasif hale getirmek için bir döngüye atıldı.
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);//leveller objesinin çocukları yani level butonları ilk başta pasif hale getirildi.
        }
        for (int i = 0; i < kilitler.transform.childCount; i++)//anamenudeki kilit resimlerini pasif hale getirmek içib bir döngüye atıldı.
        {
            kilitler.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < kilitlerAcik.transform.childCount; i++)//anamenudeki kilitacik resimlerini pasif hale getirmek içib bir döngüye atıldı.
        {
            kilitlerAcik.transform.GetChild(i).gameObject.SetActive(false);
        }

      

        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)//şuan kaçıncı leveldeysek o levele kadar olan tüm levellerin butonları aktif yani görünür hale getirir.Bunun için bir döngü oluşturuldu.
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    public void butonSec(int gelenButon)//anamenudeki butonlarının yapacağı işlemleri belirtmek için bir butonsec adında fonksiyon oluşturuldu.
    {
        if (gelenButon==1)//inspector ekranında tanımladığımız butonun id si 1 e eşit ise 
        {
            b[0].Play();//butona tıklama sesi gelmesi için tanımlandı.
            SceneManager.LoadScene(1);//oyunu başlatır.
        }
        else if (gelenButon==2)//inspector ekranında tanımladığımız butonun id si 2 e eşit ise 
        {
            b[1].Play();//tıklama sesi gelmesi için
            for (int i = 0; i < kilitler.transform.childCount; i++)//tüm kilitleri görünür hale getirmek için döngü oluşturuldu.
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < leveller.transform.childCount; i++)//tüm levellerin butonlarını görünür hale getirmek için
            {
                leveller.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)//geçtiğimiz levele kadar olan levellerin kilit resimlerinin görünümlerini pasif hale getirmek için ve kilitacik resimleri aktif hale getirmek için döngü oluşturuldu.
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(false);
                kilitlerAcik.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else if (gelenButon==3)//seçtiğimiz butonun id si 3 e eşit ise
        {
            b[2].Play();//tıklama sesi çal
            PlayerPrefs.DeleteAll();//oyundaki tüm kayıtları sil
            Application.Quit();//uygulamayı kapat.
        }
    }

    public void levellerButon(int gelenLevel)//id si 1 eşit olan tuşu gelecek olan leveli belirlemek için bir fonksiyon oluşturuldu.
    {
        SceneManager.LoadScene(gelenLevel);
    }
    
   
}
