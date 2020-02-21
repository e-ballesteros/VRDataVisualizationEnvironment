using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaCamara : MonoBehaviour {  // Clase para mover la camara VR de forma independiente al movimiento usuario

    public Elevador elevador;

    private bool banderaSubir = false;

    void Start () {
	}
	
	void LateUpdate () {
		
        if (banderaSubir == true)
        {
            transform.position = elevador.transform.position;   // Hay que probar si se producen saltos o si funciona correctamente
        }

	}

    public void Subir()    // Se llama cuando se pulsa una esfera y empieza a subir el elevador
    {
        banderaSubir = true;
    }

}
