﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CreateHullObject 
{
    [MenuItem("Assets/Create/Hull Object")]
    public static void Create()
    {
        //Just copying unity's live tutorials
        HullObject asset = ScriptableObject.CreateInstance<HullObject>();
        AssetDatabase.CreateAsset(asset, "Assets/Upgrades/Hull/NewHullObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
