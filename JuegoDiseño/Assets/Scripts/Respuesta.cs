using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Respuestas/New Respuesta")]
public class Respuesta : ScriptableObject{

    public int[] Solucion;
    public bool[] EsCorrecto;
}
