using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGraficos : MonoBehaviour {

    public GameObject botonGrafico1;
    public GameObject botonGrafico2;
    public GameObject botonGrafico3;
    public GameObject botonGrafico4;
    public GameObject botonGrafico5;
    public GameObject botonGrafico6;
    public GameObject botonGrafico7;
    public GameObject botonGrafico8;
    public GameObject botonGrafico9;
    public GameObject botonGrafico10;
    public GameObject botonGrafico11;
    public GameObject botonGrafico12;
    public GameObject botonGrafico13;


    public GameObject botonModo3D;
    public GameObject botonCrearGrafico;

    private ObjetoPulsable objetoPulsable1;
    private ObjetoPulsable objetoPulsable2;
    private ObjetoPulsable objetoPulsable3;
    private ObjetoPulsable objetoPulsable4;
    private ObjetoPulsable objetoPulsable5;
    private ObjetoPulsable objetoPulsable6;
    private ObjetoPulsable objetoPulsable7;
    private ObjetoPulsable objetoPulsable8;
    private ObjetoPulsable objetoPulsable9;
    private ObjetoPulsable objetoPulsable10;
    private ObjetoPulsable objetoPulsable11;
    private ObjetoPulsable objetoPulsable12;
    private ObjetoPulsable objetoPulsable13;


    private ObjetoPulsable objetoPulsableModo3D;
    private ObjetoPulsable objetoPulsableCrearGrafico;

    private string mensajeError;    // Almacena el mensaje de error que se debe mostrar cada momento, null en caso de no haber error

    public TrazadorGraficos trazadorGraficos;

    // En él se almacenan los puntos instanciados. Se instancia en Start y se destruye y vuelve a instanciar en TrazarGrafico()
    private GameObject soportePuntos;

    // Objeto que contiene los prefabs instanciados
    public GameObject SoportePuntos;

    // Prefab para instanciar la línea
    public GameObject PrefabLinea;

    // Instancia de PrefabLinea
    private GameObject lineaGrafico;

    // Almacena en un array los estados de los botones solo cuando se pulsa el boton de crear grafico
    private bool[] vectorBotones;       

    // Se almacenan las variables que serán empleadas para trazar los gráficos 
    private int variable1 = 0;
    private int variable2 = 0;
    private int variable3 = 0;

    // Bandera que se activa si hay demasiados botones pulsados para impedir crear gráfico
    private bool error = false;
    private int contador = 0;

    void Start () {

        // Referencias a ObjetoPulsable de todos los botones
        objetoPulsable1 = botonGrafico1.GetComponent<ObjetoPulsable>();    
        objetoPulsable2 = botonGrafico2.GetComponent<ObjetoPulsable>();
        objetoPulsable3 = botonGrafico3.GetComponent<ObjetoPulsable>();
        objetoPulsable4 = botonGrafico4.GetComponent<ObjetoPulsable>();
        objetoPulsable5 = botonGrafico5.GetComponent<ObjetoPulsable>();
        objetoPulsable6 = botonGrafico6.GetComponent<ObjetoPulsable>();
        objetoPulsable7 = botonGrafico7.GetComponent<ObjetoPulsable>();
        objetoPulsable8 = botonGrafico8.GetComponent<ObjetoPulsable>();
        objetoPulsable9 = botonGrafico9.GetComponent<ObjetoPulsable>();
        objetoPulsable10 = botonGrafico10.GetComponent<ObjetoPulsable>();
        objetoPulsable11= botonGrafico11.GetComponent<ObjetoPulsable>();
        objetoPulsable12 = botonGrafico12.GetComponent<ObjetoPulsable>();
        objetoPulsable13 = botonGrafico13.GetComponent<ObjetoPulsable>();


        objetoPulsableCrearGrafico = botonCrearGrafico.GetComponent<ObjetoPulsable>();
        objetoPulsableModo3D = botonModo3D.GetComponent<ObjetoPulsable>();

        // En el instante inicial se informa al usuario de que las instrucciones están a su espalda
        mensajeError = "Mire las instrucciones situadas a su espalda";

        vectorBotones = new bool[13];

    }

    void Update () {

        // Se comprueba si el modo 3D está pulsado o no y se informa al TrazadorGraficos del modo
        if (objetoPulsableModo3D.EstaPulsado())
        {
            trazadorGraficos.CambiaModo3D();
        }
        else
        {
            trazadorGraficos.CambiaModo2D();
        }

        // Se comprueba continuamente si se ha pulsado el botón de crear gráfico
        if (objetoPulsableCrearGrafico.EstaPulsado()){


            // Se introducen los valores de los botones en el vector
            bool[] vectorBotones = {
                objetoPulsable1.EstaPulsado(),
                objetoPulsable2.EstaPulsado(),
                objetoPulsable3.EstaPulsado(),
                objetoPulsable4.EstaPulsado(),
                objetoPulsable5.EstaPulsado(),
                objetoPulsable6.EstaPulsado(),
                objetoPulsable7.EstaPulsado(),
                objetoPulsable8.EstaPulsado(),
                objetoPulsable9.EstaPulsado(),
                objetoPulsable10.EstaPulsado(),
                objetoPulsable11.EstaPulsado(),
                objetoPulsable12.EstaPulsado(),
                objetoPulsable13.EstaPulsado(),
            };

            for( int i = 0; i < 13; i++)
            {
                if(vectorBotones[i] == true)
                {
                    if (variable1 == 0)
                    {
                        variable1 = i+1;

                        // El contador aumenta con cada asignación a variables columna
                        contador++;             
                    }
                    else if (variable2 == 0)
                    {
                        variable2 = i+1;

                        // El contador aumenta con cada asignación a variables columna
                        contador++;             
                    }
                    else if (variable3 == 0)
                    {
                        variable3 = i+1;

                        // El contador aumenta con cada asignación a variables columna
                        contador++;             
                    }
                    else
                    {
                        error = true;           // No se puede admitir una cuarta variable
                    }
                }
            }

            // Más de 3 variables seleccionadas
            if (error == true)
            {
                mensajeError = "Demasiadas variables seleccionadas";
                DestruirGrafico();
            }

            // Ninguna variable seleccionada
            else if (contador < 1)
            {
                mensajeError = "Seleccione alguna variable";
                DestruirGrafico();
            }

            else if ((contador != 3) && (objetoPulsableModo3D.EstaPulsado()))
            {
                mensajeError = "El modo 3D sólo es compatible con 3 variables seleccionadas";
                DestruirGrafico();
            }

            else
            {
                // Ya no existe error, se pone a null
                mensajeError = null;

                // Como ya no hay error se destruye el gráfico anterior
                DestruirGrafico();

                switch (contador)
                {
              
                    case 1:
                        // Se crea el primer gráfico con la variable1
                        trazadorGraficos.columnaY = variable1;
                        trazadorGraficos.TrazarGrafico();

                        break;

                    case 2:
                        // Se crea el primer gráfico con la variable1
                        trazadorGraficos.columnaY = variable1;
                        trazadorGraficos.TrazarGrafico();

                        // Se crea el segundo gráfico con la variable2
                        trazadorGraficos.columnaY = variable2;
                        trazadorGraficos.TrazarGrafico();

                        break;

                    case 3:

                        // Con tres variables en modo 3D
                        if (objetoPulsableModo3D.EstaPulsado())
                        {
                            // Se envía a trazadorGraficos las tres variables que se van a representar en X, Y, Z
                            trazadorGraficos.columnaX = variable1;
                            trazadorGraficos.columnaY = variable2;
                            trazadorGraficos.columnaZ = variable3;
                            trazadorGraficos.TrazarGrafico();
                        }

                        // Con tres variables en modo 2D
                        else
                        {
                            // La variable X es siempre el tiempo
                            trazadorGraficos.columnaX = 0;

                            // Se envía a trazadorGráficos cada variable por separado y se representan uno a uno los gráficos
                            trazadorGraficos.columnaY = variable1;
                            trazadorGraficos.TrazarGrafico();

                            trazadorGraficos.columnaY = variable2;
                            trazadorGraficos.TrazarGrafico();

                            trazadorGraficos.columnaY = variable3;
                            trazadorGraficos.TrazarGrafico();
                        }
                        break;
                    
                }
            }
            // Se elimina pulsación y se reinicia contador para la siguiente vez que se intente crear gráfico
            objetoPulsableCrearGrafico.CambiaEstado();
            contador = 0;

            // Se reinician las variables seleccionadas para la próxima vez que se cree gráfico
            variable1 = 0;
            variable2 = 0;
            variable3 = 0;
        }

    }

    public string MuestraError()
    {
        return mensajeError;
    }

    private void DestruirGrafico()
    {
        GameObject[] puntos = GameObject.FindGameObjectsWithTag("SoportePuntos");
        GameObject[] lineas = GameObject.FindGameObjectsWithTag("LineaGraficos");
        if (puntos != null || lineas != null)
        {
            // Se destruyen los anteriores puntos
            for (int i = 0; i < puntos.Length; i++)
            {
                Destroy(puntos[i]);
            }

            // Se destruyen las anteriores líneas
            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i] != null) Destroy(lineas[i]);
            }
        }
    }
}
