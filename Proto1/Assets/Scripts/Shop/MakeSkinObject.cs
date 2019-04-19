using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MakeSkinObject
{
    [MenuItem("Assets/Create/Skin Object")]
    public static void Create()
    {
        SkinObject asset = ScriptableObject.CreateInstance<SkinObject>();
        AssetDatabase.CreateAsset(asset, "Assets/Shop/ShopItems/NewSkinObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
