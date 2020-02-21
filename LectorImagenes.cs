using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LectorImagenes : MonoBehaviour {

    // Dónde se carga la imagen
    public RawImage foto;

    // Para leer los archivos de las imágenes
    private byte[] datosArchivo;

	void Start () {

        // Lectura foto de la carpeta "Fotos" dentro de "Resources"
        //foto.texture = (Texture)Resources.Load<Texture>("Fotos/" + imagenALeer) as Texture;
        
    }
	
	void Update () {
		
	}

    // Se llama para leer una nueva imagen con el nombre adecuado
    // El primer argumento es la carpeta y el segundo argumento el nombre de la imagen
    public void LeerImagen(string carpeta, string imagen)
    {
        datosArchivo = System.IO.File.ReadAllBytes(@"C:\Users\Public\Unity\Potato\" + "Fotos\\" + carpeta + "\\" + imagen + ".jpg");

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(datosArchivo);
        foto.texture = tex;

        //foto.texture = (Texture)Resources.Load<Texture>("Fotos\\" + carpeta + "\\" + imagen) as Texture;
    }
}
