using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class ConexionSEtoEV_Tarea : MonoBehaviour
{

    SerialPort arduino;

    [SerializeField]
    TextMeshProUGUI estado;
    [SerializeField]
    TextMeshProUGUI texto_boton;
    [SerializeField]
    TextMeshProUGUI texto_aviso;

    [SerializeField]
    TMP_InputField nuevo_com;

    string com = "COM4";

    // Start is called before the first frame update


    public void Start()
    {
        nuevo_com.enabled = false;
        
    }
    public void conexion()
    {
        
        if (arduino!=null)
        {
            if(!arduino.IsOpen)
            {
                arduino.Open();
                estado.text = "RECONECTADO";
                texto_boton.text = "DESCONECTAR";
            }
            else
            {
                arduino.Close();
                estado.text = "DESCONECTADO";
                texto_boton.text = "RECONECTAR";
            }
            nuevo_com.enabled = false;
        }
        else
        {
            if(nuevo_com.enabled==true)
            {
                nuevo_com.enabled = true;
                com = nuevo_com.text;
                Debug.Log(nuevo_com.text);
            }
            arduino = new SerialPort(com, 9600);
            arduino.ReadTimeout = 1000;
            try
            {
                arduino.Open();
                estado.text = "CONECTADO";
                texto_boton.text = "DESCONECTAR";
                texto_aviso.text = "Por el momento no hay errores";
            }
            catch
            {
                texto_aviso.text = "No se puede conectar al puerto, cambielo en la parte de abajo";
                nuevo_com.enabled = true;
                arduino = null;
                
            }   
        }
    }

    public void escribir_datos()
    {

    }
}
