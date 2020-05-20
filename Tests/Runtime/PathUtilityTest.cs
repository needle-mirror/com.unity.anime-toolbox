﻿using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity.AnimeToolbox.Tests {
    internal class PathUtilityTest
    {
        //TODO-sin: Move this to com.unity.anime-toolbox
        [Test]
        [UnityPlatform(RuntimePlatform.OSXEditor)]
        public void GetDirectoryNamesOnOSX() {

            string dirName = null;
            dirName = PathUtility.GetDirectoryName("/Applications/Unity 2019/Unity.app", 1);
            Assert.AreEqual("/Applications/Unity 2019", dirName);
            dirName = PathUtility.GetDirectoryName("/Applications/Unity 2019/Unity.app/Contents/MacOS/Unity", 4);
            Assert.AreEqual("/Applications/Unity 2019", dirName);
            
            //Null checks
            dirName = PathUtility.GetDirectoryName("/Applications/Unity 2019/Unity.app", 4);
            Assert.IsNull(dirName);
            dirName = PathUtility.GetDirectoryName(null);
            Assert.IsNull(dirName);
            
        }
        
//----------------------------------------------------------------------------------------------------------------------

        [Test]
        [UnityPlatform(RuntimePlatform.WindowsEditor)]
        public void GetDirectoryNamesOnWindows() {

            string dirName = null;
            dirName = PathUtility.GetDirectoryName(@"C:\Program Files\Unity 2019\Unity.exe", 1);
            Assert.AreEqual(@"C:\Program Files\Unity 2019", dirName);
            dirName = PathUtility.GetDirectoryName(@"C:\Program Files\Unity 2019\Contents\Images", 3);
            Assert.AreEqual(@"C:\Program Files", dirName);
            
            //Null checks
            dirName = PathUtility.GetDirectoryName(@"C:\Program Files\Unity 2019\Unity.exe", 4);
            Assert.IsNull(dirName);
            dirName = PathUtility.GetDirectoryName(null);
            Assert.IsNull(dirName);
            
        }

//----------------------------------------------------------------------------------------------------------------------
        
        [Test]
        [UnityPlatform(RuntimePlatform.LinuxEditor)]
        public void GetDirectoryNamesOnLinux() {

            string dirName = null;
            dirName = PathUtility.GetDirectoryName("/home/Unity/Unity 2019/Unity", 1);
            Assert.AreEqual("/Applications/Unity 2019", dirName);
            dirName = PathUtility.GetDirectoryName("/home/Unity/Unity 2019/bin/Unity", 2);
            Assert.AreEqual("/Applications/Unity 2019", dirName);
            
            //Null checks
            dirName = PathUtility.GetDirectoryName("/home/Unity/Unity 2019/Unity", 4);
            Assert.IsNull(dirName);
            dirName = PathUtility.GetDirectoryName(null);
            Assert.IsNull(dirName);
            
        }
    }
    
}
