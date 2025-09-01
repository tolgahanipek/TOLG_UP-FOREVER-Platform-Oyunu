using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altinKontrol : MonoBehaviour
{
    public Sprite[] animasyonKareleri;//altın animasyonunu oluşturmak için sprite sınıfından bir nesne oluşturdum.Public yapmamamın sebebi inspector ekranında kendi ellerimle animasyonu oluşturacak spriteları girebilmek.
    SpriteRenderer spriteRenderer;//scene ekranında başlanğıç olarak altın objesine resim vermek için oluşturuldu.
    float zaman = 0;//altın objesinin altının spriteler arasındaki daha doğrusu iki frame arasındaki zamanı tanımlamak için bir zaman değişkeni oluşturdum ve değerine 0 verdim.
    int animasyonKareleriSayaci = 0;//altın animasyonunda spriteler arası geçiş yapmak için oluşturuldu.
    void Start()//sadece oyun başlarken bir kez çalışır.
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//spriteleri oluşturmak için bir component oluşturuldu.
    }

    
    void Update()//her frame de bir kez çalışır.
    {
        zaman += Time.deltaTime;//iki frame arasındaki zaman dilimini zaman değişkenine attım.
        if (zaman > 0.03f)//iki frame arasındaki zaman farkı 0.03 ten büyük ise animasyonları oluşturacak spriteleri ekler ve son sprite a eşit ise başa sarar.
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];
            if (animasyonKareleri.Length == animasyonKareleriSayaci)
            {
                animasyonKareleriSayaci = 0;
            }
            zaman = 0;
        }
    }
}
