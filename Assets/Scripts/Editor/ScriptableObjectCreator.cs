using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectCreator : ScriptableWizard {

    [SerializeField]
    private string type;

    [MenuItem("Assets/Create/Scriptable Object", false, 'S')]
    public static void CreateWizard() {
        ScriptableWizard.DisplayWizard<ScriptableObjectCreator>("Create Scriptable Object", "Create");
    }

    public void OnWizardCreate() {
        ScriptableObject asset = ScriptableObject.CreateInstance(type);
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (string.IsNullOrEmpty(path)) {
            path = "Assets";
        } else if (! Directory.Exists(path)){
            path = Path.GetDirectoryName(path);
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{path}/New {type}.asset");
        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }    
}