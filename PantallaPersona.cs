using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaPersona : MonoBehaviour {

    // Referencia a Oscilacion de PantallaPersona
    private Oscilacion oscilacion;

    // Bandera para evitar llamadas sucesivas a oscilacion.Flota()
    private bool banderaFlota;

    void Start () {

        oscilacion = GetComponent<Oscilacion>();
        banderaFlota = true;

    }
	
	void Update () {

        if (banderaFlota == true)
        {
            oscilacion.Flota();
            banderaFlota = false;
        }

	}
}
