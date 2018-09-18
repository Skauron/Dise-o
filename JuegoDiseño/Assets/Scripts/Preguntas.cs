using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Preguntas{

    public string Nombre;
    public string Descripcion;
    public Respuesta[] Respuestas;

    public ListaPreguntas leerJsonPreguntas()
    {
        string filePath = Application.streamingAssetsPath + "/Preguntas.json";
        string jsonString = File.ReadAllText(filePath);
        ListaPreguntas preguntas = JsonUtility.FromJson<ListaPreguntas>(jsonString);
        return preguntas;
    }

    public void guardarJsonEditado(string ruta, Preguntas preguntas)
    {
        string jsonString = JsonUtility.ToJson(preguntas);
        string filePath = Application.dataPath + ruta;
        File.WriteAllText(filePath, jsonString);
    }
}

[System.Serializable]
public class ListaPreguntas{
    public List<Preguntas> preguntas;
}
