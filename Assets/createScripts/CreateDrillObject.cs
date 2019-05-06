using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CreateDrillObject 
{
    [MenuItem("Assets/Create/Drill Object")]
    public static void Create()
    {
        //Just copying unity's live tutorials
        DrillObject asset = ScriptableObject.CreateInstance<DrillObject>();
        AssetDatabase.CreateAsset(asset, "Assets/Upgrades/Drills/NewDrillObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
