using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(menuName = "Preguntas/New Pregunta")]
public class Preguntas : ScriptableObject{

    public string Nombre;
    public string Descripcion;
    public Respuesta Respuestas;
    public Sprite sprite;
    public Sprite fondo;
}
