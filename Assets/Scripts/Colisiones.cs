using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    public TextMeshProUGUI Txt_puntaje;
    public int puntaje;

    void Start()
    {
        puntaje = 0;
        //StartCoroutine("Subrutina1");
    }
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("En Colision con :" + name + "   etiqueta: " + tag);
        if (tag.Equals("Puntos"))
        {
            puntaje+=10;
            Txt_puntaje.text = puntaje.ToString();
            Txt_puntaje.color = Color.yellow;
        }
    }
}
