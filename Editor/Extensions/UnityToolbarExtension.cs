using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEngine.UIElements;


namespace Unity.AnimeToolbox.Editor {

internal static class UnityToolbarExtension {


    public delegate void OnGUICallbackFunction();
    public static OnGUICallbackFunction OnGUIRightCallback;
    public static OnGUICallbackFunction OnGUILeftCallback;

    [InitializeOnLoadMethod]
    static void OnEditorLoad() {
        const string TOOL_COUNT_FIELD_NAME = "k_ToolCount";			
        FieldInfo toolIcons = TOOLBAR_TYPE.GetField(TOOL_COUNT_FIELD_NAME, 
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static
        );
        const int DEFAULT_TOOL_COUNT = 7;
            
        m_toolCount = (null!= toolIcons) ? ((int) (toolIcons.GetValue(null))) : DEFAULT_TOOL_COUNT;

        EditorApplication.update += UpdateUnityToolbarExtension;
    }


//---------------------------------------------------------------------------------------------------------------------

    static void UpdateUnityToolbarExtension() {
        TrySetupUnityToolbar();
    }

//---------------------------------------------------------------------------------------------------------------------

    static void OnUnityToolbarGUI() {

        TrySetupGUIStyle();


        const int LEFT_EMPTY_SPACE = 9;
        const int TOOLBAR_Y = 4;
        const int PIVOT_BUTTON_LENGTH = 64;

        float screenWidth = EditorGUIUtility.currentViewWidth;
        float playButtonPosition = (screenWidth / 2) - 70;

        //LEFT
        int LEFT_RECT_OFFSET = 
            ( LEFT_EMPTY_SPACE + (AnimeToolboxConstants.TOOL_BUTTON_LENGTH * m_toolCount)) //Tool buttons
            + (LEFT_EMPTY_SPACE * 2) + (PIVOT_BUTTON_LENGTH * 2) //Two pivot buttons
            + LEFT_EMPTY_SPACE
        ;

        Rect leftRect = new Rect(0, 0, screenWidth, Screen.height);
        leftRect.xMin += LEFT_RECT_OFFSET;
        leftRect.xMax = playButtonPosition -  LEFT_EMPTY_SPACE;
        leftRect.y = TOOLBAR_Y;
        leftRect.height = AnimeToolboxConstants.TOOLBAR_HEIGHT;

        if (leftRect.width > 0) {
            GUILayout.BeginArea(leftRect);
            GUILayout.BeginHorizontal();
            if (null!=OnGUILeftCallback) {
                OnGUILeftCallback();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }


        //RIGHT
        const int RIGHT_EMPTY_SPACE = 11;
        const int CLOUD_BUTTON_LENGTH = 30;
        const int RIGHT_BUTTON_LENGTH = 78;

        const int RIGHT_RECT_OFFSET = 
            ((RIGHT_EMPTY_SPACE * 4) + (RIGHT_BUTTON_LENGTH * 3))//Account, Layers, GameLayout
            + RIGHT_EMPTY_SPACE + CLOUD_BUTTON_LENGTH
        ;

        // Find the rect to put buttons on the right
        Rect rightRect = new Rect(0, 0, screenWidth, Screen.height);
        rightRect.xMin = playButtonPosition;
        rightRect.xMin += m_commandLeftGUIStyle.fixedWidth * 3 + RIGHT_EMPTY_SPACE;  
        rightRect.xMax = screenWidth;
        rightRect.xMax -= RIGHT_RECT_OFFSET;
        rightRect.y = TOOLBAR_Y;
        rightRect.height = AnimeToolboxConstants.TOOLBAR_HEIGHT;

        if (rightRect.width > 0) {
            GUILayout.BeginArea(rightRect);
            GUILayout.BeginHorizontal();
            if (null!=OnGUIRightCallback) {
                OnGUIRightCallback();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }


//---------------------------------------------------------------------------------------------------------------------

    //[Note-sin: 2019-10-17] Needs to be executed in EditorApplication.Update. 
    //will fail if executed in InitializeOnLoad. 
    static void TrySetupUnityToolbar() {
        if (null != m_unityToolbar) 
            return;

        object[] toolbars = Resources.FindObjectsOfTypeAll(TOOLBAR_TYPE);
        m_unityToolbar = toolbars.Length > 0 ? toolbars[0] as ScriptableObject : null;
        if (null!= m_unityToolbar) {
            const string ON_GUI_HANDLER_FIELD = "m_OnGUIHandler";

            Type guiViewType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GUIView");
            PropertyInfo visualTreeProperty = guiViewType.GetProperty("visualTree",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );
            FieldInfo onGUIHandlerField = typeof(IMGUIContainer).GetField(ON_GUI_HANDLER_FIELD,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );

            if (null == guiViewType || null == visualTreeProperty || null== onGUIHandlerField) {
                Debug.LogError("[AnimeToolbox] GetOrFindUnityToolbar() Type errors");
                return;
            }

            //Attach handler to m_OnGUIHandler of the main toolbare
            VisualElement visualTree = visualTreeProperty.GetValue(m_unityToolbar, null) as VisualElement;
            if (null == visualTree || visualTree.childCount <= 0) {
                Debug.LogError("[AnimeToolbox] GetOrFindUnityToolbar() Error when finding visual Tree");
                return;
            }

            IMGUIContainer container = visualTree[0] as　IMGUIContainer;
            if (null == container) {
                Debug.LogError("[AnimeToolbox] GetOrFindUnityToolbar() 1st child of visual tree != IMGUIContainer");
                return;
            }

            Action onGUIHandler = onGUIHandlerField.GetValue(container) as Action;
            if (null == onGUIHandler) {
                Debug.LogError("[AnimeToolbox] GetOrFindUnityToolbar() " + ON_GUI_HANDLER_FIELD + " not found");
                return ;
            }
            onGUIHandler -= OnUnityToolbarGUI;
            onGUIHandler += OnUnityToolbarGUI;
            onGUIHandlerField.SetValue(container, onGUIHandler);
        }

    }

//---------------------------------------------------------------------------------------------------------------------
    static void TrySetupGUIStyle() {
        if (null != m_commandLeftGUIStyle) {
            return;
        }

        m_commandLeftGUIStyle = new GUIStyle("CommandLeft");
    }
    
//---------------------------------------------------------------------------------------------------------------------
    static readonly Type TOOLBAR_TYPE = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");	

    static int m_toolCount;
    static GUIStyle m_commandLeftGUIStyle = null;
    static ScriptableObject m_unityToolbar = null;

}


} //end namespace
