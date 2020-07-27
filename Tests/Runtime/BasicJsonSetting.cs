using System;
using Unity.AnimeToolbox;
using UnityEngine;

[Serializable]
internal class BasicJsonSetting {

//----------------------------------------------------------------------------------------------------------------------


    internal static  BasicJsonSetting GetInstance() {
        if (null != m_instance)
            return m_instance;

        BasicJsonSetting settings = LoadSetting();
        if (null != settings) {
            return settings;
        }

        m_instance = new  BasicJsonSetting();
        m_instance.SaveSetting();
        return m_instance;

    }



//----------------------------------------------------------------------------------------------------------------------
    internal string GetSettingPath() { return BASIC_SETTINGS_PATH; }
    internal void SetValue(int v) { m_basicValue = v;}
    internal int GetValue() { return m_basicValue;}
    

//----------------------------------------------------------------------------------------------------------------------
    #region File Load/Save for Serialization/deserialization
    static  BasicJsonSetting LoadSetting() {
        return FileUtility.DeserializeFromJson<BasicJsonSetting>(BASIC_SETTINGS_PATH);
    }
    
    internal void SaveSetting() {
        FileUtility.SerializeToJson(m_instance, BASIC_SETTINGS_PATH);
        
    }
    #endregion
    

//----------------------------------------------------------------------------------------------------------------------


    private const string BASIC_SETTINGS_PATH = "Library/AnimeToolboxTests/BasicJsonSetting.asset";


    [SerializeField] private int m_basicValue = 1;

//----------------------------------------------------------------------------------------------------------------------

    private static  BasicJsonSetting m_instance;

}
