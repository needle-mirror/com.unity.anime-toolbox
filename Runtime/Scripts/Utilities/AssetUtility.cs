using UnityEngine;

namespace Unity.AnimeToolbox {

/// <summary>
/// A utility class for executing operations related to Unity assets.
/// Can be executed by runtime code in the editor, but should not be executed in an executable.
/// </summary>
public static class AssetUtility {

    /// <summary>
    /// Normalize an absolute path under Unity project to make it relative to the Unity project folder.
    /// Paths that are outside Unity project will be unchanged.
    /// Only slash ('/') is regarded as a directory separator. 
    /// Ex: C:/TempUnityProject/Assets/Foo.prefab => Assets/Foo.prefab
    ///     C:/NonUnityProject/Foo.prefab => C:/NonUnityProject/Foo.prefab
    /// </summary>
    /// <param name="path">The path to be normalized.</param>
    /// <returns>The normalized path.</returns>
    public static string NormalizeAssetPath(string path) {
        if (string.IsNullOrEmpty(path))
            return null;

        if (path.StartsWith(Application.dataPath)) {
            return path.Substring(Application.dataPath.Length - "Assets".Length);
        }
        return path;
    }

}

} //end namespace