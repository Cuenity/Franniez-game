using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalizedTextEditor : EditorWindow
{
    public LocalizationData localizationData;
    private string FilePath;
    private Vector2 scrollPos;

    // Maak de window aan
    [MenuItem("Window/Localized Text Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
    }

    private void OnGUI()
    {
        
        if (localizationData != null)
        {
            EditorGUILayout.BeginVertical();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(400), GUILayout.Height(700));
            // Label zodat je weet in welke taal je bezig bent.
            string language = localizationData.items[0].value;
            EditorGUILayout.LabelField(("Input for " + language).ToUpper(), EditorStyles.boldLabel);
            EditorGUILayout.Space();
            this.Repaint();


            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");

            // UNfold properties
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();





            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Save data"))
        {
            SaveGameData();
        }
        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }

        if (GUILayout.Button("Create data"))
        {
            CreateNewData();
        }

    }


    private void LoadGameData()
    {
        FilePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

        if (!string.IsNullOrEmpty(FilePath))
        {
            string dataAsJason = File.ReadAllText(FilePath);

            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJason);
        }
    }

    private void SaveGameData()
    {
        if (!string.IsNullOrEmpty(FilePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(FilePath, dataAsJson);
        }
    }

    private void CreateNewData()
    {
        localizationData = new LocalizationData();
    }
}
