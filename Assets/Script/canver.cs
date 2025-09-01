using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canver : MonoBehaviour
{
    public Sprite[] animasyonKareleri;//sandığımıza ilişkin animasyonların oluşması için bir nesne oluşturuldu.
    SpriteRenderer spriteRenderer;//sandık spritenin ilk görünümünü vermek için tanımlandı.
    float zaman = 0;//sandığın animasyonlarını oluşturacak spritelerin arasındaki zamannı belirlemek için bir değişken tanımlandı.
    int animasyonKareleriSayaci = 0;//sandık animasyonunu oluşturmak için bir sayac oluşturuldu.
    void Start()// bir kere çalışır.
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//sprite componenti oluşturuldu.
    }

    
    void Update()//her frame de bir kez çalışır.
    {
        zaman += Time.deltaTime;//iki frame arasındaki zamanını bulmak için kullanıldı.
        if (zaman>0.1f)//iki frame arasındaki zaman farkı 0.1 den büyük ise
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];//animasyonu oluşturacak spriteleri sayac bazında oluştur.
            if (animasyonKareleri.Length==animasyonKareleriSayaci)//sandık son animasyon spriteine eşit ise son sprite pozisyonunu koru.
            {
                animasyonKareleriSayaci = animasyonKareleri.Length - 1;
            }
            zaman = 0;//iki frame arasındaki zamanı tekrar ekletmek için zaman değişkeninin değeri 0 yapıldı.
        }
    }
}
