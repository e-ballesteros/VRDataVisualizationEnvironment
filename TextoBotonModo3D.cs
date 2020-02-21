using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoBotonModo3D : MonoBehaviour {

    // Referencia a ObjetoPulsable del parent BotonModo3D
    private ObjetoPulsable objetoPulsable;

    // Referencia al script Text de esta misma clase TextoBotonModo3D
    private Text text;

	void Start () {

        objetoPulsable = transform.parent.gameObject.GetComponent<ObjetoPulsable>();

        text = GetComponent<Text>();

	}
	
	void Update () {
	    
        // Si está pulsado se muestra el texto de activado
        if(objetoPulsable.EstaPulsado() == true)
        {
            text.text = "Modo 3D activado";
        }

        // Si no está pulsado se muestra el texto de desactivado
        else
        {
            text.text = "Modo 3D desactivado";
        }

	}
}
