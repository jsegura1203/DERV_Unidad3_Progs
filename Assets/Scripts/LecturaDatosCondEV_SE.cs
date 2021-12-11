using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class LecturaDatosCondEV_SE : MonoBehaviour
{

    SerialPort arduino;

    [SerializeField]
    TextMeshProUGUI estado;
    [SerializeField]
    TextMeshProUGUI texto_boton_conectar;

    [SerializeField]
    TextMeshProUGUI texto_boton_enviar;
    // Start is called before the first frame update
    public void conexion()
    {
        if(arduino!=null)
        {
            if(!arduino.IsOpen)
            {
                arduino.Open();
                estado.text = "RECONECTADO";
                texto_boton_conectar.text = "DESCONECTAR";
            }
            else
            {
                arduino.Close();
                estado.text = "DESCONECTADO";
                texto_boton_conectar.text = "RECONECTAR";
            }
        }
        else
        {
            string com = "COM3";
            arduino = new SerialPort(com, 9600);
            arduino.ReadTimeout = 110;

            estado.text = "CONECTADO";
            texto_boton_conectar.text = "DESCONECTAR";
            arduino.Open();
        }
        
        //TAREA
        //REALIZAR LAS VALIDACIONES PERTINENTES PARA QUE EL PROGRAMA
        //PUEDA RECUPERARSE Y VOLVER A EJECUTAR CUANDO OCURRE ALGUNA DE ESTAS
        //CONDICIONES Y QUE DE TIEMPO A QUE EL USUARIO LAS CORRIJA MANUALMENTE
        //ACCESO DENEGADO
        //COM NO EXISTENTE
    }

    public void escribir_datos()
    {
        if(arduino!=null)
        {
            if(arduino.IsOpen)
            {
                if(texto_boton_enviar.text.Equals("PRENDER"))
                {
                    arduino.WriteLine("1");
                    texto_boton_enviar.text = "APAGAR";
                }
                else
                {
                    arduino.WriteLine("0");
                    texto_boton_enviar.text = "PRENDER";
                }
            }
        }
    }


    public void leer_datos()
    {
        StartCoroutine("Leer");
    }
    IEnumerator Leer()
    {
        while(true)
        {
            if(arduino!=null)
            {
                if(arduino.IsOpen)
                {
                    string valor = arduino.ReadExisting();
                    Debug.Log("A"+valor + "A");
                }
            }
            yield return new WaitForSeconds(.2f);
        }
    }
}
