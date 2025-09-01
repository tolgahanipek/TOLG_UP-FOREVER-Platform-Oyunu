using System.Collections;
using System.Collections.Generic;
using UnityEngine;//unity komutlarını tanıyabilmesi için tanımladım.

public class suAnimasyonu : MonoBehaviour
{
    public Sprite[] animasyonKareleri;//inspector ekranında su ile ilgili animasyon karelerini elle girmemiz için public olarak tanımladık ve diziye attık Böylece istediğimiz kadar ekleyebileceğim.
    SpriteRenderer spriteRenderer;//sprite oluşturmak için yazdım.
    float zaman = 0;//animasyon kareleri arasındaki zamanı belirlemek için float tipinde bir değişken tanımladım ve değerini 0 olarak atadım.
    int animasyonKareleriSayaci = 0;//animasyon karelerini bir diziye atmak ve su animasyonunu oluşturmak için değişken oluşturdum.
    void Start()// bir kez çalışır.
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//sprite oluşturmak için bir component oluşturuldu.
    }


    void Update()//her frame de bir kez çalışır
    {
        zaman += Time.deltaTime;//iki frame arasındaki zamanı belirlemek için tanımladık zaman değişkenine attık.
        if (zaman > 0.09f)//zaman 0.09 dan büyük ise
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];//animasyon karesini oluştur.
            if (animasyonKareleri.Length == animasyonKareleriSayaci)//su animasyonu sonuncu su sprite ına eşit ise
            {
                animasyonKareleriSayaci = 0;//animasyonu başa sar
            }
            zaman = 0;//iki frame arasındaki süreyi bir kez daha almak için zaman değişkeni 0 yapıldı böylece spriteler arası çakışmayı engellemiş olduk.
        }
    }
}
