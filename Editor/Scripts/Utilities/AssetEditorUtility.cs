using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.AnimeToolbox.Editor {

/// <summary>
/// A utility class for executing operations related to Unity assets in the editor.
/// </summary>
public static class AssetEditorUtility {

    /// <summary>
    /// Pings (highlights) an asset by its path in the Project window.
    /// The path can be absolute, or relative to the Unity project folder.
    /// </summary>
    /// <param name="path">The asset path.</param>
    /// <returns>True if the asset is found. False otherwise.</returns>
    public static bool PingAssetByPath(string path) {
        Object asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetUtility.NormalizeAssetPath(path));
        if (asset == null) 
            return false;
        
        EditorGUIUtility.PingObject(asset);
        return true;        
    }
    
//----------------------------------------------------------------------------------------------------------------------
    
    /// <summary>
    /// Creates an asset in a given path from an Object.
    /// This will overwrite the existing asset if it exists.
    /// </summary>
    /// <param name="asset">The object to be created as an asset.</param>
    /// <param name="path">The path of the asset, relative to the Unity project folder.</param>
    public static void OverwriteAsset(Object asset, string path) {
        if (File.Exists(path)) {
            AssetDatabase.DeleteAsset(path);
        }

        AssetDatabase.CreateAsset(asset, path);
    }
    
//----------------------------------------------------------------------------------------------------------------------
    
    /// <summary>
    /// Delete assets/files in a given path with specified file patterns.
    /// This can delete files both inside and outside Unity project folder.
    /// </summary>
    /// <param name="path">The path which contain the assets to be deleted.</param>
    /// <param name="searchPattern">The pattern of the files. Ex: "*.prefab"</param>
    public static void DeleteAssetsOrFiles(string path, string searchPattern) {
        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            return;

        bool isUnityAsset = path.StartsWith(Application.dataPath);
        Action<string> onFileFound = null;
        if (isUnityAsset) {
            onFileFound = (string filePath) => {
                AssetDatabase.DeleteAsset(AssetUtility.NormalizeAssetPath(filePath.Replace('\\','/')));
            };
        } else {
            onFileFound = (string filePath) => {
                File.Delete(filePath);
            };            
        }

        EnumerateFiles(path, searchPattern, onFileFound);        
    }
    
//----------------------------------------------------------------------------------------------------------------------
    
    private static void EnumerateFiles(string path, string searchPattern, Action<string> onFileFound) {
        DirectoryInfo di           = new DirectoryInfo(path);
        FileInfo[]    files        = di.GetFiles(searchPattern);
        foreach (FileInfo fi in files) {
            onFileFound(fi.FullName);
        }
        
    }
    
    
}

} //end namespace