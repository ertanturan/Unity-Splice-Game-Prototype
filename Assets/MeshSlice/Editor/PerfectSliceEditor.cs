using UnityEngine;
using UnityEditor;

namespace MeshSlice
{
  public class PerfectSliceEditor
  {
    [MenuItem("Mesh Slice/Levels", false, -1)]
    private static void FindSkinSettings()
    {
      SelectUtilByType(nameof(LevelsSettings));
    }

    [MenuItem("Mesh Slice/Documentation/Asset", false, 0)]
    private static void FindDocumentation()
    {
      SelectUtilByName("DocumentationAsset");
    }

    [MenuItem("Mesh Slice/Documentation/Framework", false, 0)]
    private static void FindFrameworkDocumentation()
    {
      SelectUtilByName("DocumentationFramework");
    }

    [MenuItem("Mesh Slice/Choose Level/Level 1", false, 1)]
    private static void Level1()
    {
      PlayerPrefs.SetInt("level", 0);
    }

    [MenuItem("Mesh Slice/Choose Level/Level 2", false, 1)]
    private static void Level2()
    {
      PlayerPrefs.SetInt("level", 1);
    }

    [MenuItem("Mesh Slice/Choose Level/Level 3", false, 1)]
    private static void Level3()
    {
      PlayerPrefs.SetInt("level", 2);
    }

    [MenuItem("Mesh Slice/Choose Level/Level 4", false, 1)]
    private static void Level4()
    {
      PlayerPrefs.SetInt("level", 3);
    }

    [MenuItem("Mesh Slice/Choose Level/Level 5", false, 1)]
    private static void Level5()
    {
      PlayerPrefs.SetInt("level", 4);
    }

    private static void SelectUtilByType(string className)
    {
      foreach (string guid in AssetDatabase.FindAssets($"t:{className}"))
      {
        SelectUtil(guid);
      }
    }

    private static void SelectUtilByName(string name)
    {
      foreach (string guid in AssetDatabase.FindAssets(name, new string[] {"Assets"}))
      {
        SelectUtil(guid);
      }
    }

    private static void SelectUtil(string guid)
    {
      Selection.activeInstanceID = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GUIDToAssetPath(guid)).GetInstanceID();
    }
  }
}
