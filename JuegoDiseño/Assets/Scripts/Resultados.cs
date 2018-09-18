using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Resultados{
    public int pregunta;
    public float Tiempo;
    public bool EsCorrecto;

    public void guardarJsonEditado(string ruta, ListaResultados Resultados){
        string jsonString = JsonUtility.ToJson(Resultados);
        string filePath = Application.dataPath + ruta;
        File.WriteAllText(filePath, jsonString);
    }
}

[System.Serializable]
public class ListaResultados{
    public List<Resultados>resultados;
}
