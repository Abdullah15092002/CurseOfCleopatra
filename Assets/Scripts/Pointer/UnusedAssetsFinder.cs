using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Pointer
{
    public class UnusedAssetsFinder
    {
        [MenuItem("Tools/Find Unused Assets")]
        static void FindUnusedAssets()
        {
            string[] allAssets = AssetDatabase.GetAllAssetPaths();
            foreach (string path in allAssets)
            {
                if (!path.StartsWith("Assets")) continue;
                if (AssetDatabase.GetDependencies(path).Length == 0)
                {
                    Debug.Log("Possibly unused asset: " + path);
                }
            }
        }
    }
}
