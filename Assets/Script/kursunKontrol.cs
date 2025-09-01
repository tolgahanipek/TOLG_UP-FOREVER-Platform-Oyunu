using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol : MonoBehaviour
{
    dusmanKontrol dusman;//dusmankontrol sınıfından bir nesne oluşturuldu.
    Rigidbody2D fizik;//yerçekimi hassasiyeti vermek için rigidbody2d adında bir component tanımlandı.
    void Start()//bir kez çalışır.
    {
        dusman = GameObject.FindGameObjectWithTag("dusman").GetComponent<dusmanKontrol>();//dusman tagine sahip olan objeden oluşacak dusmankontrol componenti bulunup dusman nesnesine atandı.
        fizik = GetComponent<Rigidbody2D>();//yer çekimi hassasiyeti oluşturuldu.
        fizik.AddForce(dusman.getYon()*1000);//getyon adında özel bir fonksiyon oluşturulup 1000 ile çarpılarak bir itme kuvveti oluşturuldu
    }

    
  
}
