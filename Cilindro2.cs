using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cilindro2 : MonoBehaviour {

    private ObjetoPulsable objetoPulsable;

    private bool bandera;

    private Renderer miRenderer;

    private Color miColorInicial;

	// Use this for initialization
	void Start () {

        // El objetoPulsable del padre del padre de Cilindro2
        objetoPulsable = transform.parent.transform.parent.GetComponent<ObjetoPulsable>();
        bandera = false;

        // El renderer del Cilindro2
        miRenderer = GetComponent<Renderer>();

        // El material inicial del Cilindro2
        miColorInicial = miRenderer.material.color;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(objetoPulsable.EstaPulsado() && bandera == false)
        {
            bandera = true;

            // Se pone un color destacado
            miRenderer.material.color = new Color(0.85f, 0.85f, 0.44f, 1.0f);
        }

        else if(objetoPulsable.EstaPulsado() == false && bandera == true)
        {
            bandera = false;

            // Se vuelve al color inicial
            miRenderer.material.color = miColorInicial;
        }

	}
}
