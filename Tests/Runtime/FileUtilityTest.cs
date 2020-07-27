using System.IO;
using NUnit.Framework;


namespace Unity.AnimeToolbox.Tests {

internal class FileUtilityTest {

    [Test]
    public void LoadAndSaveSetting() {
        const int TEST_VALUE = 12345;
        BasicJsonSetting singletonSetting = BasicJsonSetting.GetInstance();
        singletonSetting.SetValue(TEST_VALUE);
        singletonSetting.SaveSetting();

        Assert.IsTrue(File.Exists(singletonSetting.GetSettingPath()));
        
        BasicJsonSetting fileSetting =
            FileUtility.DeserializeFromJson<BasicJsonSetting>(singletonSetting.GetSettingPath());
        Assert.NotNull(fileSetting);

        Assert.AreEqual(TEST_VALUE, fileSetting.GetValue());        
    }
    
}
 

        
        
} //end namespace
