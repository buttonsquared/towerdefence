using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using SimpleJSON;

public class LevelExport : EditorWindow
{
    [MenuItem("Custom Editor/Export Level")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelExport));
    }

    Vector2 scrollPosition = Vector2.zero;
    int cracks = 2;
    string filename = "LevelX.json";

    void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        cracks = EditorGUILayout.IntSlider("Cracks", cracks, 1, 10);
        filename = EditorGUILayout.TextField("Filename:", filename);
        EditorGUILayout.LabelField("Export Level", EditorStyles.boldLabel);
        if (GUILayout.Button("Export"))
        {
            Export();
        }
    }

 
    // The export method
    void Export()
    {
        JSONClass levelInfo = new JSONClass();
        JSONArray listNode = new JSONArray();

        levelInfo.Add("ObjectPosition", listNode);
        levelInfo.Add("cracks", new JSONData(cracks));
        var ladyBugs = GameObject.FindGameObjectsWithTag("ladybugs");
    
        foreach (var item in ladyBugs)
        {
            JSONClass position = new JSONClass();
            position.Add("xPos", new JSONData(item.transform.position.x));
            position.Add("yPos", new JSONData(item.transform.position.y));
            position.Add("yRotation", new JSONData(item.transform.eulerAngles.y));
            position.Add("xRotation", new JSONData(item.transform.eulerAngles.x));
            position.Add("name", new JSONData("ladyBug"));
            listNode.Add(position);
        }

        var branch2Branch = GameObject.FindGameObjectsWithTag("branch2Branch");

        foreach (var item in branch2Branch)
        {
            JSONClass position = new JSONClass();
            position.Add("xPos", new JSONData(item.transform.position.x));
            position.Add("yPos", new JSONData(item.transform.position.y));
            position.Add("yRotation", new JSONData(item.transform.eulerAngles.y));
            position.Add("xRotation", new JSONData(item.transform.eulerAngles.x));
            position.Add("name", new JSONData("branch2Branch"));
            listNode.Add(position);
        }

        var branchSingle = GameObject.FindGameObjectsWithTag("branchSingle");

        foreach (var item in branchSingle)
        {
            JSONClass position = new JSONClass();
            position.Add("xPos", new JSONData(item.transform.position.x));
            position.Add("yPos", new JSONData(item.transform.position.y));
            position.Add("yRotation", new JSONData(item.transform.eulerAngles.y));
            position.Add("xRotation", new JSONData(item.transform.eulerAngles.x));
            position.Add("name", new JSONData("branchSingle"));
            listNode.Add(position);
        }

        var nest = GameObject.FindGameObjectsWithTag("nest");

        foreach (var item in nest)
        {
            JSONClass position = new JSONClass();
            position.Add("xPos", new JSONData(item.transform.position.x));
            position.Add("yPos", new JSONData(item.transform.position.y));
            position.Add("yRotation", new JSONData(item.transform.eulerAngles.y));
            position.Add("xRotation", new JSONData(item.transform.eulerAngles.x));
            position.Add("name", new JSONData("nest"));
            listNode.Add(position);
        }

        var player = GameObject.FindGameObjectsWithTag("Player");

        foreach (var item in player)
        {
            JSONClass position = new JSONClass();
            position.Add("xPos", new JSONData(item.transform.position.x));
            position.Add("yPos", new JSONData(item.transform.position.y));
            position.Add("yRotation", new JSONData(item.transform.eulerAngles.y));
            position.Add("xRotation", new JSONData(item.transform.eulerAngles.x));
            position.Add("name", new JSONData("player"));
            listNode.Add(position);
        }

        if (EditorUtility.DisplayDialog("Save confirmation",
            "Are you sure you want to save level " + filename + "?", "OK", "Cancel"))
        {
            string level = levelInfo.ToString(); 
            SaveItemInfo(level, filename);
            EditorUtility.DisplayDialog("Saved", filename + " saved!", "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("NOT Saved", filename + " not saved!", "OK");
        }
    }


    private void ShowErrorForNull(string gameObjectName)
    {
        EditorUtility.DisplayDialog("Error", "Cannot find gameobject " + gameObjectName, "OK");
    }

    public void SaveItemInfo(string json, string levelName)
    {
        string path = null;
        #if UNITY_EDITOR
                path = "Assets/Resources/GameJSONData/" + levelName;
        #endif
        #if UNITY_STANDALONE
                // You cannot add a subfolder, at least it does not work for me
                path = "Assets/Resources/GameJSONData/" + levelName;
             #endif

        using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.Write(json);
                    }
                }
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}