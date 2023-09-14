using System.IO;
using System.Linq;
using Code.BlackCubeSubmodule.Utility.Constants;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

public sealed class AndroidTests
{
    /// <summary>
    /// Checks if bundle version was increased.
    /// </summary>
    [Test]
    public void BundleVersionCodeTest()
    {
        // TODO: sometimes the test fails, you need to follow carefully on your own, if you manage to catch an error, then fix it 
        if (!PlayerPrefs.HasKey(Keys.BundleVersionCode)) return;
        var bundleVersionCode = PlayerPrefs.GetInt(Keys.BundleVersionCode);

        Assert.IsTrue(PlayerSettings.Android.bundleVersionCode > bundleVersionCode);
    }
    
    /// <summary>
    /// Checks if bundle name matches game name. 
    /// </summary>
    [Test]
    public void BundleNameMatchesGameNameTest()
    {
        var gameName = PlayerSettings.productName;
        gameName = new string(gameName.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
        gameName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(gameName.ToLower());
        var bundleName = Application.identifier;
        var expectedBundleName = Names.BundleNameFirstPart + gameName;

        Assert.AreEqual(expectedBundleName, bundleName);
    }
    
    [Test]
    public void CheckDebuggableInManifest()
    {
        var path = Application.dataPath + "/Plugins/Android/AndroidManifest.xml";
        using var stream = new StreamReader(path);
        var androidManifest = stream.ReadToEnd();
        var manifestContainsDebuggable = androidManifest.Contains("android:debuggable");

        Assert.IsFalse(manifestContainsDebuggable);
    }

    [Test]
    public void TargetArchitecturesTest()
    {
        var requiredArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        var targetArchitectures = PlayerSettings.Android.targetArchitectures;
        
        Assert.AreEqual(requiredArchitectures, targetArchitectures);
    }

    [Test]
    public void ScriptingBackendTest()
    {
        var scriptingBackend = PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android);
        
        Assert.AreEqual(ScriptingImplementation.IL2CPP, scriptingBackend);
    }
    
    /// <summary>
    /// Checks if Android minimum api level is set correctly.
    /// </summary>
    [Test]
    public void MinimumAPILevelTest()
    {
        var minAPILevel = PlayerSettings.Android.minSdkVersion;

        Assert.AreEqual(AndroidSdkVersions.AndroidApiLevel22, minAPILevel);
    }
    
    /// <summary>
    /// Checks if Android target api level is set correctly.
    /// </summary>
    [Test]
    public void MaximumAPILevelTest()
    {
        var maxAPILevel = (int)PlayerSettings.Android.targetSdkVersion;
        
        Assert.AreEqual(31, maxAPILevel);
    }

    // /// <summary>
    // /// Checks if graphical API's parameters are set correctly. 
    // /// </summary>
    // [Test]
    // public void GraphicalAPISettingsTest()
    // {
    //             
    // }
}