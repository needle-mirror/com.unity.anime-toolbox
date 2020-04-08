using System.IO;
using UnityEngine;

namespace Unity.AnimeToolbox {

/// <summary>
/// A utility class to do file operations
/// </summary>
public static class FileUtility  {

    /// <summary>
    /// Make file writable
    /// </summary>
    /// <param name="path">The path to the file</param>
    public static void TryMakeFileWritable(string path) {
        if (!File.Exists(path))  {
            Debug.LogError("[AnimeToolbox] TryMakeFileWritable() Path doesn't exist: " + path);
            return;
        }

        FileAttributes attributes = File.GetAttributes(path);

        if (FileAttributes.ReadOnly == (attributes & FileAttributes.ReadOnly) ) {
            // Remove RO
            attributes.RemoveAttribute(FileAttributes.ReadOnly);
            File.SetAttributes(path, attributes);
        } 

    }

//---------------------------------------------------------------------------------------------------------------------
        
}

} //end namespace

