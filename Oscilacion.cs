using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilacion : MonoBehaviour {

    public float magnitudOscilacion = 1f;

    Vector3 floteY;
    private float origenY;
    private bool flote;

    // Almacena valor Time.time cuando se llama a Flota(), para que el flote comience desde cero
    private float tiempoInicio;     

    void Start () {

        flote = false;
        origenY = this.transform.position.y;

    }
	
	void Update () {

        if (flote)
        {
            // Se almacena donde se encuentra
            floteY = transform.position;

            // Se calcula la nueva posición
            floteY.y = origenY + (Mathf.Sin(Time.time - tiempoInicio) * magnitudOscilacion);

            // Se lleva a la nueva posición
            transform.position = floteY;
        } 

    }

    // Indica que tiene que flotar
    public void Flota()     
    {
        origenY = this.transform.position.y;
        flote = true;
        tiempoInicio = Time.time;
    }

    // Indica que no tiene que flotar
    public void Quieto()    
    {
        flote = false;
    }

}
