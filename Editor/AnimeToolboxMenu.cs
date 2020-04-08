using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Unity.AnimeToolbox {

internal static class AnimeToolboxMenu 
{
    [MenuItem(AnimeToolboxConstants.MENU_ITEM + "/DownloadTemplate")]
    static void DownloadTemplate() {

        string templateName = "com.unity.anime-template";

        StringBuilder sb = new StringBuilder();
        sb.Append(templateName);
        sb.Append("-");
        sb.Append(VERSION);
        sb.Append(".tgz");
        string filename = sb.ToString();

        sb.Clear();
        sb.Append(GITHUB_URL);
        sb.Append("/releases/download/");
        sb.Append(VERSION);
        sb.Append("/");
        sb.Append(filename);
        string url = sb.ToString();

        Debug.LogFormat("[AnimeToolbox] Downloading template from: {0}", url);

        //Download
        WebClient client = new WebClient();
        string tmpFilePath = System.IO.Path.Combine(Application.temporaryCachePath, filename);

        client.DownloadFileCompleted += (object sender, System.ComponentModel.AsyncCompletedEventArgs e) => {
            EditorUtility.ClearProgressBar();

            if (null!=e.Error || !File.Exists(tmpFilePath)) {
                Debug.LogErrorFormat("[AnimeToolbox] Error in downloading template. url:{0}", url);
                return;
            }
            
            //Copy to the template folder
            string editorTemplatePath = Path.Combine(EditorApplication.applicationContentsPath,"Resources","PackageManager", "ProjectTemplates");
            string dest = Path.Combine(editorTemplatePath, filename);
            File.Move(tmpFilePath, dest);

            EditorUtility.DisplayDialog("Anime Toolbox", "Anime Template installed","Ok");

        };
        client.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) => {
            float progress = e.ProgressPercentage / 100f;
            if(EditorUtility.DisplayCancelableProgressBar("Anime Toolbox", "Downloading Anime Template", progress)) {
                client.CancelAsync();
            }
        };
        client.DownloadFileAsync(new System.Uri(url), tmpFilePath);
    
    }

//---------------------------------------------------------------------------------------------------------------------
    const string GITHUB_URL = "https://github.com/Unity-Technologies/AnimeSolution";
    //[TODO-sin: 2019-10-25] Find a better way to automate this
    const string VERSION = "0.0.1-preview";
}

} //end namespace