using UnityEngine;

using LightDev;
using LightDev.Core;

using DG.Tweening;

namespace MeshSlice
{
  public class HPManager : Base
  {
    private const int XpForSlicedObject = 50;
    private const string XpKey = "xp";

    private static int currentProgress;
    private static int maxProgress;

    public static int GetHP() { return PlayerPrefs.GetInt(XpKey, 0); }

    public static int GetCurrentProgress() { return currentProgress; }
    public static int GetMaxProgress() { return maxProgress; }
    public static void IncreaseProgress(float value) { SetProgress(GetCurrentProgress() + (int)(value * XpForSlicedObject)); }

    private static void SetHP(int value) { PlayerPrefs.SetInt(XpKey, value); Events.HpChanged.Call(); }
    private static void SetProgress(int value) { currentProgress = value; Events.ProgressChanged.Call(); }

    private void Awake()
    {
      Events.PostReset += OnPostReset;
      Events.RequestHpFill += OnRequestHpFill;
    }

    private void OnDestroy()
    {
      Events.PostReset -= OnPostReset;
      Events.RequestHpFill -= OnRequestHpFill;
    }

    private void OnPostReset()
    {
      currentProgress = 0;
      maxProgress = LevelsManager.GetMeshesCountOnLevel() * XpForSlicedObject;
    }

    private void OnRequestHpFill()
    {
      int progress = currentProgress;
      int hp = GetHP();
      Sequence(
        Delay(0.5f),
        DOTween.To((value) =>
        {
          int p = (int)Mathf.Lerp(0, progress, value);
          SetProgress(progress - p);
          SetHP(hp + p);
        }, 0, 1, 1),
        Delay(0.5f),
        OnFinish(() => Events.HpFilled.Call())
      );
    }
  }
}
