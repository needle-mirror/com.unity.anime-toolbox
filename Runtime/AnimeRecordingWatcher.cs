using UnityEngine;
using UnityEngine.Playables;

namespace Unity.AnimeToolbox {

#if UNITY_EDITOR

internal class AnimeRecordingWatcher : MonoBehaviour {


    private void Awake() {          
        m_activePlayableDirectors = GameObject.FindObjectsOfType<PlayableDirector>();
    }

//---------------------------------------------------------------------------------------------------------------------

    void Update() {
        //Check if any playableDirector is playing 
        foreach(PlayableDirector playableDirector in m_activePlayableDirectors) {
            if (playableDirector.state == PlayState.Playing) {
                return;
            }
        }

        //Stop
        UnityEditor.EditorApplication.ExitPlaymode();
       
    }

//---------------------------------------------------------------------------------------------------------------------

    static PlayableDirector[] m_activePlayableDirectors = null;
}

#endif //UNITY_EDITOR

} //End namespace
