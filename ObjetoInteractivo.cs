using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour {
    private Rigidbody rigidbody;

    private float factorVelocidad = 1f;     // Ajustar si es necesario
    private float factorRotacion = 1f;    // Ajustar si es necesario

    private bool agarrando;
    private Mando mandoagarre;              // El mando que agarra el objeto
    private Transform puntoagarre;          // El punto exacto donde se agarra el objeto (tu mano queda fija al punto de agarre)

    private Vector3 posDelta;
    private Quaternion rotacionDelta;
    private float angulo;
    private Vector3 eje;


	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        puntoagarre = new GameObject().transform;

	}
	
	void FixedUpdate () {
        if(mandoagarre && agarrando)                        // Si hay un mando agarrando
        {
            //Debug.Log("agarrando");
            posDelta = mandoagarre.transform.position - puntoagarre.position;
            rigidbody.velocity = posDelta / Time.fixedDeltaTime * factorVelocidad;   

            rotacionDelta = mandoagarre.transform.rotation * Quaternion.Inverse(puntoagarre.rotation); // No lo entiendo muy bien
            rotacionDelta.ToAngleAxis(out angulo, out eje);

            if(angulo > 180)
            {
                angulo -= 360;
            }
            rigidbody.angularVelocity = (eje * angulo) / Time.fixedDeltaTime * factorRotacion;  // En grados/s
        }
		
	}

    public void ComienzoAgarre(Mando mando)
    {
        mandoagarre = mando;
        puntoagarre.position = mando.transform.position;    // En el momento que comienzas a agarrar, se define el punto de agarre con la posición
        puntoagarre.rotation = mando.transform.rotation;    // actual del mando
        puntoagarre.SetParent(transform, true);             // El puntoagarre hereda la posición del mando

        agarrando = true;
    }

    public void FinAgarre(Mando mando)
    {
        if(mando == mandoagarre)
        {
            mandoagarre = null;
            agarrando = false;
        }
    }

    public bool EstaAgarrando()                             // Devuelve un 1 si actualmente hay algún mando agarrando
    {
        return agarrando;
    }
}
