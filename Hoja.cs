using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoja : MonoBehaviour {
    public float velocidad;
    public bool puertaSur;              // Si es puerta de pared Sur debe moverse hacia el lado contrario

    private bool banderaabrir;
    private Rigidbody rb;
    private bool finEscena;             // Indica al GestorEscenas si la escena ha acabado
    private int sentido;
    
    void Start () {
        rb = GetComponent<Rigidbody>();
        finEscena = false;
        if (puertaSur == true) sentido = 1; // Si es puerta Sur se cambia el sentido de apertura
        else sentido = -1;
    }
	
	void FixedUpdate () {
		if ( (banderaabrir == true) && (Mathf.Abs(transform.position.z) < 0.83) ) // Mover hasta que se llegue a un valor de z determinado
        {
            rb.velocity = velocidad * new Vector3(0.0f,0.0f,1) * sentido;
        }
        else
        {
            rb.velocity = velocidad * new Vector3(0.0f, 0.0f, 0.0f);
        }
	}

    public void abrir()
    {
        banderaabrir = true;
    }

    public bool FinEscena()     // Devuelve el valor de finEscena
    {
        return finEscena;
    }
}
