using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SistemaDePreguntas : MonoBehaviour
{
    [Header("UI")]
    public EventSystem system;
    public TextMeshProUGUI txPregunta;
    public TextMeshProUGUI txTiempo;
    public Image fondo;
    public float Timer;
    [Space(10)]

    [Header("Destinos y sprites")]
    public GameObject[] ListaDestinos;
    public GameObject[] ListaOrigen;
    public Image[] ListaSprite;
    [Space(10)]

    [Header("Preguntas")]
    public Preguntas[] preguntas;
    [Space(5)]

    private Preguntas AuxPregunta;
    private int randomInt;
    public faseJuego fase;
    public List<Preguntas> ListaPreguntas;
    //public CodigoEstudiante Estudiante;

    // Use this for initialization
    void Start()
    {
        ListaPreguntas = new List<Preguntas>(preguntas.Length);
        foreach (Preguntas pregunta in preguntas){
            ListaPreguntas.Add(pregunta);
        }
        randomInt = Random.Range(0, ListaPreguntas.Count);
        AuxPregunta = ListaPreguntas[randomInt];
        ListaPreguntas.Remove(ListaPreguntas[randomInt]);
        fondo.sprite = AuxPregunta.fondo;
        txPregunta.text = AuxPregunta.Descripcion;
        for (int i = 0; i < 4; i++)
        {
            ListaSprite[i].sprite = AuxPregunta.sprite;
            ListaSprite[i].GetComponentInChildren<Text>().text = AuxPregunta.Respuestas.Solucion[i].ToString();
        }
        fase = faseJuego.Objetivo;
    }

    // Update is called once per frame
    void Update(){
        if (fase == faseJuego.NuevaPregunta)
        {
            NuevoPregunta();
        }
        if (fase == faseJuego.Objetivo)
        {
            IrAlObjetivo();
        }
        if (fase == faseJuego.CambiarPregunta)
        {
            IrAlOrigen();
        }
        if (fase == faseJuego.Espera)
        {
            Timer += Time.deltaTime;
        }
        txTiempo.text = ((int)Timer).ToString();
    }

    private void NuevoPregunta(){
        if (ListaPreguntas.Count == 0){
            AcabarJuego();
            return;
        }
        Timer = 0f;
        randomInt = Random.Range(0, ListaPreguntas.Count);
        AuxPregunta = ListaPreguntas[randomInt];
        ListaPreguntas.Remove(ListaPreguntas[randomInt]);
        fondo.sprite = AuxPregunta.fondo;
        txPregunta.text = AuxPregunta.Descripcion;
        for (int i = 0; i < 4; i++)
        {
            ListaSprite[i].sprite = AuxPregunta.sprite;
            ListaSprite[i].GetComponentInChildren<Text>().text = AuxPregunta.Respuestas.Solucion[i].ToString();
        }
        system.SetSelectedGameObject(null);
        fase = faseJuego.Objetivo;
    }

    private void AcabarJuego(){
        fase = faseJuego.Acabo;
        Debug.Log("Se acabo esta vuelta");
    }

    private void IrAlObjetivo()
    {
        for (int i = 0; i < 4; i++)
        {
            ListaSprite[i].transform.position = Vector2.MoveTowards(ListaSprite[i].transform.position, ListaDestinos[i].transform.position, 100f * Time.deltaTime);
            if (ListaSprite[i].transform.position.y >= ListaDestinos[i].transform.position.y)
            {
                fase = faseJuego.Espera;
            }
        }
    }

    private void IrAlOrigen()
    {
        for (int i = 0; i < 4; i++)
        {
            ListaSprite[i].transform.position = Vector2.MoveTowards(ListaSprite[i].transform.position, ListaOrigen[i].transform.position, 150f * Time.deltaTime);
            if (ListaSprite[i].transform.position.y <= ListaOrigen[i].transform.position.y)
            {
                fase = faseJuego.NuevaPregunta;
            }
        }
    }

    public void VerificarPregunta(int posicion) {
        if (AuxPregunta.Respuestas.EsCorrecto[posicion])
        {
            Debug.Log("Buena");
        }
        else
        {
            Debug.Log("Mala");
        }
        fase = faseJuego.CambiarPregunta;
    }

    public enum faseJuego{
        Objetivo,Espera, NuevaPregunta, CambiarPregunta, Acabo
    }
}
