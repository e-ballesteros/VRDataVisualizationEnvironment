using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPantalla : MonoBehaviour {

    // Referencias a BotonArriba y a BotonAbajo
    public GameObject pulsadorIzquierda;
    public GameObject pulsadorDerecha;

    // Referencia a TextoPantalla
    public TextoPantalla textoPantalla;

    // Referencias a ObjetoPulsable de BotonArriba y BotonAbajo
    private ObjetoPulsable objetoPulsableIzquierda;
    private ObjetoPulsable objetoPulsableDerecha;

	// Use this for initialization
	void Start () {

        objetoPulsableIzquierda = pulsadorIzquierda.GetComponent<ObjetoPulsable>();
        objetoPulsableDerecha = pulsadorDerecha.GetComponent<ObjetoPulsable>();

    }

    // Update is called once per frame
    void Update () {
		
        // Se comprueba si BotonArriba está pulsado y si es así se retrocede una página
        if (objetoPulsableIzquierda.EstaPulsado())
        {
            // Una vez pulsado se despulsa para evitar sucesivas lecturas del botón pulsado
            objetoPulsableIzquierda.CambiaEstado();
            Debug.Log("Anterior texto");
            textoPantalla.AnteriorTexto();
        }

        // Se comprueba si BotonArriba está pulsado y si es así se avanza una página
        if (objetoPulsableDerecha.EstaPulsado())
        {
            // Una vez pulsado se despulsa para evitar sucesivas lecturas del botón pulsado
            objetoPulsableDerecha.CambiaEstado();
            Debug.Log("Siguiente texto");
            textoPantalla.SiguienteTexto();
        }

    }
}
