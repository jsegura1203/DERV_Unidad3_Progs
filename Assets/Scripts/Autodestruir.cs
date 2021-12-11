using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruir : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Soy el objeto: " + this.gameObject.name);
        GameObject gameobj = GameObject.Find(this.gameObject.name);
        Destroy(gameobj);
    }
}
