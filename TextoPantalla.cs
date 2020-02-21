using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoPantalla : MonoBehaviour {

    // La última página del texto
    public int ultimaPagina = 6;

    // La página en la que se encuentra
    private int pagina;

    // True cuando se produce un cambio de página
    private bool banderaCambioPagina;

    // Referencia a Text de TextoPantalla
    private Text text;

	void Start () {

        // Referencia a Text de TextoPantalla
        text = GetComponent<Text>();

        // En el inicio se escribe la primera página
        text.text = "Instrucciones de uso";
        text.fontSize = 8;

        // Inicialización variables
        pagina = 1;
        banderaCambioPagina = false;

	}
	
	void Update () {
		
        // Si se detecta un cambio de página
        if(banderaCambioPagina)
        {
            Debug.Log("BanderaCambioPagina");
            switch (pagina)
            {
                case 1:
                    text.text = "Instrucciones de uso";
                    text.fontSize = 8;
                    break;
                case 2:
                    //**HAY QUE COMPROBAR QUE LOS SALTOS DE LÍNEA FUNCIONEN
                    text.text = "Hay dos modos de funcionamiento:\r\n\r\nModo 2D\r\n\r\nModo 3D"; 
                    text.fontSize = 7;
                    break;
                case 3:
                    text.text = "Modo 2D";
                    text.fontSize = 12;
                    break;
                case 4:
                    text.text = "1 - Compruebe que la opción Modo 3D, que se encuentra arriba a su derecha, está desactivada. En caso contrario, desactívela con el botón Trigger";
                    text.fontSize = 5;
                    break;
                case 5:
                    text.text = "2 - Pulse los botones de las variables que desea representar con el botón Trigger, siendo el tiempo la variable correspondiente al eje X y la variable seleccionada la correspondiente al eje Y.Como máximo se pueden representar 3 variables simultáneamente";
                    text.fontSize = 5;
                    break;
                case 6:
                    text.text = "3 - Pulse el botón Crear Gráfico situado arriba a su izquierda";
                    text.fontSize = 6;
                    break;
                case 7:
                    text.text = "4- Dé la vuelta y observe los resultados";
                    text.fontSize = 6;
                    break;
                case 8:
                    text.text = "Modo 3D";
                    text.fontSize = 12;
                    break;
                case 9:
                    text.text = "1 - Compruebe que la opción Modo 3D, que se encuentra arriba a su derecha, está activada. En caso contrario, actívela con el botón Trigger";
                    text.fontSize = 5;
                    break;
                case 10:
                    text.text = "2 - Pulse los botones de las variables que desea representar con el botón Trigger. En este modo se deben pulsar 3 botones";
                    text.fontSize = 5;
                    break;
                case 11:
                    text.text = "3 - Pulse el botón Crear Gráfico situado arriba a su izquierda";
                    text.fontSize = 6;
                    break;
                case 12:
                    text.text = "4- Dé la vuelta y observe los resultados";
                    text.fontSize = 6;
                    break;
                case 13:
                    text.text = "¡Ya está listo para comenzar!";
                    text.fontSize = 6;
                    break;
                default:
                    text.text = "Se ha producido un error";
                    text.fontSize = 8;
                    break;
            } 

            // Se evita que se reescriba continuamente en text.text
            banderaCambioPagina = false;
        }

	}

    // Se llama desde BotonAbajo para seguir leyendo texto
    public void SiguienteTexto()
    {
        if (pagina != ultimaPagina)
        {
            pagina++;
            banderaCambioPagina = true;
        }
    }

    // Se llama desde BotonArriba para seguir leyendo texto
    public void AnteriorTexto()
    {
        if(pagina != 1)
        {
            pagina--;
            banderaCambioPagina = true;
        }
    }
}
