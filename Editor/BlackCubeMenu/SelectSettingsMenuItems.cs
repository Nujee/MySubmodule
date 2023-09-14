#if UNITY_EDITOR
using Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Code.BlackCubeSubmodule.Editor.BlackCubeMenu
{
    public static class SelectSettingsMenuItems
    {
        [MenuItem("BlackCube/Select Names Config", priority = 100)]
        public static void SelectNamesSettings() => Select<NamesConfig>();

        [MenuItem("BlackCube/Select Build Config", priority = 101)]
        public static void SelectBuildSettings() => Select<BuildConfig>();
        
        [MenuItem("BlackCube/Select Game Config", priority = 102)]
        public static void SelectGameConfig() => Select<GameConfig>();
        
        [MenuItem("BlackCube/Select Ui Pools Config", priority = 103)]
        public static void SelectUiPrefabsConfig() => Select<UiPoolsConfig>();

        [MenuItem("BlackCube/Select ECS Config", priority = 150)]
        public static void SelectEcsConfig() => Select<ECSConfig>();

        private static void Select<T>()
        where T: Object
        {
            var guid = AssetDatabase.FindAssets(string.Format("{0} t:{0}", typeof(T).Name));
            var selection = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid[0]));
            Selection.activeObject = selection;
        }
    }
}
#endif

