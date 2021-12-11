using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class Half_duplex_fisicas : MonoBehaviour
{
    SerialPort arduino;

    [SerializeField]
    int valorsensor1;

    [SerializeField]
    int valorsensor2;

    Rigidbody rb;
    public int desplazamiento = 150;
    public void Start()
    {
        valorsensor1 = 0;
        valorsensor2 = 0;
        rb = GetComponent<Rigidbody>();
        conexion();
        leer_datos();
    }
    
    public void conexion()
    {
        if(arduino!=null)
        {
            if(!arduino.IsOpen)
            {
                arduino.Open();
              
            }
            else
            {
                arduino.Close();
            }
        }
        else
        {
            string com = "COM3";
            arduino = new SerialPort(com, 9600);
            arduino.ReadTimeout = 110;
            arduino.Open();
        }
    }

    public void leer_datos()
    {
        StartCoroutine("Leer");
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
                            if (valorsensor2==2)
                            {
                                Debug.Log("Adelante");
                                rb.MovePosition(rb.position + transform.forward * desplazamiento * Time.deltaTime);
                            }
                            if (valorsensor2==0)
                            {
                                Debug.Log("Atras");
                                rb.MovePosition(rb.position + transform.forward * -1 * desplazamiento * Time.deltaTime);
                            }
                            if (valorsensor1==2)
                            {
                                Debug.Log("Izquierda");
                                rb.MovePosition(rb.position + transform.right * -1 * desplazamiento * Time.deltaTime);
                            }
                            if (valorsensor1==0)
                            {
                                Debug.Log("Derecha");
                                rb.MovePosition(rb.position + transform.right * desplazamiento * Time.deltaTime);
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(.2f);
        }
    }
}
