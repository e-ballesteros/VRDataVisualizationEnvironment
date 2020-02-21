using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrazadorGraficos : MonoBehaviour {

    // Objeto que contiene los prefabs instanciados
    public GameObject SoportePuntos;

    // Prefab para instanciar los puntos
    public GameObject PrefabPunto;

    // Prefab para instanciar la línea
    public GameObject PrefabLinea;

    // Prefab para instanciar los conos
    public GameObject PrefabConoEje;

    // Bandera que se pone a true cuando se va a representar el primer punto
    private bool banderaPrimerPunto;

    // Nombre del archivo input, sin extension
    public string archivoInput;

    // List para almacenamiento de datos del lector CSV
    private List<Dictionary<string, object>> listaPuntos;

    // En él se almacenan los puntos instanciados. Se instancia en Start y se destruye y vuelve a instanciar en TrazarGrafico()
    private GameObject soportePuntos;

    // Instancia de PrefabLinea
    private GameObject lineaGrafico;

    // Instancia de PrefabLinea para los ejes en modo 3D
    private GameObject lineaGraficoX;
    private GameObject lineaGraficoY;
    private GameObject lineaGraficoZ;

    // Instancia de ConoEje para los ejes en modo 3D
    private GameObject conoEjeX;
    private GameObject conoEjeY;
    private GameObject conoEjeZ;


    // Media para los colores
    private float pesoX;
    private float pesoY;
    private float pesoZ;

    // Para activar el modo 2D. En el modo 2D, la variable X es siempre el tiempo
    private bool modo2D = true;

    // Indices de las columnas asignadas. Aquí se introducen las columnas del CSV que se quieran representar
    //** Desde los botones se podrá cambiar los valores de cada columna
    public int columnaX = 1;
    public int columnaY = 2;
    public int columnaZ = 3;

    // Nombres de las columnas 
    //**Posiblemente sólo necesite dos coordenadas
    public string nombreX;
    public string nombreY;
    public string nombreZ;

    // Para variar la escala del gráfico
    public float escalaGrafico = 10;

    // El tamaño del inicio y el final de la linea de gráfico
    public float inicioLineaGrafico;
    public float finalLineaGrafico;


    // Para establecer límites en cada eje. A todos los puntos se les resta xInicial/escalaGrafico y se les suma yInicial/escalaGrafico
    public float xInicial = 0;
    public float yInicial = 0;
    public float zInicial = 0;

    // Definimos los colores de todos los gráficos aquí, en sintonía con los colores de los pulsadores en la escena
    private Color color1 = new Color(0, 0, 1, 1.0f);
    private Color color2 = new Color(0, 1, 0, 1.0f);
    private Color color3 = new Color(1, 0, 0, 1.0f);
    private Color color4 = new Color(0.5f, 0, 1, 1.0f);
    private Color color5 = new Color(0, 1, 1, 1.0f);
    private Color color6 = new Color(1, 0, 1, 1.0f);
    private Color color7 = new Color(1, 1, 0, 1.0f);
    private Color color8 = new Color(1, 0.5f, 0, 1.0f);
    private Color color9 = new Color(0.5f, 0.4f, 0.2f, 1.0f);
    private Color color10 = new Color(1, 0.5f, 1, 1.0f);
    private Color color11 = new Color(1, 1, 0.5f, 1.0f);
    private Color color12 = new Color(1, 0.5f, 0.5f, 1.0f);
    private Color color13 = new Color(0.2f, 0.5f, 0.2f, 1.0f);


    private Color colorEstandarPunto = new Color(0.2f, 1, 0.4f, 1.0f);

    // Para guardar los valores del punto
    //private Vector3 valores;

    void Start()
    {
        // Se inicializa a true al no haberse representado el primer punto aún
        banderaPrimerPunto = true;
    }

    private void Update()
    {

    }

    // Función para trazar el gráfico, tanto 2D como 3D
    public void TrazarGrafico()
    {

        
        // Se destruye el anterior gráfico, destruyendo soportePuntos y lineRenderer, y se vuelve a instanciar
        //Destroy(soportePuntos);
        soportePuntos = Instantiate(
            SoportePuntos,
            new Vector3(0, 0, 0),
            Quaternion.identity);

        // Se destruye la anterior línea
        //Destroy(lineaGrafico);
        lineaGrafico = Instantiate(
            PrefabLinea,
            new Vector3(0, 0, 0),
            Quaternion.identity);
        

        // Se accede al Component LineRenderer de lineaGrafico para cambiar valores de ancho de inicio y final
        lineaGrafico.GetComponent<LineRenderer>().startWidth = inicioLineaGrafico;
        lineaGrafico.GetComponent<LineRenderer>().endWidth = finalLineaGrafico;

        // Se establece el color de lineaGrafico
        lineaGrafico.GetComponent<LineRenderer>().startColor = DimeColor(columnaY);
        lineaGrafico.GetComponent<LineRenderer>().endColor = DimeColor(columnaY);

        // Resultado funcion leer del LectorCSV en listaPuntos con argumento archivoInput. Se manda false porque se quiere leer números
        listaPuntos = LectorCSV.Leer(archivoInput);

        // Lista de cadenas, con las keys (nombres de las columnas)
        List<string> listaColumnas = new List<string>(listaPuntos[1].Keys);

        // Asignacion nombres columnas desde listaColumnas a variables nombre
        nombreX = listaColumnas[columnaX];
        nombreY = listaColumnas[columnaY];
        nombreZ = listaColumnas[columnaZ];

        // Obtención máximos en cada eje
        float xMax = EncontrarValorMaximo(nombreX);
        float yMax = EncontrarValorMaximo(nombreY);
        float zMax = EncontrarValorMaximo(nombreZ);

        // Obtención de mínimos en cada eje
        float xMin = EncontrarValorMinimo(nombreX);
        float yMin = EncontrarValorMinimo(nombreY);
        float zMin = EncontrarValorMinimo(nombreZ);

        float x;
        float y;
        float z;

        // El número de vértices del lineRenderer es el número de puntos en listaPuntos
        lineaGrafico.GetComponent<LineRenderer>().positionCount = listaPuntos.Count;

        // Loop en listaPuntos
        for (var i = 0; i < listaPuntos.Count; i++) // listaPuntos.Count devuelve el número de filas en la lista
        {
            // Obtener valor en listaPuntos en la iesima fila de la columna con nombre determinado, normalizado
            if (xMax != xMin)
            {
                x =
                (System.Convert.ToSingle(listaPuntos[i][nombreX]) - xMin) / (xMax - xMin) + xInicial / escalaGrafico;
            }

            else
            {
                x = System.Convert.ToSingle(listaPuntos[i][nombreX]) + xInicial / escalaGrafico;
            }

            if (yMax != yMin)
            {
                y =
                (System.Convert.ToSingle(listaPuntos[i][nombreY]) - yMin) / (yMax - yMin) + yInicial / escalaGrafico;
            }

            else
            {
                y = System.Convert.ToSingle(listaPuntos[i][nombreY]) + yInicial / escalaGrafico;
            }


            // Si el modo 2D está desactivado se da valor a z de la forma habitual
            if (modo2D == false)
            {
                if (zMax != zMin)
                {
                    z =
                    (System.Convert.ToSingle(listaPuntos[i][nombreZ]) - zMin) / (zMax - zMin) + zInicial / escalaGrafico;
                }

                else
                {
                    z = System.Convert.ToSingle(listaPuntos[i][nombreZ]) + zInicial / escalaGrafico;
                }
            }

            // Si el modo 2D está activado todos los puntos tienen el mismo valor en z
            else
            {
                // Se multiplica el valor por escalaGrafico al instanciar posteriormente
                z = zInicial/escalaGrafico;

                // Se añade punto al lineRenderer
                lineaGrafico.GetComponent<LineRenderer>().SetPosition(i, new Vector3(x, y, z) * escalaGrafico);

            }

            // Se instancia un PrefabPunto
            GameObject puntoDatos = Instantiate(
                PrefabPunto,
                new Vector3(x, y, z) * escalaGrafico,
                Quaternion.identity);

            EsferaDatos esferaDatos = puntoDatos.GetComponent<EsferaDatos>(); 

            // Se hace puntoDatos derivada de SoportePuntos
            puntoDatos.transform.parent = soportePuntos.transform;

            // Asignacion valor a nombrePuntoDatos
            string nombrePuntoDatos =
            listaPuntos[i][nombreX] + " "
            + listaPuntos[i][nombreY] + " "
            + listaPuntos[i][nombreZ];

            // Se asigna nombre coherente al prefab
            puntoDatos.transform.name = nombrePuntoDatos;

            // Se guardan los valores del punto
            Vector3 valores = new Vector3(
                System.Convert.ToSingle(listaPuntos[i][nombreX]),
                System.Convert.ToSingle(listaPuntos[i][nombreY]),
                System.Convert.ToSingle(listaPuntos[i][nombreZ])
                );
            esferaDatos.GuardarValores(valores);
            esferaDatos.GuardarVariables(nombreX, nombreY, nombreZ);

            // Define un color RGBA para el punto
            if (modo2D == true)
            {

                // En modo 2D los puntos tienen el color de la línea
                puntoDatos.GetComponent<Renderer>().material.color = DimeColor(columnaY);

            }

            else
            {
                // Interpolación de colores para cada esfera
                pesoX = x / (x + y + z);
                pesoY = y / (x + y + z);
                pesoZ = z / (x + y + z);


                puntoDatos.GetComponent<Renderer>().material.color =
                new Color(
                    DimeColor(columnaX).r * pesoX + DimeColor(columnaY).r * pesoY + DimeColor(columnaZ).r * pesoZ,
                    DimeColor(columnaX).g * pesoX + DimeColor(columnaY).g * pesoY + DimeColor(columnaZ).g * pesoZ,
                    DimeColor(columnaX).b * pesoX + DimeColor(columnaY).b * pesoY + DimeColor(columnaZ).b * pesoZ,
                    1.0f);

            }

        }

        // Si se está en modo 3D, se deben instanciar los ejes y girar todo 90 grados
        if (modo2D == false)
        {
            InstanciaEjes();

            conoEjeX.transform.parent = lineaGraficoX.transform;
            conoEjeY.transform.parent = lineaGraficoY.transform;
            conoEjeZ.transform.parent = lineaGraficoZ.transform;

            // Se gira soportePuntos, los ejes y las flechas para que apunten de cara
            soportePuntos.transform.rotation = Quaternion.Euler(0, 90, 0);
            lineaGraficoX.transform.rotation = Quaternion.Euler(0, 90, 0);
            lineaGraficoY.transform.rotation = Quaternion.Euler(0, 90, 0);
            lineaGraficoZ.transform.rotation = Quaternion.Euler(0, 90, 0);

            // Se giran los conos para que apunten en la dirección adecuada
            conoEjeX.transform.rotation = Quaternion.Euler(-90, 0, 0);
            conoEjeY.transform.rotation = Quaternion.Euler(0, 0, 0);
            conoEjeZ.transform.rotation = Quaternion.Euler(0, 0, -90);

        }
    }

    private float EncontrarValorMaximo(string nombreColumna)
    {
        // Valor inicial es el primer valor
        float maxValor = Convert.ToSingle(listaPuntos[0][nombreColumna]);

        // Loop en diccionario, sobreescribiendo maxValor si el nuevo valor es superior
        for (var i = 0; i < listaPuntos.Count; i++)
        {
            if (maxValor < Convert.ToSingle(listaPuntos[i][nombreColumna]))
                maxValor = Convert.ToSingle(listaPuntos[i][nombreColumna]);
        }

        // Se devuelve valor máximo
        return maxValor;
    }

    private float EncontrarValorMinimo(string nombreColumna)
    {

        float minValor = Convert.ToSingle(listaPuntos[0][nombreColumna]);

        // Loop en diccionario, sobreescribiendo el minValor si el nuevo es más pequeño
        for (var i = 0; i < listaPuntos.Count; i++)
        {
            if (Convert.ToSingle(listaPuntos[i][nombreColumna]) < minValor)
                minValor = Convert.ToSingle(listaPuntos[i][nombreColumna]);
        }

        return minValor;
    }

    /*
    public void DestruirGrafico()
    {
        Destroy(soportePuntos);
        Destroy(lineaGrafico);
    }
    */

    public void CambiaModo2D()
    {
        modo2D = true;
    }
    
    public void CambiaModo3D()
    {
        modo2D = false;
    }

    // Devuelve el color que corresponde a cada variable
    private Color DimeColor(int variable)
    {
        switch (variable)
        {
            case 1: return color1;
            case 2: return color2;
            case 3: return color3;
            case 4: return color4;
            case 5: return color5;
            case 6: return color6;
            case 7: return color7;
            case 8: return color8;
            case 9: return color9;
            case 10: return color10;
            case 11: return color11;
            case 12: return color12;
            case 13: return color13;
            default: return color1;
        }
    }

    private void InstanciaEjes()
    {
        // Se instancian las líneas de los tres ejes
        lineaGraficoX = Instantiate(
    PrefabLinea,
    new Vector3(0, 0, 0),
    Quaternion.identity);

        lineaGraficoY = Instantiate(
    PrefabLinea,
    new Vector3(0, 0, 0),
    Quaternion.identity);

        lineaGraficoZ = Instantiate(
    PrefabLinea,
    new Vector3(0, 0, 0),
    Quaternion.identity);

        // Se establecen las anchuras de los tres ejes
        lineaGraficoX.GetComponent<LineRenderer>().startWidth = inicioLineaGrafico;
        lineaGraficoX.GetComponent<LineRenderer>().endWidth = finalLineaGrafico;

        lineaGraficoY.GetComponent<LineRenderer>().startWidth = inicioLineaGrafico;
        lineaGraficoY.GetComponent<LineRenderer>().endWidth = finalLineaGrafico;

        lineaGraficoZ.GetComponent<LineRenderer>().startWidth = inicioLineaGrafico;
        lineaGraficoZ.GetComponent<LineRenderer>().endWidth = finalLineaGrafico;

        // Se establece los colores de los tres ejes
        lineaGraficoX.GetComponent<LineRenderer>().startColor = DimeColor(columnaX);
        lineaGraficoX.GetComponent<LineRenderer>().endColor = DimeColor(columnaX);

        lineaGraficoY.GetComponent<LineRenderer>().startColor = DimeColor(columnaY);
        lineaGraficoY.GetComponent<LineRenderer>().endColor = DimeColor(columnaY);

        lineaGraficoZ.GetComponent<LineRenderer>().startColor = DimeColor(columnaZ);
        lineaGraficoZ.GetComponent<LineRenderer>().endColor = DimeColor(columnaZ);

        // Se establecen cuantos puntos debe recorrer la línea (es recta, sólo 2)
        lineaGraficoX.GetComponent<LineRenderer>().positionCount = 2;
        lineaGraficoY.GetComponent<LineRenderer>().positionCount = 2;
        lineaGraficoZ.GetComponent<LineRenderer>().positionCount = 2;

        // Se establece el inicio de las tres líneas, que es en el origen
        lineaGraficoX.GetComponent<LineRenderer>().SetPosition(0, new Vector3(xInicial / escalaGrafico,
             yInicial / escalaGrafico,
             zInicial / escalaGrafico));
        lineaGraficoY.GetComponent<LineRenderer>().SetPosition(0, new Vector3(xInicial / escalaGrafico,
             yInicial / escalaGrafico,
             zInicial / escalaGrafico));
        lineaGraficoZ.GetComponent<LineRenderer>().SetPosition(0, new Vector3(xInicial / escalaGrafico,
             yInicial / escalaGrafico,
             zInicial / escalaGrafico));

        // Se establece el final de las tres líneas
        lineaGraficoX.GetComponent<LineRenderer>().SetPosition(1, new Vector3(1 + xInicial / escalaGrafico,
            yInicial / escalaGrafico,
            zInicial / escalaGrafico) * escalaGrafico);
        lineaGraficoY.GetComponent<LineRenderer>().SetPosition(1, new Vector3(xInicial / escalaGrafico,
            1 + yInicial / escalaGrafico,
            zInicial / escalaGrafico) * escalaGrafico);
        lineaGraficoZ.GetComponent<LineRenderer>().SetPosition(1, new Vector3(xInicial / escalaGrafico,
            yInicial / escalaGrafico,
            1 + zInicial / escalaGrafico) * escalaGrafico);

        
        // Los tres conos finales de los ejes
        conoEjeX = Instantiate(
            PrefabConoEje,
            new Vector3(1 + xInicial / escalaGrafico,
            yInicial / escalaGrafico,
            zInicial / escalaGrafico) * escalaGrafico,
            Quaternion.identity);

        conoEjeY = Instantiate(
            PrefabConoEje,
            new Vector3(xInicial / escalaGrafico,
            1 + yInicial / escalaGrafico,
            zInicial / escalaGrafico) * escalaGrafico,
            Quaternion.identity);

        conoEjeZ = Instantiate(
            PrefabConoEje,
            new Vector3(xInicial / escalaGrafico,
            yInicial / escalaGrafico,
            1 + zInicial / escalaGrafico) * escalaGrafico,
            Quaternion.identity);

    }


}
