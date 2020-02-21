using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


// Taken from here: https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/
// Comments

// Divide un CSV, convirtiendo valores en ints o floats si es posible y devolviendo un List<Dictionary<string, object>>

public class LectorCSV
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // Define limites de palabras
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r"; // Define limites de lineas
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Leer(string file) //Declare method
    
    {

        var list = new List<Dictionary<string, object>>(); // Declara lista de diccionarios

        //TextAsset data = Resources.Load(file) as TextAsset; // Lee el TextAsset en el archivo con el nombre pasado como argumento


        /*
        // Se lee el fichero datosvariables.txt
        if (file == "datosvariables")
        {
            data = System.IO.File.ReadAllText(@"C:\Users\Public\CarpetaPrueba\datosvariables.txt");
        }

        // Se lee el fichero datospersona.txt
        else if (file == "recuerdo")
        {
            data = System.IO.File.ReadAllText(@"C:\Users\Public\CarpetaPrueba\recuerdo.txt");
        }
        */
        
        string data = System.IO.File.ReadAllText(@"C:\Users\Public\Unity\Potato\" + file + ".txt");


        var lineas = Regex.Split(data, LINE_SPLIT_RE); // Divide data.text en lineas usando caracteres LINE_SPLIT_RE

        if (lineas.Length <= 1) return list; // Comprueba si hay más de una línea

        var header = Regex.Split(lineas[0], SPLIT_RE); // Divide header (elemento 0)

        // Bucle a lo largo de cada linea
        for (var i = 1; i < lineas.Length; i++)
        {
            Debug.Log("Lines length:" + lineas.Length);
            var valores = Regex.Split(lineas[i], SPLIT_RE); // Divide líneas de acuerdo con SPLIT_RE, guardandolo en valores
            if (valores.Length == 0 || valores[0] == "") continue; // Pasa al final del loop si el valor tiene longitud 0 o si el primer valor está vacío

            var entrada = new Dictionary<string, object>(); // Crea un objeto diccionario

            // Loop en cada valor
            for (var j = 0; j < header.Length && j < valores.Length; j++)
            {
                Debug.Log("Header length:" + header.Length);
                Debug.Log("Values length:" + valores.Length);
                string valor = valores[j]; // Almacena en variable local
                valor = valor.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object valorfinal = valor; // Guarda el valor final
                
                int n; // Create int, to hold value if int

                float f; // Create float, to hold value if float

                // Intenta convertir en int o float
                if (int.TryParse(valor, out n))
                {
                    valorfinal = n;
                }
                else if (float.TryParse(valor, out f))
                {
                    valorfinal = f;
                }
                Debug.Log("Dato leído:" + valorfinal);
                entrada[header[j]] = valorfinal;
            }
            list.Add(entrada); // Añade diccionario a la lista
        }
        return list; //Return list
    }
}