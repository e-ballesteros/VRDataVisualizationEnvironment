using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AjustaTamaño : MonoBehaviour {

    public RawImage foto;
    public GameObject panel;
    public GameObject soporte;
    public GameObject boton;

    // El tamaño de la pantalla se debe ajustar desde el Inspector
    public float escalaInicial;

    // El tamaño del marco
    public float marco;

    private float ancho;
    private float alto;
    private float proporcion;   // Almacena el ancho/alto

    private float anchoAnterior;
    private float altoAnterior;

    void Start () {

        ancho = 1;
        alto = 1;

        anchoAnterior = 1;
        altoAnterior = 1;

    }
	
    // Debe ser LateUpdate para asegurar que se han cargado todas las imágenes previamente al escalado
	void LateUpdate () {

        // Se lee en píxeles el ancho y el alto
        ancho = foto.texture.width;
        alto = foto.texture.height;

        // Sólo se realiza el reescalado cuando cambie el ancho o el alto
        if((anchoAnterior != ancho) || (altoAnterior != alto))
        {
            Reescala();
            anchoAnterior = ancho;
            altoAnterior = alto;
        }
    }

    private void Reescala()
    {
        proporcion = ancho / alto;

        // Reescalado del panel
        panel.GetComponent<RectTransform>().localScale = new Vector3(escalaInicial * proporcion, escalaInicial, 1);

        // Reescalado del soporte. La profundidad de la pantalla es constante: 0.05 (No se cambia nunca)
        soporte.transform.localScale = new Vector3(proporcion + marco, 1 + marco, 0.04f);

        // Reescalado del botón
        boton.transform.localScale = new Vector3(proporcion, 1, 0.005f);
    }

}

