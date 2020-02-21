using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzadorRayosVisor : MonoBehaviour {

    // El objeto que se observa en este instante y el objeto que se observaba anteriormente
    private ObjetoObservable p_objetoObservableActual;
    private ObjetoObservable p_objetoObservableAnterior;

    // El alcance del rayo del visor
    private float longitudRayo;

    private QueryTriggerInteraction queryTriggerInteraction;

    // Para que las demás clases puedan acceder al objetoObservableActual
    public ObjetoObservable objetoObservableActual
    {
        get
        {
            return objetoObservableActual;
        }
    }

    // Si activado es true el LanzadorRayosVisor lanza rayos
    private bool activado;

    void Start () {

        // En un primer instante no se lanzan rayos
        activado = true;

        // HAY QUE PROBAR
        longitudRayo = 5;

	}
	
	void Update () {

        // Sólo se lanzan rayos si está activado
        if (activado)
        {
            LanzaRayos();
        }

	}

    private void LanzaRayos()
    {

        // Define un rayo frontal a la visión de la cámara
        Ray rayo = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // Almacena información sobre el choque del rayo con el ObjetoObservable
        RaycastHit choque;

        // Lanza el rayo en la dirección de rayo
        if (Physics.Raycast(rayo, out choque, longitudRayo))
        {
            ObjetoObservable observable = choque.collider.GetComponent<ObjetoObservable>();
            p_objetoObservableActual = observable;

            // Si se ha chocado con un ObjetoObservable y es distinto del anterior
            if (observable && observable != p_objetoObservableAnterior)
            {


                // Se activa el objetoObservable
                observable.Activando();
            }

            // Se desactiva el ObjetoObservable anterior
            if (observable != p_objetoObservableAnterior)
            {
                DesactivarObjetoObservableAnterior();
            }

            p_objetoObservableAnterior = observable;

        }

        // No se ha chocado con nada
        else
        {
            // Se desactiva el objetoObservableAnterior
            DesactivarObjetoObservableAnterior();
            p_objetoObservableActual = null;

        }

    }

    // Para activar el LanzadorRayosVisor
    public void CambiaEstado()
    {
        activado = activado ^ true;
    }

    // Para comprobar si se encuentra activo el LanzadorRayosVisor
    public bool EstaActivo()
    {
        return activado;
    }

    // Para desactivar el ObjetoObservable anterior
    private void DesactivarObjetoObservableAnterior()
    {
        if (p_objetoObservableAnterior  == null)
            return;

        // Se desactiva el objetoObservableAnterior
        p_objetoObservableAnterior.Desactivando();
        p_objetoObservableAnterior = null;

    }
}
