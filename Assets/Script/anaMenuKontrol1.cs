using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anaMenuKontrol1 : MonoBehaviour
{

    public void butonSec1(int gelenButon)//oyun bitiminde olacak işlemleri belirtmek için butonsec1 adında bir fonksiyon oluşturuldu.
    {
        if (gelenButon==0)//butonun id si 0 a eşit ise bizi anamenuye yönlendirir.
        {
            SceneManager.LoadScene(0);
        }

    }



}
