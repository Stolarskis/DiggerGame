using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CreateEngineObject
{
    [MenuItem("Assets/Create/Engine Object")]
    public static void Create()
    {
        //Just copying unity's live tutorials
        EngineObject asset = ScriptableObject.CreateInstance<EngineObject>();
        AssetDatabase.CreateAsset(asset, "Assets/Upgrades/Engines/NewEngineObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
