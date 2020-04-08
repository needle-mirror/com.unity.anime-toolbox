using System.IO;

namespace Unity.AnimeToolbox {
    internal static class FileAttributesExtension {

        public static void RemoveAttribute(this FileAttributes attributes, FileAttributes attributesToRemove) {
            attributes &=~attributesToRemove;
        }

    }

}

