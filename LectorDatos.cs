using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LectorDatos : MonoBehaviour {

    // Nombre del archivo input, sin extension, situado en la carpeta Resources
    public string archivoInput;

    /*
    // Número máximo de personas
    public int maximoPersonas = 4;

    // Número máximo de informacion sobre cada persona
    public int maximoInformacion = 4;
    */

    // List para almacenamiento de datos del lector CSV
    private List<Dictionary<string, object>> listaDatos;

    // Almacena la lista de nombres que conoce el robot
    private List<string> listaNombres;

    // Vectores (matriz) de strings con los datos de cada persona (desde el 0), ordenados como animal, comida, paisaje y palabra
    // Primer valor es la fila (variable) y segundo valor es la columna (la persona)
    //PREGUNTAR COMO HACER PARA QUE EL TAMAÑO DE LA MATRIZ SE INDIQUE MEDIANTE UNA VARIABLE
    private string[,] matrizDatos = new string [4,5];

    // Es Awake porque debe primero leerse los datos del txt antes de realizar el resto de instrucciones en ControlPantallaPersona
    void Awake () {

        // Se leen los datos del archivoInput al comienzo del programa
        LeerDatosTxt();

	}
	
	void Update () {
		
	}


    //**INCOMPLETO
    public void LeerDatosTxt()
    {
        // Resultado funcion leer del LectorCSV en listaPuntos con argumento archivoInput
        Debug.Log("LLamadaLectorCSV");
        listaDatos = LectorCSV.Leer(archivoInput);

        // Lista de cadenas, leyendo las keys de la primera fila (nombres de las personas)
        listaNombres = new List<string>(listaDatos[1].Keys);

        // Loop en listaNombres a lo largo de todas las columnas que haya
        for (var j = 0; j < listaNombres.Count; j++)
        {
            // Loop en listaDatos a lo largo de todos los datos que haya
            for (var i = 0; i < listaDatos.Count; i++)  // listaPuntos.Count devuelve el número de filas en la lista
            {

               

                // Convierte el objeto leído por LectorCSV a string para poder ser utilizado
                // AQUÍ HAY UN ERROR EL TIPO DE listaDatos[i][listaNombres[j]] NO ES OBJETO
                matrizDatos[i,j] = (string)listaDatos[i][listaNombres[j]];
                Debug.Log("Dato almacenado en fila " + i + "y columna " + j + ": " + matrizDatos[i, j]);

            }
        }

    }

    // Devuelve la lista de strings con el nombre de las personas
    public List<string> LeerListaNombres()
    {
        return listaNombres;
    }


    // Devuelve un vector de strings con los datos de cada persona (desde el 0), ordenados como animal, comida, paisaje y palabra 
    public string[] LeerDatosPersona(int persona)
    {
        // El vector de strings con los datos de cada persona (animal, comida, paisaje, palabra)
        string[] datosPersona = new string[4];

        for (var k = 0; k < 4; k++)
        {
            datosPersona[k] = matrizDatos[k,persona];
        }

        return datosPersona;
    }



}
