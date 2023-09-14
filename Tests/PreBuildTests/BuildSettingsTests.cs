using Code.BlackCubeSubmodule.Utility.Constants;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

public sealed class BuildSettingsTests
{
    
    /// <summary>
    /// Checks if game is set to run in full screen mode.  
    /// </summary>
    [Test]
    public void FullScreenModeTest()
    {
        Assert.AreEqual(FullScreenMode.FullScreenWindow, PlayerSettings.fullScreenMode);
        Assert.IsFalse(PlayerSettings.resizableWindow);
    }
    
    /// <summary>
    /// Checks if default icon in playerSettings is assigned.
    /// </summary>
    [Test]
    public void IconTest()
    {
        var icons = PlayerSettings.GetIcons(NamedBuildTarget.Unknown, IconKind.Any);
        Assert.AreNotEqual(null, icons[0]);
    }
    
    /// <summary>
    /// Checks if default icon has 1024 x 1024 pix resolution. 
    /// </summary>
    [Test]
    public void DefaultIconResolutionTest()
    {
        var icons = PlayerSettings.GetIcons(NamedBuildTarget.Unknown, IconKind.Any);
        if (icons[0] != null)
        {
            Assert.AreEqual(1024, icons[0].height);
            Assert.AreEqual(1024, icons[0].width);
        }
    }
    
    [Test]
    public void ScreenOrientationTests()
    {
        Assert.AreEqual(UIOrientation.Portrait, PlayerSettings.defaultInterfaceOrientation);
    }
    
    /// <summary>
    /// Checks if game name was changed from default GameBase name. 
    /// </summary>
    [Test]
    public void GameNameNotDefaultTest()
    {
        Assert.AreNotEqual(Names.DefaultGameName, PlayerSettings.productName);
    }
    
    /// <summary>
    /// Checks if company name is correct. 
    /// </summary>
    [Test]
    public void CompanyNameTest()
    {
        Assert.AreEqual(Names.Company, PlayerSettings.companyName);
    }
    
    /// <summary>
    /// Checks if game version was increased.
    /// </summary>
    [Test]
    public void GameVersionTest()
    {
        // TODO: sometimes the test fails, you need to follow carefully on your own, if you manage to catch an error, then fix it
        if (!PlayerPrefs.HasKey(Keys.GameVersion)) return;
        var gameVersionString = PlayerPrefs.GetString(Keys.GameVersion);
        var savedGameVersion = float.Parse(gameVersionString);
        var currentGameVersion = float.Parse(PlayerSettings.bundleVersion);
        

        Assert.IsTrue(currentGameVersion > savedGameVersion);
    }

    [Test]
    public void IsAutoRotateAllowedTest()
    {
        Assert.IsFalse(PlayerSettings.allowedAutorotateToLandscapeRight);
        Assert.IsFalse(PlayerSettings.allowedAutorotateToLandscapeLeft);
        Assert.IsFalse(PlayerSettings.allowedAutorotateToPortraitUpsideDown);
    }

    [Test]
    public void ScenesInBuildSettingsTest()
    {
        Assert.AreEqual(1, EditorBuildSettings.scenes.Length);
    }
}