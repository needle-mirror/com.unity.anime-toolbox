using System.IO;

namespace Unity.AnimeToolbox {

public static class PathUtility {

    /// <summary>
    /// Get directory name n-levels up as specified by the parameter
    /// </summary>
    /// <param name="path">the base path</param>
    /// <param name="n">how many levels up</param>
    /// <returns>the directory name</returns>
    internal static string GetDirectoryName(string path, int n = 1) {
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