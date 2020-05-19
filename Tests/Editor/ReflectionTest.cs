using NUnit.Framework;
using Unity.AnimeToolbox.Editor;

namespace Unity.AnimeToolbox.EditorTests {
internal class ReflectionTest {
        
        
    // A Test behaves as an ordinary method
    [Test]
    public void UnityRecorderReflectionTest() {
        Assert.IsNotNull(AnimeRecorderTrack.RECORDER_EDITOR_ASSEMBLY);
        Assert.IsNotNull(AnimeRecorderTrack.RECORDERS_INVENTORY_TYPE);
        Assert.IsNotNull(AnimeRecorderTrack.CREATE_DEFAULT_RECORDER_SETTINGS_METHOD);
        Assert.IsNotNull(AnimeRecorderTrack.GAME_VIEW_INPUT_SETTINGS_TYPE);
        Assert.IsNotNull(AnimeRecorderTrack.OUTPUT_IMAGE_HEIGHT_PROPERTY);
        Assert.IsNotNull(AnimeRecorderTrack.IMAGE_HEIGHT_TYPE);
        Assert.IsNotNull(AnimeRecorderTrack.IMAGE_HEIGHT_1440_QHD);
    }

//----------------------------------------------------------------------------------------------------------------------

    [Test]
    public void UnityTimelineReflectionTest() {
        Assert.IsNotNull(AnimeRecorderTrack.TIMELINE_EDITOR_ASSEMBLY);
        Assert.IsNotNull(AnimeRecorderTrack.TIMELINE_WINDOW_TYPE);
        Assert.IsNotNull(AnimeRecorderTrack.TIMELINE_WINDOW_INSTANCE_PROPERTY);
        Assert.IsNotNull(AnimeRecorderTrack.REFRESH_TIMELINE_WINDOW_METHOD);
    }

//----------------------------------------------------------------------------------------------------------------------

    [Test]
    public void WindowLayoutReflectionTest() {
        Assert.IsNotNull(LayoutUtility.LOAD_WINDOW_LAYOUT_METHOD);
        Assert.IsNotNull(LayoutUtility.SAVE_WINDOW_LAYOUT_METHOD);
    }
}
}
