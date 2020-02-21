using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour {

    // Hay que probar este valor. Campo en el que se considera usuario sobre elevador
    public float campoElevador = 1f;    

    public GestorEscenas gestorEscenas;

    private bool banderaTeletransportar;

    // Distancia entre elevador y camara principal
    private Vector3 vectorCamara;       


    void Start()
    {
        banderaTeletransportar = false;

    }

    void Update()
    {
  
    }

    public void Teletransportar()
    {
        banderaTeletransportar = true;

        // Aquí se instancia un efecto que dé sensación de teletransporte y cuando acabe el efecto se llama 
        // a GestorEscenas.CambiaEscena(). La idea es que el efecto instanciado llame a GestorEscenas

        gestorEscenas.CambiaEscena();
    }

    // Devuelve true si se ha salido del elevador
    public bool SalidaElevador()   
    {
        vectorCamara = Camera.main.transform.position - transform.position;

        // Salida elevador
        if (Mathf.Sqrt(vectorCamara.x * vectorCamara.x + vectorCamara.z * vectorCamara.z) > campoElevador) 
        {
            return true;
        }   

        // Dentro de elevador
        return false;
    }

}
