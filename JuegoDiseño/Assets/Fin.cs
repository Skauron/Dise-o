using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Fin : MonoBehaviour {

    public GameObject estudiante;
    public puntajeFinal puntaje;

    public TextMeshProUGUI puntajetext;

    void Start()
    {
        estudiante = GameObject.Find("CodigoEstudiante");
        puntaje = GameObject.Find("Puntaje").GetComponent<puntajeFinal>();
        puntajetext.text = puntaje.puntaje.ToString();
        Destroy(puntaje);
        Destroy(estudiante);
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Inicio");
    }
}
