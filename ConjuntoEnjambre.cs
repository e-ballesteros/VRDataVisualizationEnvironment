using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuntoEnjambre : MonoBehaviour {

    private Oscilacion m_oscilacion;

	// Use this for initialization
	void Start () {

        m_oscilacion = GetComponent<Oscilacion>();
        m_oscilacion.Flota();

	}
	
	// Update is called once per frame
	void Update () {
		


	}
}
