using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoPulsable : MonoBehaviour {

    
    private bool on;

	void Start () {
        on = false;
	}
	
	void Update () {
		
	} 

    public void CambiaEstado()      // Cambia el estado del botón
    {
        on = on ^ true;             // XOR en C#
    }

    public bool EstaPulsado()       // Devuelve el estado del botón: true - pulsado, false - no pulsado
    {
        return on;
    }

}
