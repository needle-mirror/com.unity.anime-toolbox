using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.AnimeToolbox {

internal class AnimeToolboxWindow : EditorWindow {
    [MenuItem("Window/General/Anime Toolbox")] 
    public static void ShowAnimeToolboxWindow() {
        AnimeToolboxWindow window = GetWindow<AnimeToolboxWindow>();
        window.titleContent = new GUIContent("Anime Toolbox");
        window.minSize = new Vector2(250, 50);
    }
    
//---------------------------------------------------------------------------------------------------------------------

    void OnEnable() {        
        m_root = rootVisualElement;

        string stylePath = Path.Combine(AnimeToolboxConstants.PACKAGE_PATH,"Editor/USS/AnimeToolboxStyle.uss");
        string windowLayoutPath = Path.Combine(AnimeToolboxConstants.PACKAGE_PATH,"Editor/UXML/AnimeToolboxWindow.uxml");

        m_root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(stylePath));
        VisualTreeAsset windowLayout = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(windowLayoutPath);
        windowLayout.CloneTree(m_root);

        UQueryBuilder<Button> buttons = m_root.Query<Button>();
        buttons.ForEach(SetupButton);

    }

//---------------------------------------------------------------------------------------------------------------------

    void SetupButton(Button button)  {
        button.tooltip = button.name;

        //Click event
        if ("CompositionButton" == button.name) {
            button.clickable.clicked += () => {
                LayoutUtility.LoadLayout(AnimeToolboxConstants.COMPOSITION_LAYOUT_FULL_PATH);
            };
            return;
        }

        button.clickable.clicked += () => Debug.Log(button.name);
    }


//---------------------------------------------------------------------------------------------------------------------

    VisualElement m_root = null;

    const string PACKAGE_PATH = "Packages/com.unity.anime-toolbox";

}


} //end namespace