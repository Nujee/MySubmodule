#if UNITY_EDITOR
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Editor.BlackCubeMenu.PreBuildSetup
{
    public sealed class GeneralPreBuildSet
    {
        private const string CompanyName = "Octavian Game Art";
        private const int MinAllowedScenes = 3;
        
        public bool SetInterfaceOrientation()
        {
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            PlayerSettings.allowedAutorotateToLandscapeLeft = false;
            PlayerSettings.allowedAutorotateToLandscapeRight = false;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;

            return true;
        }

        public bool CheckScenesInTheBuildSettings()
        {
            var scenesInBuildSetting = EditorBuildSettings.scenes.Length;
            return scenesInBuildSetting > MinAllowedScenes;
        }
        
        public bool CheckCompanyName()
        {
            return PlayerSettings.companyName == CompanyName;
        }

        public bool CheckGameName()
        {
            var productName = PlayerSettings.productName;
            var moreThanTwoWords = productName.ToList().Any(char.IsWhiteSpace);
            var hasCapitalLetters = productName.ToList().Where(char.IsUpper).Count() > 1;
            var nameMatchExpectedName = productName == GetExpectedName();
            
            return moreThanTwoWords && hasCapitalLetters && nameMatchExpectedName;
        }

        private string GetExpectedName()
        {
            var dataPathSubStrings = Application.dataPath.Split('/');
            var expectedName = AddSpacesToSentence(dataPathSubStrings[2]);

            return expectedName;
        }
        
        private string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
#endif
