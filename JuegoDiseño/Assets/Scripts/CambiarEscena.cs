using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CambiarEscena : MonoBehaviour {

    TMP_InputField InputField;
    CodigoEstudiante CodigoEstudiante;

	// Use this for initialization
	void Start () {
        CodigoEstudiante = GameObject.Find("CodigoEstudiante").transform.GetComponent<CodigoEstudiante>();
        InputField = GameObject.Find("Input").transform.GetComponent<TMP_InputField>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void iniciarExp(){
        if (!string.IsNullOrEmpty(InputField.text)){
            CodigoEstudiante.codigo = InputField.text;
            DontDestroyOnLoad(CodigoEstudiante);
            SceneManager.LoadScene("Juego");
        }
        else {
            Debug.Log("Malisima");
        }
    }
}
