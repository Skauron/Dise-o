using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class SistemaDePreguntas : MonoBehaviour
{

    private int eleccion,preguntaRandom;
    private bool seleccionado;
    private float Timer;

    private GameObject Destino1, Destino2, Destino3;
    private GameObject opcion1, opcion2, opcion3;
    private GameObject[] ListaDestinos;

    private TextMeshProUGUI txPregunta,txTiempo;

    private Vector2 posicionInicial;

    private SpriteRenderer[] ListaSprites;

    private CodigoEstudiante Estudiante;

    private Preguntas objPreguntas;
    private ListaPreguntas preguntas;
    public ListaResultados resultados;
    private Resultados resultado;

    // Use this for initialization
    void Start()
    {
        objPreguntas = new Preguntas();
        resultado = new Resultados();
        Estudiante = GameObject.Find("CodigoEstudiante").transform.GetComponent<CodigoEstudiante>();
        preguntas = objPreguntas.leerJsonPreguntas();
        txPregunta = GameObject.Find("Pregunta").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        txTiempo = GameObject.Find("Tiempo").transform.GetComponent<TextMeshProUGUI>();
        Destino1 = GameObject.Find("DestinoMax1");
        Destino2 = GameObject.Find("DestinoMax2");
        Destino3 = GameObject.Find("DestinoMax3");
        opcion1 = GameObject.Find("SpriteOpcion1");
        opcion2 = GameObject.Find("SpriteOpcion2");
        opcion3 = GameObject.Find("SpriteOpcion3");
        posicionInicial = opcion1.transform.position;

        ListaSprites = new SpriteRenderer[] { opcion1.transform.GetComponent<SpriteRenderer>(), opcion2.transform.GetComponent<SpriteRenderer>(), opcion3.transform.GetComponent<SpriteRenderer>() };
        ListaDestinos = new GameObject[] { Destino1, Destino2, Destino3 };

        SetterInicio();
    }

    // Update is called once per frame
    void Update()
    {
        switch (eleccion)
        {
            case 1:
                IrAlObjetivo();
                break;
            case 2:
                BajarOpcionesReiniciar();
                break;
            case 3:
                SetterInicio();
                break;
        }
        if (opcion1.transform.position.Equals(Destino1.transform.position)){
            Timer += Time.deltaTime;
            txTiempo.text = ((int)Timer).ToString();
        }
    }

    private void SetterInicio()
    {
        txTiempo.text = "0";
        seleccionado = false;
        eleccion = 1;
        preguntaRandom = Random.Range(0,preguntas.preguntas.Count);
        txPregunta.text = preguntas.preguntas[preguntaRandom].Descripcion;
        foreach (SpriteRenderer ListaSprites in ListaSprites)
        {
            ListaSprites.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        }
        for (int i = 0; i < preguntas.preguntas[preguntaRandom].Respuestas.Length; i++)
        {
            ListaSprites[i].GetComponentInChildren<TextMeshProUGUI>().text = preguntas.preguntas[preguntaRandom].Respuestas[i].Solucion.ToString();
        }
    }

    private void IrAlObjetivo()
    {
        for (int i = 0; i < ListaSprites.Length; i++)
        {
            ListaSprites[i].transform.position = Vector2.MoveTowards(ListaSprites[i].transform.position, ListaDestinos[i].transform.position, 200f * Time.deltaTime);
        }
        if (opcion1.transform.position.Equals(Destino1.transform.position) && seleccionado)
        {
            eleccion = 2;
            seleccionado = false;
        }
    }

    public void VerificarPregunta(int posicion)
    {
        bool EsCorrecto;
        if (preguntas.preguntas[preguntaRandom].Respuestas[posicion].EsCorrecto){
            seleccionado = true;
            EsCorrecto = true;
            Debug.Log("Buena");
        }
        else{
            seleccionado = true;
            EsCorrecto = false;
            Debug.Log("Mala");
        }
        GuardarDatos(Estudiante.codigo, preguntaRandom, EsCorrecto,Timer);
    }

    public void BajarOpcionesReiniciar(){
        for (int i = 0; i < ListaSprites.Length; i++)
        {
            ListaSprites[i].transform.GetComponent<Rigidbody2D>().gravityScale = 20f;
            Timer = 0;
        }
        if (opcion1.transform.position.y <= posicionInicial.y)
        {
            for (int i = 0; i < ListaSprites.Length; i++)
            {
                ListaSprites[i].transform.GetComponent<Rigidbody2D>().gravityScale = 0f;
                ListaSprites[i].transform.position = Vector2.MoveTowards(ListaSprites[i].transform.position, ListaDestinos[i].transform.position, 200f * Time.deltaTime);
                if (opcion1.transform.position.y > posicionInicial.y)
                {
                    eleccion = 3;
                }
            }
        }
    }

    public void GuardarDatos(string estudiante,int pregunta,bool correcta,float tiempo){
        resultados.resultados[pregunta].pregunta = pregunta;
        resultados.resultados[pregunta].Tiempo = tiempo;
        resultados.resultados[pregunta].EsCorrecto = correcta;
        resultado.guardarJsonEditado("/StreamingAssets/Estudiantes/" + estudiante + ".json", resultados);
    }
}
