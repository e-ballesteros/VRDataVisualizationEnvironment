using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LectorDiscos : MonoBehaviour {

    // Referencia al prefab que se va a instanciar
    public Disco prefabDisco;

    // Referencias a los objetos disco instanciados

    public GameObject discoJazz;
    public GameObject discoClasica;

    // Variable que indica si hay algún disco reproduciendo para al reproducir otro, aquel se instancie en su lugar inicial
    private bool hayDiscoPuesto;

    // Referencia al disco que se esté reproduciendo
    private Disco discoActual;

    // Referencia al disco que se esté reproduciendo
    private Disco discoAnterior;

    // Música del disco que ha entrado en LectorDiscos
    private string musicaDiscoActual;

    // Música del disco que estaba en LectorDiscos
    private string musicaDiscoAnterior;

    // Para almacenar el origen en posición y rotación en que se haya situado el disco de Jazz
    private Vector3 posicionInicialJazz;
    private Quaternion rotacionInicialJazz;

    // Para almacenar el origen en posición y rotación en que se haya situado el disco de Clásica
    private Vector3 posicionInicialClasica;
    private Quaternion rotacionInicialClasica;

    // Para almacenar la posición y rotación al estar puesto el disco
    public Vector3 posicionReproduciendo;
    public Quaternion rotacionReproduciendo;

    // El component objetoInteractivo del discoActual
    private ObjetoInteractivo objetoInteractivo;

    // Evita que el trigger de salida haga nada
    private bool anuladorTriggerSalida;

    // Evita que el trigger de salida del mando haga nada
    private bool anuladorTriggerMando;

    // El altavoz que reproduce la canción
    public GameObject altavoz;

    // El AudioClip del altavoz
    private AudioClip audioClip;

    // El AudioClip del disco de música clásica
    private AudioClip audioClipClasica;

    // El AudioClip del disco de jazz
    private AudioClip audioClipJazz;

    // El AudioSource del altavoz
    private AudioSource audioSource;

    // Para comprobar si se ha cambiado de disco
    private string compruebaCambio;

    // Para indicar que se debe cambiar de canción
    private bool hazCambio;


    void Start () {

        audioSource = altavoz.GetComponent<AudioSource>();
        

        // Al principio no se está reproduciendo ningún disco
        hayDiscoPuesto = false;

        anuladorTriggerSalida = false;
        anuladorTriggerMando = true;

        hazCambio = false;
        
        // Se obtienen los valores iniciales de rotación y posición de los discos
        posicionInicialJazz = discoJazz.transform.localPosition;
        rotacionInicialJazz = discoJazz.transform.localRotation;

        // Se obtienen los valores iniciales de rotación y posición de los discos
        posicionInicialClasica = discoClasica.transform.localPosition;
        rotacionInicialClasica = discoClasica.transform.localRotation;

        musicaDiscoActual = "nada";

        audioClipClasica = discoClasica.GetComponent<Disco>().pista;
        audioClipJazz = discoJazz.GetComponent<Disco>().pista;


        /*
        // Referencia al collider del Lector Discos
        Collider collider = GetComponent<Collider>();
        */
    }

    void Update () {
        compruebaCambio = musicaDiscoActual;

        // Debe reproducirse la música
        if (hayDiscoPuesto == true && hazCambio == true)
        {
            Debug.Log("Ha habido un cambio de audio a" + musicaDiscoActual);

            // Ya no hay que cambiar de canción
            hazCambio = false;

            // Primero se para de reproducir
            audioSource.Stop();

            // Se cambia de canción
            if (musicaDiscoActual == "Clasica")
            {
                audioSource.clip = audioClipClasica;
            }

            else if(musicaDiscoActual == "Jazz")
            {
                audioSource.clip = audioClipJazz;
            }

            // Se reproduce
            audioSource.Play();

        }

        // Si no hay disco puesto no se reproduce nada
        else if (!hayDiscoPuesto)
        {
            audioSource.Stop();
            audioClip = null;
        }

	}

    // Cuando un objeto ha entrado en el collider de LectorDiscos
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ha entrado algo");

        if ((other.tag == "Disco") && (other.GetComponent<ObjetoInteractivo>().EstaAgarrando()))
        {
            // El disco que ha entrado es el actual
            Disco discoActual = other.GetComponent<Disco>();
            objetoInteractivo = discoActual.GetComponent<ObjetoInteractivo>();
            Rigidbody rigidbody = discoActual.GetComponent<Rigidbody>();

            // Almacena la anterior música a la asignación
            compruebaCambio = musicaDiscoActual;

            // Se almacena la música correspondiente al disco que ha entrado
            musicaDiscoActual = discoActual.musica;

            // Comprueba si ha habido algún cambio
            if(musicaDiscoActual != compruebaCambio)
            {
                hazCambio = true;
            }

            else
            {
                audioSource.Play();
            }

            if (hayDiscoPuesto)
            {

                // Desactivo el trigger para evitar que al sacar el disco detecte la salida del mismo
                anuladorTriggerSalida = true;

                // Se pone el discoAnterior en su posición inicial
                discoAnterior.transform.localPosition = posicionInicialJazz;
                discoAnterior.transform.rotation = rotacionInicialJazz;

                // Se activa la gravedad del discoAnterior
                Rigidbody rigidbodyAnterior = discoAnterior.GetComponent<Rigidbody>();
                rigidbodyAnterior.useGravity = true;
                rigidbodyAnterior.isKinematic = false;

            }

            rigidbody.isKinematic = true;

            // Se coloca el disco que ha entrado en la posición de reproducción
            discoActual.transform.position = posicionReproduciendo;
            discoActual.transform.rotation = rotacionReproduciendo;
            anuladorTriggerMando = false;

            // Se desactiva objetoInteractivo del disco para evitar que se siga cogiendo. Se activa en OnTriggerExit
            objetoInteractivo.enabled = false;

            // Ahora se sabe que hay disco puesto para las siguientes reproducciones
            hayDiscoPuesto = true;

            // Se almacena la anterior música para la próxima vez que se meta un disco
            musicaDiscoAnterior = musicaDiscoActual;

            // Se almacena la referencia en discoAnterior para el siguiente cambio de disco
            discoAnterior = discoActual;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigidbodyAnterior = discoAnterior.GetComponent<Rigidbody>();
        ObjetoInteractivo objetoInteractivo = discoAnterior.GetComponent<ObjetoInteractivo>();

        // Solo cuando el anuladorTriggerSalida lo permita (sea false)
        if (anuladorTriggerSalida == false)
        {

            // Si sale un disco del LectorDiscos
            if (other.tag == "Disco")
            {

                // Se activa la gravedad del discoAnterior
                rigidbodyAnterior.isKinematic = false;
                rigidbodyAnterior.useGravity = true;

                // Ya no se está reproduciendo música
                hayDiscoPuesto = false;
            }
        }

        // Se vuelve a permitir el trigger de salida
        anuladorTriggerSalida = false;

        // Si sale el mando del lector de discos se vuelve a poder coger el disco que está puesto
        if ((other.tag == "Mando") && anuladorTriggerMando == false)
            {

                // Si hay disco puesto se vuelve a activar el component ObjetoInteractivo del disco para poder volver a cogerlo
                if (hayDiscoPuesto)
                {
                    objetoInteractivo.enabled = true;
                    rigidbodyAnterior.useGravity = false;
                    rigidbodyAnterior.isKinematic = false;
                }

            anuladorTriggerMando = true;

            }
    }
}
