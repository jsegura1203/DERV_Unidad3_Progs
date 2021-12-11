using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class ConexionSEtoEV : MonoBehaviour
{

    SerialPort arduino;

    [SerializeField]
    TextMeshProUGUI estado;
    [SerializeField]
    TextMeshProUGUI texto_boton;

    // Start is called before the first frame update
    public void conexion()
    {
        if(arduino!=null)
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
        }
        else
        {
            string com = "COM3";
            arduino = new SerialPort(com, 9600);
            arduino.ReadTimeout = 1000;

            estado.text = "CONECTADO";
            texto_boton.text = "DESCONECTAR";
            arduino.Open();
        }
    }

    public void escribir_datos()
    {

    }
}
