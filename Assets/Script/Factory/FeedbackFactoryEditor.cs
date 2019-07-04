//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Tilemaps;
//using UnityEditor;

//[CustomEditor(typeof(FeedbackFactory))]
//public class FeedbackFactoryEditor : Editor
//{
//    FeedbackFactory fbfactory;
//    Vector2 textBlocSize = new Vector2(100, 30);

//    private void Awake()
//    {
//        fbfactory = (FeedbackFactory)target;

//        Debug.Log("Inspector Enter");
//    }

//    public override void OnInspectorGUI()
//    {
//        foreach (string key in fbfactory.tileBaseFactory.GetKeys())
//        {
//            EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.LabelField("key : " + key);
//            TileBase tb = fbfactory.tileBaseFactory.GetObject(key);
//            tb = (TileBase)EditorGUILayout.ObjectField(tb, typeof(TileBase), true);
//            EditorGUILayout.EndHorizontal();
//        }

//        string keyToAdd = "";
//        keyToAdd = EditorGUILayout.TextField(keyToAdd);

//        if (GUILayout.Button("Add"))
//        {
//            Debug.Log("ADD");
//            fbfactory.tileBaseFactory.AddObject(keyToAdd, null);
//        }
//    }

//    private void OnDisable()
//    {
//        Debug.Log("Inspector End");
//    }

//}
