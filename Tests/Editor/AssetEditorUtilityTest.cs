using System.IO;
using NUnit.Framework;
using Unity.AnimeToolbox.Editor;
using UnityEngine;

namespace Unity.AnimeToolbox.EditorTests {
internal class AssetEditorUtilityTest {
                
    [Test]
    public void CreateAndDeleteScriptableObjectInDataPath() {
        const string TEST_FILE_NAME = "AssetEditorUtilityTest.asset";
       
        IntScriptableObject asset = ScriptableObject.CreateInstance<IntScriptableObject>();
        string     path = AssetUtility.NormalizeAssetPath(Path.Combine(Application.dataPath, TEST_FILE_NAME));
        AssetEditorUtility.OverwriteAsset(asset,path);        
        Assert.IsTrue(File.Exists(path));

        AssetEditorUtility.DeleteAssetsOrFiles(Application.dataPath, TEST_FILE_NAME);
        Assert.IsFalse(File.Exists(path));                
    }

//----------------------------------------------------------------------------------------------------------------------

    [Test]
    public void CreateAndDeleteTextInTempCachePath() {
        const string TEST_FILE_NAME = "AssetEditorUtilityTest.txt";
        const string TEXT           = "This is a test from AnimeToolbox";
        
        string path = Path.Combine(Application.temporaryCachePath, TEST_FILE_NAME);
        File.WriteAllText(path, TEXT);
        Assert.IsTrue(File.Exists(path));

        AssetEditorUtility.DeleteAssetsOrFiles(Application.temporaryCachePath, TEST_FILE_NAME);
        Assert.IsFalse(File.Exists(path));                
    }

    
}
}
