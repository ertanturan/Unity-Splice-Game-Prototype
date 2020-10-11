using UnityEngine;

namespace MeshSlice
{
  [CreateAssetMenu(fileName = "Levels", menuName = "PerfectSlice/Levels", order = 1)]
  public class LevelsSettings : ScriptableObject
  {
    public LevelInfo[] levels;

    private void OnValidate()
    {
      for (int i = 0; i < levels.Length; i++)
      {
        levels[i].name = "Level " + (i + 1);
      }
    }
  }
}
