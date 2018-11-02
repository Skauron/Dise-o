using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(menuName = "Preguntas/New Pregunta")]
public class Preguntas : ScriptableObject{

    public int id;
    public int subTemaId;
    public string nombre;
    public string descripcion;
    public string pregunta_0;
    public string pregunta_1;
    public string pregunta_2;
    //public string pregunta_3;
    public int respuesta;
    public int fondo;
    public int sprite;

    public Preguntas(int Id,int SubTemaId, string Nombre, string Descripcion, string Pregunta_0, string Pregunta_1, string Pregunta_2, int Respuesta, int Fondo, int Sprite)
    {
        this.id = Id;
        this.subTemaId = SubTemaId;
        this.nombre = Nombre;
        this.descripcion = Descripcion;
        this.pregunta_0 = Pregunta_0;
        this.pregunta_1 = Pregunta_1;
        this.pregunta_2 = Pregunta_2;
        // this.pregunta_3 = Pregunta_3;
        this.respuesta = Respuesta;
        this.fondo = Fondo;
        this.sprite = Sprite;
    }
}
