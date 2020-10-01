using System.IO;

namespace Unity.AnimeToolbox {

/// <summary>
/// A utility class for executing path-related operations.
/// </summary>
public static class PathUtility {

    /// <summary>
    /// Get the directory name of the path n-levels up 
    /// Ex: n=1. Assets/Scripts/Foo.cs => Assets/Scripts
    ///     n=2. Assets/Scripts/Foo.cs => Assets
    /// </summary>
    /// <param name="path">the base path</param>
    /// <param name="n">how many levels up</param>
    /// <returns>the directory name</returns>
    public static string GetDirectoryName(string path, int n = 1) {
        if (string.IsNullOrEmpty(path) || n<1)
            return null;

        string curDir = Path.GetDirectoryName(path);
        if (null == curDir)
            return null;
        
        if (n > 1) {
            return GetDirectoryName(curDir, n - 1);
        }

        return curDir;


    }
    
}

}