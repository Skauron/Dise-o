using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using MySql.Data.MySqlClient;
using System.Data;

public class CambiarEscena : MonoBehaviour {

    public TMP_InputField InputField;
    public CodigoEstudiante codigoEstudiante;
    public adminMYSQL admin;

    private MySqlDataReader reader;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void iniciarExp(){
        if (!string.IsNullOrEmpty(InputField.text)){
            codigoEstudiante.codigo = InputField.text;
            reader = admin.Select(codigoEstudiante.codigo);
            if (reader.HasRows)
            {
                DataTable tabla = new DataTable();
                tabla.Load(reader);
                DataRow row = tabla.Rows[0];
                int id = int.Parse(row["id"].ToString());
                codigoEstudiante.idEstudiante = id;
                DontDestroyOnLoad(codigoEstudiante);
                SceneManager.LoadScene("Juego");
            }else{
                Debug.Log("Ese niño no existe so");
            }
            reader.Close();
        }
        else{
            Debug.Log("El campo esta vacio");
        }
    }
}
