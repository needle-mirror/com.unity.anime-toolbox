using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Unity.AnimeToolbox.Tests {

internal class AssetUtilityTest {

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor)]
    
    public void NormalizeAssetPathOnWindows() {

        string unityAssetPath = Path.Combine(Application.dataPath, "Foo.prefab").Replace(Path.DirectorySeparatorChar,'/'); 
        const string NON_UNITY_ASSET_PATH = @"C:/NonUnityProject/Foo.prefab";

        string normalizedUnityAssetPath = AssetUtility.NormalizeAssetPath(unityAssetPath);
        Assert.AreEqual("Assets/Foo.prefab", normalizedUnityAssetPath);

        string normalizedNonUnityAssetPath = AssetUtility.NormalizeAssetPath(NON_UNITY_ASSET_PATH);
        Assert.AreEqual(NON_UNITY_ASSET_PATH, normalizedNonUnityAssetPath);
    }
    
}
 

        
        
} //end namespace
