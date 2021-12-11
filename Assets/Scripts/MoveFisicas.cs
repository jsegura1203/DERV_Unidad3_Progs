using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFisicas : MonoBehaviour
{
    public int desplazamiento = 1;
    public int lateral = 1;

    [SerializeField]
    private int speedRotacion = 10;
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Adelante");
            rb.MovePosition(rb.position + transform.forward * desplazamiento * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Atras");
            rb.MovePosition(rb.position + transform.forward * -1 * desplazamiento * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Izquierda");
            rb.MovePosition(rb.position + transform.right * -1 * lateral * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Derecha");
            rb.MovePosition(rb.position + transform.right * lateral * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Rota izquierda");
            Quaternion q = Quaternion.Euler(new Vector3(0, -1, 0) * speedRotacion * Time.deltaTime);
            rb.MoveRotation(rb.rotation * q);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Rota derecha");
            Quaternion q = Quaternion.Euler(new Vector3(0, 1, 0) * speedRotacion * Time.deltaTime);
            rb.MoveRotation(rb.rotation * q);
        }
    }
}
