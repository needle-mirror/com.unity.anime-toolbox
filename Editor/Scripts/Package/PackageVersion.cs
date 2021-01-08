using System;

namespace Unity.AnimeToolbox.Editor {

/// <summary>
/// A class that holds the version (semantic versioned) of a package in a structure
/// </summary>
public class PackageVersion {
    public PackageVersion() {
        
    }
    
//----------------------------------------------------------------------------------------------------------------------
    public PackageVersion(string semanticVer) {
        if (!TryParse(semanticVer, out PackageVersion temp))
            return;
        
        this.Major     = temp.Major;
        this.Minor     = temp.Minor;
        this.Patch     = temp.Patch;
        this.Lifecycle = temp.Lifecycle;
        this.AdditionalMetadata = temp.AdditionalMetadata;
    }

//----------------------------------------------------------------------------------------------------------------------
    
    public override string ToString() {
        string ret = $"{Major}.{Minor}.{Patch}";

        switch (this.Lifecycle) {
            case PackageLifecycle.RELEASED: break;
            case PackageLifecycle.PRERELEASE: ret += "-pre"; break;
            case PackageLifecycle.PREVIEW:
            case PackageLifecycle.EXPERIMENTAL: {
                ret += "-" + Lifecycle.ToString().ToLower();
                break;                
            }
            default: break;
        }       
        
        if (!string.IsNullOrEmpty(AdditionalMetadata)) {
            ret += "." + AdditionalMetadata;
        }

        return ret;

    }
    
//----------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Parse a semantic versioned string to a PackageVersion class
    /// </summary>
    /// <param name="semanticVer">Semantic versioned input string</param>
    /// <param name="packageVersion">The detected PackageVersion. Set to null when the parsing fails</param>
    /// <returns>true if successful, false otherwise</returns>
    public static bool TryParse(string semanticVer, out PackageVersion packageVersion) {
        packageVersion = null;
        string[] tokens = semanticVer.Split('.');
        if (tokens.Length <= 2)
            return false;

        if (!int.TryParse(tokens[0], out int major))
            return false;

        if (!int.TryParse(tokens[1], out int minor))
            return false;

        //Find patch and lifecycle
        string[] patches = tokens[2].Split('-');
        if (!int.TryParse(patches[0], out int patch))
            return false;
               
        PackageLifecycle lifecycle = PackageLifecycle.INVALID;
        if (patches.Length > 1) {
            string lifecycleStr = patches[1].ToLower();                    
            switch (lifecycleStr) {
                case "experimental": lifecycle = PackageLifecycle.EXPERIMENTAL; break;
                case "preview"     : lifecycle = PackageLifecycle.PREVIEW; break;
                case "pre"         : lifecycle = PackageLifecycle.PRERELEASE; break;
                default: lifecycle             = PackageLifecycle.INVALID; break;
            }
            
        } else {
            lifecycle = PackageLifecycle.RELEASED; 
            
        }

        packageVersion = new PackageVersion() {
            Major     = major,
            Minor     = minor,
            Patch     = patch,
            Lifecycle = lifecycle
        };

        const int METADATA_INDEX = 3;
        if (tokens.Length > METADATA_INDEX) {
            packageVersion.AdditionalMetadata = String.Join(".",tokens, METADATA_INDEX, tokens.Length-METADATA_INDEX);
        }

        return true;

    } 
    
//----------------------------------------------------------------------------------------------------------------------
    public int              Major;
    public int              Minor;
    public int              Patch;
    public PackageLifecycle Lifecycle;
    public string           AdditionalMetadata;
    
    
    

}

}

