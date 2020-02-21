using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoObservable : MonoBehaviour {

    private bool on;

    private EsferaDatos esferaDatos;

    public Text textoInformacion;

    private ObjetoPulsable objetoPulsableModo3D;

    private Image fondoInformacion;

    private ControlGraficos controlGraficos;

    void Start () {

        on = false;

        // El Text donde se escriben los valores de las variables del punto
        textoInformacion = GameObject.FindWithTag("TextoInformacion").GetComponent<Text>();

        // El fondo donde se escriben los valores de las variables del punto
        fondoInformacion = GameObject.FindWithTag("FondoInformacion").GetComponent<Image>();

        // El objetoPulsable del botonModo3D
        objetoPulsableModo3D = GameObject.FindWithTag("BotonModo3D").GetComponent<ObjetoPulsable>();

        // ControlGraficos de la escena
        controlGraficos = GameObject.FindWithTag("ControlGraficos").GetComponent<ControlGraficos>();

        // El script EsferaDatos del objeto
        esferaDatos = GetComponent<EsferaDatos>();

    }
	
	void Update () {
		


	}

    // Se llama cuando se empieza a observar el objeto
    public void Activando()
    {
        on = true;
        Debug.Log("Ha habido un choque");
        Debug.Log(esferaDatos.ConsultarValores().x);
        Debug.Log(esferaDatos.ConsultarValores().y);
        Debug.Log(esferaDatos.ConsultarValores().z);

        // Aquí se introduce lo que se hace cuando se empieza a activar

        // Se activa el fondo del texto
        fondoInformacion.enabled = true;

        // Si se está en modo 3D se deben mostrar los valores de las tres variables 
        if (objetoPulsableModo3D.EstaPulsado() && controlGraficos.MuestraError() != "El modo 3D sólo es compatible con 3 variables seleccionadas")
        {
            // Se escriben los valores de las variables del punto
            textoInformacion.text = (esferaDatos.ConsultarVariables()[0] + " = " +
                esferaDatos.ConsultarValores().x + "\r\n" +
                esferaDatos.ConsultarVariables()[1] + " = " +
                esferaDatos.ConsultarValores().y + "\r\n" + 
                esferaDatos.ConsultarVariables()[2] + " = " +
                esferaDatos.ConsultarValores().z
                );
        }

        else
        {
            textoInformacion.text = (esferaDatos.ConsultarVariables()[0] + " = " +
                esferaDatos.ConsultarValores().x + "\r\n" +
                esferaDatos.ConsultarVariables()[1] + " = " +
                esferaDatos.ConsultarValores().y
                );
        }




        

        // Aquí se introduce lo que se hace cuando se empieza a activar

    }

    // Se llama cuando deja de observarse el objeto
    public void Desactivando()
    {
        on = false;

        // Aquí se introduce lo que se hace cuando se desactiva

        // Se desactiva el fondo del texto
        fondoInformacion.enabled = false;

        // Se borra el texto con la información del punto
        textoInformacion.text = "";

        // Aquí se introduce lo que se hace cuando se desactiva

    }

    // Devuelve si el objeto está siendo observado
    public bool EsObservado()
    {
        return on;
    }

}
