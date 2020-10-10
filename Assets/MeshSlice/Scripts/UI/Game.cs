using UnityEngine.UI;
using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace MeshSlice.UI
{
  public class Game : CanvasElement
  {
    [Header("References")]
    public Base holder;
    public Base progress;

    [Header("Text")]
    public BaseText levelText;
    public BaseText progressText;

    [Header("Progress")]
    public Image progressImage;

    public override void Subscribe()
    {
      Events.GameStart += Show;
      Events.PreReset += Hide;
      Events.ProgressChanged += OnProgressChanged;
      Events.GameFinish += OnGameFinish;
    }

    public override void Unsubscribe()
    {
      Events.GameStart -= Show;
      Events.PreReset -= Hide;
      Events.ProgressChanged -= OnProgressChanged;
      Events.GameFinish -= OnGameFinish;
    }

    private void OnGameFinish()
    {
      progress.Sequence(
        progress.MoveY(-900, 0.4f).SetEase(Ease.InOutSine)
      );
    }

    private void OnProgressChanged()
    {
      UpdateProgress();
    }

    private void UpdateProgress()
    {
      progressImage.fillAmount = (float)HPManager.GetCurrentProgress() / HPManager.GetMaxProgress();
      progressText.SetText($"{HPManager.GetCurrentProgress()}/{HPManager.GetMaxProgress()}");
    }

    protected override void OnStartShowing()
    {
      UpdateProgress();
      levelText.SetText($"Level: {LevelsManager.GetLevel()}");

      holder.SetPositionY(500);
      progress.SetPositionY(-400);
    }

    protected override void OnFinishShowing()
    {
      holder.Sequence(
        holder.MoveY(0, 0.4f).SetEase(Ease.OutBack)
      );
    }
  }
}
