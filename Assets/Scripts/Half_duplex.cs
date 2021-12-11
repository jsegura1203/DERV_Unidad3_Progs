using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class Half_duplex : MonoBehaviour
{

    SerialPort arduino;

    [SerializeField]
    TextMeshProUGUI estado;
    [SerializeField]
    TextMeshProUGUI texto_boton_conectar;

    [SerializeField]
    TextMeshProUGUI texto_boton_enviar;

    [SerializeField]
    int valorsensor1;

    [SerializeField]
    int valorsensor2;

    [SerializeField]
    int valorsensor3;


    public void Start()
    {
        valorsensor1 = 15;
        valorsensor2 = 200;
        valorsensor3 = 1023;
    }
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
        StartCoroutine("GestionActuadores");
    }
    string valor = "";

    [SerializeField]
    
    IEnumerator Leer()
    {
        while(true)
        {
            if(arduino!=null)
            {
                if(arduino.IsOpen)
                {
                    valor += arduino.ReadExisting();
                    if (valor.Length>0 && valor[0] == 'E')
                    {
                        Debug.Log("Empieza correctamente");
                        if (valor.IndexOf('T') != -1) {
                            Debug.Log("Termina correctamente");
                            string temp = valor.Substring(1, valor.IndexOf('T')-1);
                            valor = valor.Substring(valor.IndexOf('T') + 1);
                            Debug.Log(temp);
                            //PROCESAR TEMP
                            string[] valores = temp.Split('S');
                            valorsensor1 = int.Parse(valores[0]);
                            valorsensor2 = int.Parse(valores[1]);
                            //valorsensor3 = int.Parse(valores[2]);

                        }
                    }
                }
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    /*IEnumerator GestionActuadores()
    {
        while(true)
        {
            if(arduino!=null)
            {
                if (arduino.IsOpen)
                {
                    int valToActuador1 = valorsensor1 / 4;
                    int valToActuador2 = valorsensor2 / 4;
                    int valToActuador3 = valorsensor3 / 4;
                    string trama = "E" + valToActuador1.ToString() +
                                       "S" + valToActuador2.ToString() +
                                       "S" + valToActuador3.ToString() +"T";
                    arduino.WriteLine(trama);
                    
                    Debug.Log("Info enviada" + trama);

                }
            }
            yield return new WaitForSeconds(0.15f);
        }
    }*/
}
