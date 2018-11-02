using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MySql.Data.MySqlClient;

public class SistemaDePreguntas : MonoBehaviour
{
    [Header("SQL")]
    public adminMYSQL admin;
    public CodigoEstudiante estudiante;
    public puntajeFinal puntaje;
    [Space(10)]

    [Header("UI")]
    public EventSystem system;
    public TextMeshProUGUI txPregunta;
    public TextMeshProUGUI txTiempo;
    public Image fondo;
    public Sprite[] ListaSpritesJuego;
    public float Timer;
    [Space(10)]

    [Header("Destinos y sprites")]
    public GameObject[] ListaDestinos;
    public GameObject[] ListaOrigen;
    public Image[] ListaSprite;
    public Sprite[] ListaFondos;
    [Space(10)]

    [Header("Preguntas")]
    public Preguntas[] preguntas;
    public Sprite[] retroalimentacion;
    public Image ImagenRetro;
    [Space(5)]

    private Preguntas AuxPregunta;
    private int randomInt;
    public faseJuego fase;
    public List<Preguntas> ListaPreguntas;

    //Private
    private MySqlDataReader reader;

    // Use this for initialization
    void Start()
    {
        estudiante = GameObject.Find("CodigoEstudiante").GetComponent<CodigoEstudiante>();
        ConsultarPreguntas();
        ListaPreguntas = new List<Preguntas>(preguntas.Length);
        foreach (Preguntas pregunta in preguntas){
            ListaPreguntas.Add(pregunta);
        }
        randomInt = Random.Range(0, ListaPreguntas.Count);
        AuxPregunta = ListaPreguntas[randomInt];
        ListaPreguntas.Remove(ListaPreguntas[randomInt]);
        fondo.sprite = ListaFondos[AuxPregunta.fondo - 1];
        txPregunta.text = AuxPregunta.descripcion;
        for (int i = 0; i < 3; i++)
        {
            ListaSprite[i].sprite = ListaSpritesJuego[AuxPregunta.sprite - 1];
        }
        ListaSprite[0].GetComponentInChildren<Text>().text = AuxPregunta.pregunta_0;
        ListaSprite[1].GetComponentInChildren<Text>().text = AuxPregunta.pregunta_1;
        ListaSprite[2].GetComponentInChildren<Text>().text = AuxPregunta.pregunta_2;
        //ListaSprite[3].GetComponentInChildren<Text>().text = AuxPregunta.pregunta_3;

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

    public void ConsultarPreguntas()
    {
        preguntas = admin.SelectPreguntas();
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
        fondo.sprite = ListaFondos[AuxPregunta.fondo - 1];
        txPregunta.text = AuxPregunta.descripcion;
        for (int i = 0; i < 3; i++)
        {
            ListaSprite[i].sprite = ListaSpritesJuego[AuxPregunta.sprite - 1];
        }
        ListaSprite[0].GetComponentInChildren<Text>().text = AuxPregunta.pregunta_0;
        ListaSprite[1].GetComponentInChildren<Text>().text = AuxPregunta.pregunta_1;
        ListaSprite[2].GetComponentInChildren<Text>().text = AuxPregunta.pregunta_2;
        // ListaSprite[3].GetComponentInChildren<Text>().text = AuxPregunta.pregunta_3.ToString();

        system.SetSelectedGameObject(null);
        fase = faseJuego.Objetivo;
    }

    private void AcabarJuego(){
        fase = faseJuego.Acabo;
        Debug.Log("Se acabo esta vuelta");
        SceneManager.LoadScene("Fin");
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
        StopAllCoroutines();
        if (AuxPregunta.respuesta == posicion)
        {
            StartCoroutine(retroalimentacionPregunta(retroalimentacion[0]));
            admin.InsertCalifacion(AuxPregunta.subTemaId, AuxPregunta.id, estudiante.idEstudiante, 1, Mathf.RoundToInt(Timer));
            puntaje.puntaje += 10;
            Debug.Log("Buena");
        }
        else
        {
            StartCoroutine(retroalimentacionPregunta(retroalimentacion[1]));
            admin.InsertCalifacion(AuxPregunta.subTemaId, AuxPregunta.id, estudiante.idEstudiante, 0, Mathf.RoundToInt(Timer));
            Debug.Log("Mala");
        }
        fase = faseJuego.CambiarPregunta;
    }

    IEnumerator retroalimentacionPregunta(Sprite imagen)
    {
        ImagenRetro.gameObject.SetActive(true);
        ImagenRetro.sprite = imagen;
        yield return new WaitForSeconds(1.5f);
        ImagenRetro.gameObject.SetActive(false);
    }

    public enum faseJuego{
        Objetivo,Espera, NuevaPregunta, CambiarPregunta, Acabo
    }
}
