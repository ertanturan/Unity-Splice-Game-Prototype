using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace MeshSlice.UI
{
  public class Finish : CanvasElement
  {
    [Header("References")]
    public Base background;
    public Base passedText;
    public BaseText hpText;
    public Base tapToReplay;

    private bool isHpFilled;

    public override void Subscribe()
    {
      Events.GameFinish += Show;
      Events.PreReset += Hide;
      Events.HpChanged += UpdateHpText;
      Events.HpFilled += OnHpFilled;
      Events.PointerUp += OnPointerUp;
    }

    public override void Unsubscribe()
    {
      Events.GameFinish -= Show;
      Events.PreReset -= Hide;
      Events.HpChanged -= UpdateHpText;
      Events.HpFilled -= OnHpFilled;
      Events.PointerUp -= OnPointerUp;
    }

    private void OnHpFilled()
    {
      isHpFilled = true;
      ShowTapToReplay();
    }

    private void OnPointerUp()
    {
      if (isHpFilled)
      {
        isHpFilled = false;
        Events.RequestSplash.Call();
      }
    }

    private void ShowTapToReplay()
    {
      tapToReplay.Sequence(
        tapToReplay.Fade(1, 0.2f).SetEase(Ease.InSine),
        tapToReplay.Fade(0, 1).SetEase(Ease.InSine)
      ).SetLoops(-1);
    }

    protected override void OnStartShowing()
    {
      background.SetFade(0);
      passedText.SetFade(0);
      hpText.SetFade(0);
      tapToReplay.SetFade(0);
    }

    protected override void OnFinishShowing()
    {
      ShowBackground();
      ShowPassedText();
      ShowHP();
    }

    private void ShowBackground()
    {
      background.Sequence(
        background.Fade(0.4f, 0.2f).SetEase(Ease.InSine),
        OnFinish(() => Events.RequestHpFill.Call())
      );
    }

    private void ShowPassedText()
    {
      passedText.Sequence(
        passedText.Fade(1, 0.2f).SetEase(Ease.InSine)
      );
    }

    private void ShowHP()
    {
      UpdateHpText();
      hpText.Sequence(
        hpText.Fade(1, 0.2f).SetEase(Ease.InSine)
      );
    }

    private void UpdateHpText()
    {
      hpText.SetText($"XP: {HPManager.GetHP()}");
    }
  }
}
