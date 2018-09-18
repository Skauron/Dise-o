using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class EditorPreguntas : EditorWindow {

    public ListaPreguntas preguntas;
    private string PreguntasProjectFilePath = "/StreamingAssets/Preguntas.json";

    [MenuItem("Window/Preguntas")]
    static void Init(){
        EditorPreguntas windows = (EditorPreguntas)EditorWindow.GetWindow(typeof(EditorPreguntas));
        windows.Show();
    }

    void OnGUI(){
        if(preguntas != null){
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("preguntas");
            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("Save data")){
                SaveGameData();
            }
        }
        if (GUILayout.Button("Load data")){
            LoadGameData();
        }
    }
    private void LoadGameData(){
        string filePath = Application.dataPath + PreguntasProjectFilePath;
        if (File.Exists(filePath)){
            string dataAsJson = File.ReadAllText(filePath);
            preguntas = JsonUtility.FromJson<ListaPreguntas>(dataAsJson);
        }
        else {
            preguntas = new ListaPreguntas();
        }
    }
    private void SaveGameData(){
        string dataAsJson = JsonUtility.ToJson(preguntas);
        string filePath = Application.dataPath + PreguntasProjectFilePath;
        File.WriteAllText(filePath,dataAsJson);
    }
}
