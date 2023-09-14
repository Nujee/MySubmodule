namespace Code.BlackCubeSubmodule.Editor.BlackCubeMenu.PreBuildSetup
{
    public static class PreBuildMessages
    {
        public const string VersionError = "Version and Android bundle code may not match!";
        public const string ManifestError = "Android manifest has debuggable set to true;";
        public const string ManifestNotFoundError = "Android manifest was not found";
        public const string GameAnalyticsVersionError = "Game version and GameAnalytics version doesn't match!";
        public const string CompanyNameError = "Company name is wrong!";
        public const string GameNameError = "Game name may be wrong!";
        public const string LevelsError = "Not enough levels in the build settings!";

        public const string TestingStartedMessage = "Starting prebuild tests";
        public const string SuccessMessage = "All tests were successful";
        public const string FailMessage = "Some tests failed";
        public const string WrongPlatformMessage = "Target Platform is wrong!";
    }
}