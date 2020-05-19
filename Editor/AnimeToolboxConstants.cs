using System.IO;

namespace Unity.AnimeToolbox.Editor {

internal static class AnimeToolboxConstants  {
    public const int TOOL_BUTTON_LENGTH = 32;
    public const int TOOLBAR_HEIGHT = 24;
    public const string PACKAGE_NAME = "com.unity.anime-toolbox";
    public static readonly string PACKAGE_PATH = Path.Combine("Packages", PACKAGE_NAME);
    public static readonly string COMPOSITION_LAYOUT_FULL_PATH = Path.GetFullPath(Path.Combine(PACKAGE_PATH,"Editor/Layouts/CompositionLayout.window"));
    public const string MENU_ITEM = "Window/Anime Toolbox";
    

}

}
