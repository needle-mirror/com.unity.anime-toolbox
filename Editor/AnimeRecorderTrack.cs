using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEditor.Recorder.Timeline;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace Unity.AnimeToolbox.Editor {

internal class AnimeRecorderTrack : RecorderTrack
{

    public void Init(double duration) {
        AddTimelineClip(duration);
    }

//---------------------------------------------------------------------------------------------------------------------

    public void SetDuration(double duration) {
        IEnumerator<TimelineClip> enumerator = this.GetClips().GetEnumerator();
        if (!enumerator.MoveNext()) {
            Debug.LogError("[AnimeToolbox] AnimeRecorderTrack doesn't have a TimelineClip. Creating");
            AddTimelineClip(duration);
            return;
        }
        TimelineClip timelineClip = enumerator.Current;
        timelineClip.duration = duration;
    }

//---------------------------------------------------------------------------------------------------------------------
    void AddTimelineClip(double duration) {
        TimelineClip timelineClip = this.CreateClip<RecorderClip>();
        timelineClip.start = 0;
        timelineClip.duration = duration;

        RecorderClip recorderClip = timelineClip.asset as RecorderClip;
        if (null!=recorderClip.settings) {
            UnityEngine.Object.DestroyImmediate(recorderClip,true);
            recorderClip.settings = null;
        }

        recorderClip.settings = CreateMovieRecorderSettings();
        AssetDatabase.AddObjectToAsset(recorderClip.settings, recorderClip);
        AssetDatabase.Refresh();

        //force refresh
        object timelineWindow = TIMELINE_WINDOW_INSTANCE_PROPERTY.GetValue(null);
        REFRESH_TIMELINE_WINDOW_METHOD.Invoke(timelineWindow,new object[] {true});
    }


//---------------------------------------------------------------------------------------------------------------------

    static MovieRecorderSettings CreateMovieRecorderSettings() {
        object obj = CREATE_DEFAULT_RECORDER_SETTINGS_METHOD.Invoke(null,new object[] {typeof(MovieRecorderSettings)});
        MovieRecorderSettings recorderSettings = obj as MovieRecorderSettings;
        if (null == recorderSettings) {
            Debug.LogError("[AnimeToolbox] Failed in creating MovieRecorderSettings");
            return null;
        }

        GameViewInputSettings inputSettings = recorderSettings.imageInputSettings as GameViewInputSettings;
        if (null == inputSettings) {
            Debug.LogError("[AnimeToolbox] Failed in getting GameViewInputSettings");
            return null;
        }
        
        OUTPUT_IMAGE_HEIGHT_PROPERTY.SetValue(inputSettings, IMAGE_HEIGHT_1440_QHD);
        return recorderSettings;
    }

//---------------------------------------------------------------------------------------------------------------------

    //Recorder Reflection
    internal static Assembly RECORDER_EDITOR_ASSEMBLY = Assembly.GetAssembly(typeof(RecorderClip));
    internal static Type RECORDERS_INVENTORY_TYPE = RECORDER_EDITOR_ASSEMBLY.GetType("UnityEditor.Recorder.RecordersInventory");
    internal static MethodInfo CREATE_DEFAULT_RECORDER_SETTINGS_METHOD 
        = RECORDERS_INVENTORY_TYPE.GetMethod("CreateDefaultRecorderSettings", BindingFlags.Static | BindingFlags.NonPublic);

    internal static Type GAME_VIEW_INPUT_SETTINGS_TYPE = typeof(GameViewInputSettings);
    internal static PropertyInfo OUTPUT_IMAGE_HEIGHT_PROPERTY
        = GAME_VIEW_INPUT_SETTINGS_TYPE.GetProperty("outputImageHeight", BindingFlags.Instance | BindingFlags.NonPublic);

    internal static Type IMAGE_HEIGHT_TYPE = RECORDER_EDITOR_ASSEMBLY.GetType("UnityEditor.Recorder.ImageHeight");
    internal static object IMAGE_HEIGHT_1440_QHD = System.Enum.Parse(IMAGE_HEIGHT_TYPE, "x1440p_QHD");    

        

    //Timeline Reflection
    internal static Assembly TIMELINE_EDITOR_ASSEMBLY = Assembly.GetAssembly(typeof(TimelineEditor));
    internal static Type TIMELINE_WINDOW_TYPE = TIMELINE_EDITOR_ASSEMBLY.GetType("UnityEditor.Timeline.TimelineWindow");
    internal static PropertyInfo TIMELINE_WINDOW_INSTANCE_PROPERTY
        = TIMELINE_WINDOW_TYPE.GetProperty("instance", BindingFlags.Static | BindingFlags.Public);
    internal static MethodInfo REFRESH_TIMELINE_WINDOW_METHOD 
        = TIMELINE_WINDOW_TYPE.GetMethod("RefreshSelection", BindingFlags.NonPublic | BindingFlags.Instance);

//---------------------------------------------------------------------------------------------------------------------

}

}
