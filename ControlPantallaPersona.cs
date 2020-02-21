using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPantallaPersona : MonoBehaviour {

    // Los GameObjects UI Text a modificar en función de la lectura del txt
    public GameObject textoNombre;
    public GameObject textoAnimal;
    public GameObject textoComida;
    public GameObject textoPaisaje;
    public GameObject textoPalabra;

    // El lectorDatos proporciona todos los datos de texto leídos del txt
    public LectorDatos lectorDatos;

    // El LectorImagenes de la pantalla principal
    public LectorImagenes lectorImagenes;

    /*
    // Los botones para cambiar de persona
    public GameObject siguientePersona;
    public GameObject anteriorPersona;
    */

    // Los Components Text de los textos, es donde finalmente se escribe la información
    private Text palabraTextoNombre;
    private Text palabraTextoAnimal;
    private Text palabraTextoComida;
    private Text palabraTextoPaisaje;
    private Text palabraTextoPalabra;


    /*
    // ObjetoPulsable de los dos botones
    private ObjetoPulsable objetoPulsableSiguiente;
    private ObjetoPulsable objetoPulsableAnterior;
    */

    // Lista de nombres leída
    private List<string> listaNombres;

    // Variable int que indica la persona que se debe mostrar en pantalla (la columna del txt a mostrar) comenzando desde 0
    private int personaMostrada;

    
    //BANDERA PARA HACER PRUEBAS
    bool banderaprueba = true;
    
    void Start () {
        /*
        // Se obtienen los Components ObjetoPulsable de cada botón
        objetoPulsableSiguiente = siguientePersona.GetComponent<ObjetoPulsable>();
        objetoPulsableAnterior = anteriorPersona.GetComponent<ObjetoPulsable>();
        */

        // Se obtienen los Components Text de los textos
        palabraTextoNombre = textoNombre.GetComponent<Text>();
        palabraTextoAnimal = textoAnimal.GetComponent<Text>();
        palabraTextoComida = textoComida.GetComponent<Text>();
        palabraTextoPaisaje = textoPaisaje.GetComponent<Text>();
        palabraTextoPalabra = textoPalabra.GetComponent<Text>();

        // Se comienza mostrando la primera persona
        personaMostrada = 0;
       
        // Se almacena la lista de nombres para posteriormente poder escribir los strings en los objetos UI de texto
        listaNombres = lectorDatos.LeerListaNombres();

    }

    void Update () {
        
        
        if (banderaprueba)
        {
            banderaprueba = false;
            MuestraPersona(personaMostrada);
        }
        

        /*
        // Se comprueba si el botón siguiente está pulsado
        if (objetoPulsableSiguiente.EstaPulsado())
        {
            // Se despulsa el botón
            objetoPulsableSiguiente.CambiaEstado();

            // Si no es la última persona de la lista de personas conocidas
            if(personaMostrada + 1 < listaNombres.Count)
            {
                // Se muestra la siguiente persona
                personaMostrada++;
                EscribeDatos();
            }
        }

        if (objetoPulsableAnterior.EstaPulsado())
        {
            // Se despulsa el botón
            objetoPulsableAnterior.CambiaEstado();

            // Si no es la primera persona de la lista de personas conocidas
            if (personaMostrada > 0)
            {
                // Se muestra la anterior persona
                personaMostrada--;
                EscribeDatos();
            }
        }
        */
    }

    // Se llama a esta función con argumento número de la persona (columna en txt)
    public void MuestraPersona(int persona)
    {
        // Se cambia la persona a mostrar
        personaMostrada = persona;

        // Se escriben los datos en PantallaPersona
        EscribeDatos();
        ImprimeImagen();
    }

    // Función a la que se llama en ciertos momentos para que escriba los nuevos datos en PantallaPersona
    private void EscribeDatos()
    {
        // Se escribe el nombre de la persona
        palabraTextoNombre.text = listaNombres[personaMostrada];

        // Variable local para almacenar lo que se recibe desde LectorDatos y llamada a lectorDatos.LeerDatosPersona
        // datosPersona almacena: 0 - Animal, 1 - Comida, 2 - Paisaje, 3 - Palabra
        string[] datosPersona = lectorDatos.LeerDatosPersona(personaMostrada);

        // Se escriben los datos de la persona
        palabraTextoAnimal.text = datosPersona[0];
        palabraTextoComida.text = datosPersona[1];
        palabraTextoPaisaje.text = datosPersona[2];
        palabraTextoPalabra.text = datosPersona[3];
    }

    private void ImprimeImagen()
    {
        // Lectura de la primera imagen de la persona seleccionada
        lectorImagenes.LeerImagen(personaMostrada.ToString(), personaMostrada.ToString() + ".0");
    }
}
