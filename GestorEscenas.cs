using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscenas : MonoBehaviour {

    private string escenaSiguiente;

    void Update()
    {

    }

    // Para que los objetos puedan comunicar a GestorEscenas a que escena se debe cambiar
    public void SiguienteEscena(string escena)      
    {
        // Se almacena el nombre de la siguiente escena 
        escenaSiguiente = escena;                  
        Debug.Log(escenaSiguiente);
    }

    public void CambiaEscena()                     
    {
        Debug.Log("Cambia escena");
        SceneManager.LoadSceneAsync(escenaSiguiente);
    }
}
