using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class  CreateFuelTankObject
{
    [MenuItem("Assets/Create/FuelTank Object")]
    public static void Create()
    {
        //Just copying unity's live tutorials
        DrillObject asset = ScriptableObject.CreateInstance<DrillObject>();
        AssetDatabase.CreateAsset(asset, "Assets/Upgrades/FuelTanks/NewFuelTankObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
