using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Para cualquier objeto que queramos hacer aparecer y desaparecer en ciertos momentos, desactivando y activando
// el renderer del objeto

public class EsconderAparecer : MonoBehaviour {

    new private Renderer renderer;

	void Start () {

        renderer = GetComponent<Renderer>();

	}
	
	void Update () {
		
	}

    public void Esconder()
    {
        renderer.enabled = true;
    }

    public void Aparecer()
    {
        renderer.enabled = false;
    }
}
