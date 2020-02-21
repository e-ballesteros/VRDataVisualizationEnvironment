using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mando : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    // Para saber el index del mando en cada momento
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    HashSet<ObjetoInteractivo> objetoscercanos = new HashSet<ObjetoInteractivo>(); // Con el HashSet conseguiremos elegir un solo objeto para agarrar

    private ObjetoInteractivo objetoMasCercano; // El objeto más cercano será el agarrado
    private ObjetoInteractivo objetoAgarrado;   // El objeto que se está agarrando actualmente

    private ObjetoPulsable objetoPulsable;
    private bool gripPulsando;                  // Si gripPulsando == true, el grip se está pulsando

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        gripPulsando = false;
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Mando no inicializado");
        }

        if (controller.GetPressDown(triggerButton))                    // Aquí van todas las posibles acciones con el botón Trigger
        {
            

            float minDistancia = float.MaxValue;
            float distancia;

            foreach (ObjetoInteractivo objeto in objetoscercanos)    // Calcula distancia de cada objeto al mando y comprueba si es la mínima 
            {
                distancia = (objeto.transform.position - transform.position).sqrMagnitude;  // Calcula distancia, no vector
                if (distancia < minDistancia)
                {
                    minDistancia = distancia;
                    objetoMasCercano = objeto;
                }
            }

            objetoAgarrado = objetoMasCercano;                      // El objeto que se agarrará es el que tenga la menor distancia al mando
            objetoMasCercano = null;                                // Borra el objeto más cercano una vez agarrado

            if (objetoAgarrado)                                     
            {
                if (objetoAgarrado.EstaAgarrando())                 // Si el objeto ya está siendo agarrado (por otro mando) termina agarre con el
                {                                                   // otro mando para comenzar a agarrar con el nuevo
                    objetoAgarrado.FinAgarre(this);
                }
                objetoAgarrado.ComienzoAgarre(this);                // Comienza a agarrar el objeto
            }
        }

        if (controller.GetPressUp(triggerButton) && objetoAgarrado != null)  // Botón trigger no está pulsado y hay un objeto agarrado
        {
            objetoAgarrado.FinAgarre(this);
        }

        if (controller.GetPressDown(gripButton))                 // Aquí van todas las acciones con el botón Grip
        {
            if (objetoPulsable && gripPulsando == false)         // Si hay un objeto pulsable en el collider y Trigger no estaba pulsado antes
            {
                objetoPulsable.CambiaEstado();                      // Cambia el estado del botón
                gripPulsando = true;                             // Se cambia bandera para evitar que encienda y apague mientras se pulsa
            }
        }

        if (controller.GetPressUp(gripButton))
        {
            gripPulsando = false;                                // Se reinicia la bandera
        }

        /*gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);*/

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        ObjetoInteractivo objetoInteractuando = other.GetComponent<ObjetoInteractivo>(); // Un objeto que simplemente ha entrado en el collider del mando
        if (objetoInteractuando)                                                         // Si el objeto que ha entrado es un ObjetoInteractivo
        {
            objetoscercanos.Add(objetoInteractuando);                                    // Añade al HashSet ese objeto
        }
        //***HAY QUE PROBAR ESTO***//
        ObjetoPulsable esObjetoPulsable = other.GetComponent<ObjetoPulsable>();          // esObjetoPulsable es la variable donde se comprueba si es
        if (esObjetoPulsable != null)                                                            // pulsable y objetoPulsable donde se guarda la referencia
        {                                                                                // del objeto que ha entrado y es pulsable
            Debug.Log("esObjetoPulsable");

            objetoPulsable = esObjetoPulsable;
        }
        //***HAY QUE PROBAR ESTO***//
    }

    private void OnTriggerExit(Collider other)
    {
        ObjetoInteractivo objetoInteractuando = other.GetComponent<ObjetoInteractivo>(); // Un objeto que ha salido del collider del mando
        if (objetoInteractuando)                                                         // Si el objeto que ha salido es un ObjetoInteractivo
        {
            objetoscercanos.Remove(objetoInteractuando);                                 // Quita del HashSet ese objeto
        }
        //***HAY QUE PROBAR ESTO***//
        ObjetoPulsable esObjetoPulsable = other.GetComponent<ObjetoPulsable>();          // Se comprueba si el objeto que ha salido es pulsable
        if (esObjetoPulsable != null)                                                            // Si es pulsable, se pone objetoPulsable = null y se
        {                                                                                // reinicializa la variable de comprobacion esObjetoPulsable
            objetoPulsable = null;
            esObjetoPulsable = null;
        }
        //***HAY QUE PROBAR ESTO***//

    }

}
