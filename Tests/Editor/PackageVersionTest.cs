using NUnit.Framework;
using Unity.AnimeToolbox.Editor;

namespace Unity.AnimeToolbox.EditorTests {

internal class PackageVersionTest {
                
    [Test]
    public void CreatePackageVersionsFromSemVer() {
        CreateAndCheckPackageVersion("1.0.2-preview"              , 1, 0, 2, PackageLifecycle.PREVIEW, null);
        CreateAndCheckPackageVersion("9.3.5-preview.1"            , 9, 3, 5, PackageLifecycle.PREVIEW, "1");
        CreateAndCheckPackageVersion("4.0.5-experimental.alpha.1" , 4, 0, 5, PackageLifecycle.EXPERIMENTAL, "alpha.1");
        CreateAndCheckPackageVersion("3.0.4-pre.10"               , 3, 0, 4, PackageLifecycle.PRERELEASE, "10");
        CreateAndCheckPackageVersion("7.0.2.final"                , 7, 0, 2, PackageLifecycle.RELEASED, "final");
    }

//----------------------------------------------------------------------------------------------------------------------


    private void CreateAndCheckPackageVersion(string semanticVer, int major, int minor, int patch, PackageLifecycle lifecycle, 
        string additionalMetadata) {

        PackageVersion packageVersion = new PackageVersion(semanticVer);
        Assert.AreEqual(major, packageVersion.Major);
        Assert.AreEqual(minor, packageVersion.Minor);
        Assert.AreEqual(patch, packageVersion.Patch);
        Assert.AreEqual(lifecycle, packageVersion.Lifecycle);
        Assert.AreEqual(additionalMetadata, packageVersion.AdditionalMetadata);        
        Assert.AreEqual(semanticVer, packageVersion.ToString());
    }
    
}
}