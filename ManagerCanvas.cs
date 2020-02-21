using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerCanvas : MonoBehaviour {

    public GameObject alertasEmocion;
    public GameObject botonCrearGrafico;
    public ControlGraficos controlGraficos;
    public Text textoAlertasEmocion;                                    // El texto que se muestra de en el UI AlertasEmocion 

    //private ObjetoPulsable objetoPulsableCrearGrafico;                // Seguramente no haga falta

	void Start () {

        //objetoPulsableCrearGrafico = botonCrearGrafico.GetComponent<ObjetoPulsable>();    // Seguramente no haga falta

	}

    void Update()
    {
        if (controlGraficos.MuestraError() != null)                     // Si existe algún error al crear gráfico
        {
            alertasEmocion.SetActive(true);                             // Se activa el Canvas AlertasEmocion
            textoAlertasEmocion.text = controlGraficos.MuestraError();  // Se muestra el error gestionado por controlGraficos
        }
        else                                                            // Debe desaparecer el Canvas AlertasEmocion
        {
            alertasEmocion.SetActive(false);                            // Se desactiva el Canvas AlertasEmocion
        }
    }
}
