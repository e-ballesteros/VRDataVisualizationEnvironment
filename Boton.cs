using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boton : MonoBehaviour
{
    // El botón le dice al elevador que suba
    public Elevador elevador;
    
    public float velocidad = 1.5f;

    // El botón comunica al gestorEscenas a qué escena hay que cambiar
    public GestorEscenas gestorEscenas;                     

    private ObjetoPulsable objetoPulsable;
    private Oscilacion oscilacion;

    private Vector3 posicionObjetivo;

    // En relación con la clase madre
    private float posicionVisible = 0f;                     
    private float posicionEscondidoArriba = 5f;

    // TRUE cuando está desplazada, FALSE cuando llega al pto central
    private bool banderaDesplazamiento = false;

    // Distancia de la esfera con posición central para comenzar oscilación
    public float zonaOscilacion = 0.008f;

    // Distingue esferas: 1- Emoción 2- Recuerdo 3- Personalidad
    private string escenaEsfera;

    // Bandera que permite o no comprobar entrada
    private bool banderaEntradaElevador = false;
    
    void Awake()
    {
        // Referencia a Oscilacion del objeto
        oscilacion = GetComponent<Oscilacion>();

        // Referencia a ObjetoPulsable del objeto
        objetoPulsable = GetComponent<ObjetoPulsable>();

        // Al cargar la escena directamente se encuentran en posición escondido
        DesaparecerArriba();                                
        transform.localPosition = posicionObjetivo;                          

        // Asignación valor a identificador según nombre esfera
        if (this.gameObject.name == "Boton1")               
        {
            escenaEsfera = "Sala de Emociones";
        }
        else if (this.gameObject.name == "Boton2")
        {
            escenaEsfera = "Sala de Recuerdos";
        }
        else if (this.gameObject.name == "Boton3")
        {
            escenaEsfera = "Sala de Personalidad";
        }

        else if (this.gameObject.name == "BotonRegreso")
        {
            escenaEsfera = "Potato";
        }

        banderaEntradaElevador = true;

    }

    void Update()
    {
        // Si el botón está pulsado
        if (objetoPulsable.EstaPulsado()) 
        {
            DesaparecerArriba();

            // Le pasa a gestorEscenas la siguiente escena
            gestorEscenas.SiguienteEscena(escenaEsfera);            
            elevador.Teletransportar();
        }

        // Cuando se desplaza la posición se actualiza con lerp
        if (banderaDesplazamiento == true)                          
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                posicionObjetivo,
                Time.deltaTime * velocidad
                );
        }

        // Posible bug: si las esferas se desplazasen mientras están dentro zonaOscilación, se bloquearía para siempre (no va a pasar)

        if ( (Mathf.Abs(transform.localPosition.y) < zonaOscilacion) && (banderaDesplazamiento == true) )
        {
            banderaDesplazamiento = false;
            oscilacion.Flota();
        }

        // Cuando se sale del elevador las esferas desaparecen
        if (elevador.SalidaElevador())  
        {
            banderaEntradaElevador = true;
            DesaparecerArriba();
        }

        // Cuando se vuelve a entrar al elevador vuelven a aparecer
        else if (banderaEntradaElevador == true)
        {
            banderaEntradaElevador = false;
            Aparecer();
        }

    }

    void DesaparecerArriba()                // Debe llamarse cuando el jugador sale del elevador
    {
        banderaDesplazamiento = true;
        oscilacion.Quieto();                // Para de oscilar mientras se desplaza        
        posicionObjetivo = new Vector3(
            transform.localPosition.x,
            posicionEscondidoArriba,
            transform.localPosition.z
            );
    }

    void Aparecer()                         // Debe llamarse cuando el jugador entra en el elevador
    {
        banderaDesplazamiento = true;
        oscilacion.Quieto();                // Para de oscilar mientras se desplaza 
        posicionObjetivo = new Vector3(
            transform.localPosition.x,
            posicionVisible,
            transform.localPosition.z
            );
    }


}
