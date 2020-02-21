using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaDatos : MonoBehaviour {

    // El valor de las variables del punto 
    private Vector3 valorVariablesPunto;

    // El nombre de cada variable del punto
    private string variableX;
    private string variableY;
    private string variableZ;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Para almacenar los valores de las variables en el punto
    public void GuardarValores(Vector3 valores)
    {
        valorVariablesPunto = valores;
    }

    // Para almacenar los nombres de las variables en el punto
    public void GuardarVariables(string nombreX, string nombreY, string nombreZ)
    {
        variableX = nombreX;
        variableY = nombreY;
        variableZ = nombreZ;
    }

    // Para consultar los valores de las variables del punto
    public Vector3 ConsultarValores()
    {
        return valorVariablesPunto;
    }

    public string[] ConsultarVariables()
    {
        string[] variables = new string[3];

        variables[0] = variableX;
        variables[1] = variableY;
        variables[2] = variableZ;

        return variables;
    }

}
