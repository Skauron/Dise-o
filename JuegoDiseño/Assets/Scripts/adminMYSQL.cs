 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.Common;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

public class adminMYSQL : MonoBehaviour {

    public string servidorBaseDatos;
    public string nombreBaseDatos;
    public string usuarioBaseDatos;
    public string contraseñaBaseDatos;

    private string datosConexion;
    private MySqlConnection conexion;

    // Use this for initialization
    void Start () {
		datosConexion = "Server=" + servidorBaseDatos + ";Database=" + nombreBaseDatos + ";Uid=" +usuarioBaseDatos
            + ";Pwd=" + contraseñaBaseDatos + ";";
        ConectarServidorBaseDatos();
    }
	
	public void ConectarServidorBaseDatos()
    {
        conexion = new MySqlConnection(datosConexion);
        try
        {
            conexion.Open();
            Debug.Log("Todo bien, me conecte!");
        }
        catch(MySqlException error )
        {
            Debug.LogError("Mala prro el error es este: "+error);
        }
    }

    public MySqlDataReader Select(string select)
    {
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM estudiantes WHERE estudiantes.nombre='"+select+"'";
        MySqlDataReader resultado = cmd.ExecuteReader();
        return resultado;
    }

    public Preguntas[] SelectPreguntas()
    {
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM preguntamts";
        MySqlDataReader resultado = cmd.ExecuteReader();
        DataTable tabla = new DataTable();
        tabla.Load(resultado);

        Preguntas[] pregunta = new Preguntas[tabla.Rows.Count];

        for (int i = 0; i < tabla.Rows.Count; i++)
        {
            DataRow row = tabla.Rows[i];
            int id = int.Parse(row["id"].ToString());
            int subTemaId = int.Parse(row["sub_tema_id"].ToString());
            string nombre = row["nombre"].ToString();
            string descripcion = row["descripcion"].ToString();
            string pregunta_1 = row["pregunta_1"].ToString();
            string pregunta_2 = row["pregunta_2"].ToString();
            string pregunta_3 = row["pregunta_3"].ToString();
            //string pregunta_4 = row["pregunta_4"].ToString();
            int respuesta = int.Parse(row["respuesta"].ToString());
            int fondo = int.Parse(row["fondo_id"].ToString());
            int sprite = int.Parse(row["icono_id"].ToString());

            pregunta[i] = new Preguntas(id,subTemaId,nombre, descripcion, pregunta_1, pregunta_2, pregunta_3, respuesta, fondo, sprite);
        }
        
        resultado.Close();
        return pregunta;
    }

    public void InsertCalifacion(int evaluacionID,int preguntaID,int estudianteID, int nota, int tiempo) {
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "INSERT INTO calificacions (evaluacion_id,preguntamt_id,estudiante_id,nota,tiempo) VALUES("+evaluacionID+","+ preguntaID +","+ estudianteID +"," + nota +","+tiempo+ ");";
        cmd.ExecuteNonQuery(); 
    }
}
