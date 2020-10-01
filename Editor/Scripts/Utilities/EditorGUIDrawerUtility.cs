﻿using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Unity.AnimeToolbox.Editor {

/// <summary>
/// A utility class for drawing GUI in the editor.
/// </summary>
public static class EditorGUIDrawerUtility {    

//----------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Draws a standard file selector GUI used in AnimeToolbox. 
    /// </summary>
    /// <param name="label">The label. Can be null.</param>
    /// <param name="panelDialogTitle">The title of the file panel dialog box.</param>
    /// <param name="initialFilePath">The initial file path.</param>
    /// <param name="fileExtension">The default extension of the file to be selected.</param>
    /// <param name="onReload">The action to be performed when the reload button is clicked.
    ///     Passing null will hide the reload button.
    /// </param>
    /// <param name="onValidFileSelected">A postprocess to be executed on the new path. Can be null.</param>
    /// <returns></returns>
    public static string DrawFileSelectorGUI(string label, 
        string panelDialogTitle, 
        string initialFilePath, 
        string fileExtension,
        Action onReload,
        Func<string, string> onValidFileSelected)
    {

        string newFilePath = null;
        using(new EditorGUILayout.HorizontalScope()) {
            DrawSelectableText(label, initialFilePath);
            newFilePath = ReceiveDragAndDropFromLastGUI(initialFilePath);
            DrawReloadButton(onReload);           
            newFilePath = DrawSelectFileButton(panelDialogTitle,newFilePath,fileExtension, onValidFileSelected);
        }
        
        using (new EditorGUILayout.HorizontalScope()) {
            GUILayout.FlexibleSpace();
            DrawRevealInFinderButton(newFilePath);            
            bool isValidFile = File.Exists(newFilePath) && newFilePath.StartsWith("Assets/");
            DrawHighlightInProjectButton(newFilePath, isValidFile);
        }
        
        return newFilePath;
    }

//----------------------------------------------------------------------------------------------------------------------    
    /// <summary>
    /// Draws a standard folder selector GUI used in AnimeToolbox. 
    /// </summary>
    /// <param name="label">The label. Can be null.</param>
    /// <param name="panelDialogTitle">The title of the folder panel dialog box.</param>
    /// <param name="initialFolderPath">The initial folder path.</param>
    /// <param name="onReload">The action to be performed when the reload button is clicked.
    ///     Passing null will hide the reload button.
    /// </param>
    /// <param name="onValidFolderSelected">A postprocess to be executed on the new path. Can be null.</param>
    /// <returns></returns>
    public static string DrawFolderSelectorGUI(string label, 
        string panelDialogTitle, 
        string initialFolderPath, 
        Action onReload,        
        Func<string, string> onValidFolderSelected)
    {

        string newDirPath = null;
        using(new EditorGUILayout.HorizontalScope()) {
            DrawSelectableText(label, initialFolderPath);
            newDirPath = ReceiveDragAndDropFromLastGUI(initialFolderPath);           
            DrawReloadButton(onReload);            
            newDirPath = DrawSelectFolderButton(panelDialogTitle, newDirPath, onValidFolderSelected);
        }
        
        using (new EditorGUILayout.HorizontalScope()) {
            GUILayout.FlexibleSpace();
            DrawRevealInFinderButton(newDirPath);
            DrawHighlightInProjectButton(newDirPath, AssetDatabase.IsValidFolder(newDirPath));
        }
        
        return newDirPath;
    }
    
    
//----------------------------------------------------------------------------------------------------------------------
    private static string DrawSelectFileButton(string panelDialogTitle, string filePath, string fileExtension,
        Func<string, string> onValidFileSelected) 
    {
        bool buttonPressed = DrawTextureButton("d_Project@2x", "Select");
        if (!buttonPressed) 
            return filePath;
        
        string dir          = PathUtility.GetDirectoryName(filePath);
        string selectedFile = EditorUtility.OpenFilePanel(panelDialogTitle, dir,fileExtension);
        return ValidatePanelResult(selectedFile, onValidFileSelected);        
    }
    
//----------------------------------------------------------------------------------------------------------------------    
    
    private static string DrawSelectFolderButton(string title, string folderPath, 
        Func<string, string> onValidFolderSelected) 
    {
        bool buttonPressed = DrawTextureButton("d_Project@2x", "Select");
        if (!buttonPressed) 
            return folderPath;
        
        string selectedFolder = EditorUtility.OpenFolderPanel(title, folderPath, "");
        return ValidatePanelResult(selectedFolder, onValidFolderSelected);
    }
    
//----------------------------------------------------------------------------------------------------------------------

    private static string ValidatePanelResult(string panelResult, Func<string, string> onSelected) {
        if(!string.IsNullOrEmpty(panelResult)) {
            string newResult = null;                    
            newResult = (onSelected != null) ? onSelected(panelResult) : panelResult;
            return newResult;
        } else {
            GUIUtility.ExitGUI(); //prevent error when cancel is pressed                
        }
        return panelResult;
    } 
    

//----------------------------------------------------------------------------------------------------------------------
    private static string ReceiveDragAndDropFromLastGUI(string val) {
        Rect lastGUIRect = GUILayoutUtility.GetLastRect();
        
        Event evt = Event.current;
        switch (evt.type) {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!lastGUIRect.Contains (evt.mousePosition))
                    return val;
     
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (evt.type == EventType.DragPerform) {
                    DragAndDrop.AcceptDrag ();
    
                    if (DragAndDrop.paths.Length <= 0)
                        break;
                    
                    return DragAndDrop.paths[0];
                }

                break;
            default:
                break;
        }

        return val;

    }
    
//----------------------------------------------------------------------------------------------------------------------

    private static void DrawSelectableText(string prefix, string text) {
        if (!string.IsNullOrEmpty (prefix)) {
            EditorGUILayout.PrefixLabel(prefix);
        } 

        EditorGUILayout.SelectableLabel(text,
            EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight)
        );
        
    }
    

    private static void DrawReloadButton(Action onReload) {
        if (null == onReload) 
            return;
        
        bool buttonPressed = DrawTextureButton("d_RotateTool On@2x", "Reload");
        if (buttonPressed) {
            onReload();
        }
    }
    
    private static bool DrawTextureButton(string textureName, string textFallback, int guiWidth = 32) {

        Texture2D reloadTex     = EditorGUIUtility.Load(textureName) as Texture2D;
        float     lineHeight    = EditorGUIUtility.singleLineHeight;
        if (null == reloadTex) {
            return GUILayout.Button(textFallback, GUILayout.Width(guiWidth), GUILayout.Height(lineHeight));
        }
        
        return GUILayout.Button(reloadTex, GUILayout.Width(guiWidth), GUILayout.Height(lineHeight));
    }

    private static void DrawRevealInFinderButton(string path) {
        if (GUILayout.Button("Show", GUILayout.Width(50f),GUILayout.Height(EditorGUIUtility.singleLineHeight))) {
            EditorUtility.RevealInFinder(path);
        }        
    }

    private static void DrawHighlightInProjectButton(string path, bool enabled) {
        EditorGUI.BeginDisabledGroup(!enabled);        
        if(GUILayout.Button("Highlight in Project Window", GUILayout.Width(180f))) {
            AssetEditorUtility.PingAssetByPath(path);
        }                
        EditorGUI.EndDisabledGroup();
        
    }
    
}

} //end namespace

