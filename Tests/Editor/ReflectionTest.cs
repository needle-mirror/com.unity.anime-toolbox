using NUnit.Framework;
using Unity.AnimeToolbox.Editor;

namespace Unity.AnimeToolbox.EditorTests {
internal class ReflectionTest {


//----------------------------------------------------------------------------------------------------------------------

    [Test]
    public void WindowLayoutReflectionTest() {
        Assert.IsNotNull(LayoutUtility.LOAD_WINDOW_LAYOUT_METHOD);
        Assert.IsNotNull(LayoutUtility.SAVE_WINDOW_LAYOUT_METHOD);
    }
}
}
