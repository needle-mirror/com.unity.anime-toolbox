using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Recorder.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.AnimeToolbox.Editor {

[InitializeOnLoad]
internal class AnimeToolbar   {
    static AnimeToolbar() {
        m_autoAddedRecorderTracks = new List<RecorderTrack>();
        UnityToolbarExtension.OnGUIRightCallback += OnUnityToolbarGUIRight;

        //Load image button
        m_recorderButtonImage = AssetDatabase.LoadAssetAtPath<Texture2D>(RECORDER_BUTTON_IMAGE_PATH);
        if (null == m_recorderButtonImage) {
            EditorApplication.update += UpdateToLoadPackageImages;
        }
    }

//---------------------------------------------------------------------------------------------------------------------

    static void UpdateToLoadPackageImages() {
        if (!EditorApplication.isCompiling) {
            m_recorderButtonImage = AssetDatabase.LoadAssetAtPath<Texture2D>(RECORDER_BUTTON_IMAGE_PATH);
            EditorApplication.update -= UpdateToLoadPackageImages;
        }
    }

//---------------------------------------------------------------------------------------------------------------------

    static void OnUnityToolbarGUIRight() {
        if (null == m_recorderButtonImage || !m_showRecorderButton) {
            return;
        }

        if (GUILayout.Button(m_recorderButtonImage, 
                GUILayout.Width(AnimeToolboxConstants.TOOL_BUTTON_LENGTH), 
                GUILayout.Height(AnimeToolboxConstants.TOOLBAR_HEIGHT))
           ) 
        {
            if (Application.isPlaying) {
                UnityEditor.EditorApplication.isPlaying = false;
            } else {

                StartAnimeRecording();
            }
        }
    }

//---------------------------------------------------------------------------------------------------------------------

    static void StartAnimeRecording() {

        TryAddWatcher();

        //Find all playable directors and add RecorderTrack
        m_activePlayableDirectors = GameObject.FindObjectsOfType<PlayableDirector>();
        foreach (PlayableDirector playableDirector in m_activePlayableDirectors) {
            PlayableAsset playableAsset = playableDirector.playableAsset;

            if (!(playableAsset is TimelineAsset)) {
                continue;
            }

            //Add RecorderTrack if it doesn't exist yet
            TimelineAsset timelineAsset = playableAsset as TimelineAsset;
            AnimeRecorderTrack recorderTrack = null;
            foreach (TrackAsset trackAsset in timelineAsset.GetOutputTracks()) {
                if (trackAsset is AnimeRecorderTrack) {
                    recorderTrack = trackAsset as AnimeRecorderTrack;
                    break;
                }
            }
            if (null == recorderTrack) {
                recorderTrack = timelineAsset.CreateTrack<AnimeRecorderTrack>(null, "Anime Recorder Track");
                recorderTrack.Init(playableAsset.duration);
            } else {
                recorderTrack.SetDuration(playableAsset.duration);
            }

        }

        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        //Start recording
        EditorApplication.EnterPlaymode();

    }

//---------------------------------------------------------------------------------------------------------------------

    static void OnPlayModeStateChanged(PlayModeStateChange stateChange) {
        Debug.Log("OnPlayModeStateChanged");
        if (stateChange == PlayModeStateChange.EnteredEditMode) {
            CleanupAnimeRecording();
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }
    }
//---------------------------------------------------------------------------------------------------------------------
    static void CleanupAnimeRecording() {

        Debug.Log("Cleaning Up");

        foreach(RecorderTrack rt in m_autoAddedRecorderTracks) {
            rt.timelineAsset.DeleteTrack(rt);
        }
        m_autoAddedRecorderTracks.Clear();
    }


//---------------------------------------------------------------------------------------------------------------------

    static void TryAddWatcher() {
        AnimeRecordingWatcher[] watchers = GameObject.FindObjectsOfType<AnimeRecordingWatcher>();
        if (watchers.Length > 0)
            return;

        GameObject go = new GameObject("AnimeRecordingWatcher");
        go.AddComponent<AnimeRecordingWatcher>();
    }    

//---------------------------------------------------------------------------------------------------------------------

    static PlayableDirector[] m_activePlayableDirectors = null;
    static List<RecorderTrack> m_autoAddedRecorderTracks = null;

    static Texture2D m_recorderButtonImage = null;
    const string RECORDER_BUTTON_IMAGE_FILENAME = "BtnRecord.png";
    static readonly string RECORDER_BUTTON_IMAGE_PATH = Path.Combine(AnimeToolboxConstants.PACKAGE_PATH, "Editor/Images", RECORDER_BUTTON_IMAGE_FILENAME);
    private static bool m_showRecorderButton = false;
}

} //end namespace


