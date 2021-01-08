# Changelog

## [0.4.0-preview] - 2020-1-8

* feat: add a PackageVersion class to parse package version (semver) 
* chore: change the class names of PackageRequest related classes

## [0.3.1-preview] - 2020-12-14

* chore: include UIElements as a dependency of AnimeToolbox
* chore: cleanup internal functions 

## [0.3.0-preview] - 2020-10-29

* feat: add ObjectExtensions, RenderTextureExtensions, Texture2DExtensions classes 
* feat: add PathUtility::GenerateUniqueFolder() utility function
* feat: add a notifier to notify users to restart Unity after script compilation


## [0.2.1-preview] - 2020-10-13

* chore: remove unsupported/unused window

## [0.2.0-preview] - 2020-10-1

* feat: add utility functions from StreamingImageSequence
* feat: add utility functions from MeshSync (AssetUtility, AssetEditorUtility, EditorGUIDrawerUtility) 
* chore: delete unused legacy functions
* chore: test com.unity.anime-toolbox against Unity 2020 and 2021
* chore: remove dependency to recorder. No longer required.
* chore: use new Yamato conf template and reapply the existing settings
* fix: package warnings
* doc: add package badge in the top readme


## [0.1.6-preview] - 2020-08-26

* fix: fix test code on Linux
* fix: fix doc warnings
* chore: update package info 

## [0.1.5-preview] - 2020-08-14

* fix: remove obsolete/unsupported tracks from the menu

## [0.1.4-preview] - 2020-07-27

* make UIElementsUtility into a public class 

## [0.1.3-preview] - 2020-07-27

* fix build error when building applications
* add UIElementsUtility which provides several utility UIElements-related utility functions
* add more error handling in FileUtility 

## [0.1.2-preview] - 2020-05-20

* fix: open UIElementsEditorUtility to public	
* fix: Open PathUtility functions to public
* chore: rename runtime assembly to Unity.AnimeToolbox without Runtime
* test: add PathUtilityTest for testing PathUtility

## [0.1.1-preview] - 2020-05-20

* fix: change dependency of com.unity.recorder to version 2.1.0-preview.1


## [0.1.0-preview] - 2020-05-19

* feat: add new utility scripts (FileUtility, PathUtility, UIElementsEditorUtility)
* feat: Add PackageRequest classes 
* chore: rename editor namespace to Unity.AnimeToolbox.Editor

## [0.0.2-preview] - 2020-04-08

* The first release of *Anime Toolbox \<com.unity.anime-toolbox\>*.

