using UnityEngine;

namespace Unity.AnimeToolbox {


/// <summary>
/// Extension methods for RenderTexture class.
/// </summary>
public static class RenderTextureExtensions {

    /// <summary>
    /// Clear the depth and the color of a RenderTexture using RGBA(0,0,0,0)
    /// </summary>
    /// <param name="rt">the target RenderTexture</param>
    public static void ClearAll(this RenderTexture rt) {
        RenderTexture prevRT = RenderTexture.active;
        RenderTexture.active = rt;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = prevRT;            
    }

    /// <summary>
    /// Clear a RenderTexture
    /// </summary>
    /// <param name="rt">the target RenderTexture</param>
    /// <param name="clearDepth">Should the depth buffer be cleared? </param>
    /// <param name="clearColor">Should the color buffer be cleared? </param>
    /// <param name="bgColor">The color to clear with, used only if clearColor is true. </param>
    public static void Clear(this RenderTexture rt, bool clearDepth, bool clearColor, Color bgColor) {
        RenderTexture prevRT = RenderTexture.active;
        RenderTexture.active = rt;
        GL.Clear(clearDepth, clearColor, bgColor);
        RenderTexture.active = prevRT;            
    }
    
}

} //end namespace