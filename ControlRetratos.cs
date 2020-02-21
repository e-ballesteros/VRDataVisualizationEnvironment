using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRetratos : MonoBehaviour {

    // Los botones de los retratos de las personas conocidas
    public GameObject botonRetrato0;
    public GameObject botonRetrato1;
    public GameObject botonRetrato2;
    public GameObject botonRetrato3;

    // Los lectores de imágenes de las personas conocidas
    public LectorImagenes lectorImagenes0;
    public LectorImagenes lectorImagenes1;
    public LectorImagenes lectorImagenes2;
    public LectorImagenes lectorImagenes3;

    // El control de la pantalla central. ControlRetratos le va a comunicar qué persona se debe mostrar cada momento
    public ControlPantallaPersona controlPantallaPersona;

    // ObjetoPulsable de los cuatro boton
    private ObjetoPulsable objetoPulsable0;
    private ObjetoPulsable objetoPulsable1;
    private ObjetoPulsable objetoPulsable2;
    private ObjetoPulsable objetoPulsable3;

    // La persona (columna) que se debe mostrar en la pantalla
    private int persona;


    // Use this for initialization
    void Start () {

        // Referencias objetosPulsables
        objetoPulsable0 = botonRetrato0.GetComponent<ObjetoPulsable>();
        objetoPulsable1 = botonRetrato1.GetComponent<ObjetoPulsable>();
        objetoPulsable2 = botonRetrato2.GetComponent<ObjetoPulsable>();
        objetoPulsable3 = botonRetrato3.GetComponent<ObjetoPulsable>();

        // Se cargan las primeras imágenes de cada carpeta en los retratos
        lectorImagenes0.LeerImagen("0", "0.0");
        lectorImagenes1.LeerImagen("1", "1.0");
        lectorImagenes2.LeerImagen("2", "2.0");
        lectorImagenes3.LeerImagen("3", "3.0");

    }

    // Update is called once per frame
    void Update () {


        if (objetoPulsable0.EstaPulsado())
        {
        Debug.Log("Pulsacion");

            persona = 0;
            objetoPulsable0.CambiaEstado();
            controlPantallaPersona.MuestraPersona(persona);
        }

        else if (objetoPulsable1.EstaPulsado())
        {
            Debug.Log("Pulsacion");

            persona = 1;
            objetoPulsable1.CambiaEstado();
            controlPantallaPersona.MuestraPersona(persona);
        }

        else if (objetoPulsable2.EstaPulsado())
        {
            Debug.Log("Pulsacion");

            persona = 2;
            objetoPulsable2.CambiaEstado();
            controlPantallaPersona.MuestraPersona(persona);
        }

        else if (objetoPulsable3.EstaPulsado())
        {
            Debug.Log("Pulsacion");

            persona = 3;
            objetoPulsable3.CambiaEstado();
            controlPantallaPersona.MuestraPersona(persona);
        }

    }
}
