using UnityEngine;
using LightDev;

namespace MeshSlice
{
  public class LevelsManager : MonoBehaviour
  {
    public LevelsSettings levels;

    [Header("Random Levels")]
    public int minRandomMeshes = 4;
    public int maxRandomMeshes = 5;
    public Mesh[] randomMeshes;

    private static int currentLevelIndex;
    private static int nextMeshIndex;
    private static int randomMeshesCount;
    private static LevelInfo[] s_levels;
    private static ExlusiveRandom<Mesh> meshRandomer;

    private const string levelKey = "level";

    private void OnValidate()
    {
      minRandomMeshes = Mathf.Max(minRandomMeshes, 1);
      maxRandomMeshes = Mathf.Max(maxRandomMeshes, minRandomMeshes);
    }

    private void Awake()
    {
      s_levels = levels.levels;
      meshRandomer = new ExlusiveRandom<Mesh>(randomMeshes);

      Events.PreReset += OnPreReset;
      Events.GameFinish += OnGameFinish;
    }

    private void OnDestroy()
    {
      Events.PreReset -= OnPreReset;
      Events.GameFinish -= OnGameFinish;
    }

    private void OnPreReset()
    {
      currentLevelIndex = PlayerPrefs.GetInt(levelKey, 0);
      randomMeshesCount = Random.Range(minRandomMeshes, maxRandomMeshes + 1);
      nextMeshIndex = 0;
    }

    private void OnGameFinish()
    {
      PlayerPrefs.SetInt(levelKey, currentLevelIndex + 1);
    }

    public static int GetLevel()
    {
      return currentLevelIndex + 1;
    }

    public static int GetMeshesCountOnLevel()
    {
      return IsRandomLevel() ? randomMeshesCount : GetCurrentLevelMeshes().Length;
    }

    public static bool HasNextMesh()
    {
      return nextMeshIndex != GetMeshesCountOnLevel();
    }

    public static MeshInfo GetNextMeshInfo()
    {
      nextMeshIndex++;
      return IsRandomLevel() ? new MeshInfo(meshRandomer.GetNext(), new Vector3(0, Random.Range(0, 360), 0)) : GetCurrentLevelMeshes()[nextMeshIndex - 1];
    }

    private static MeshInfo[] GetCurrentLevelMeshes()
    {
      return s_levels[currentLevelIndex % s_levels.Length].meshes;
    }

    private static bool IsRandomLevel()
    {
      return currentLevelIndex >= s_levels.Length;
    }
  }
}
