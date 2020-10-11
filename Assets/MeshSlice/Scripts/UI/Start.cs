using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace MeshSlice.UI
{
  public class Start : CanvasElement
  {
    [Header("References")]
    public Base tapToStart;
    public Base logoHolder;

    [Header("Info Text")]
    public Base knob;
    public float knobWidth;
    public BaseText levelText;
    public BaseText hpText;

    public override void Subscribe()
    {
      Events.PreReset += Show;
      Events.GameStart += Hide;
    }

    public override void Unsubscribe()
    {
      Events.PreReset -= Show;
      Events.GameStart -= Hide;
    }

    protected override void OnStartShowing()
    {
      UpdateTexts();

      ShowTapToStart();
      ShowLogoHolder();
    }

    protected override void OnStartHiding()
    {
      HideTapToStart();
      HideLogoHolder();
    }

    private void UpdateTexts()
    {
      hpText.SetText($"xp: {HPManager.GetHP()}");
      levelText.SetText($"level: {LevelsManager.GetLevel()}");

      float hpWidth = hpText.GetTextComponent().preferredWidth;
      float levelWidth = levelText.GetTextComponent().preferredWidth;

      float fullSize = hpWidth + knobWidth + levelWidth;

      float hpPos = -fullSize / 2 + hpWidth / 2;
      float knobPos = hpPos + hpWidth / 2 + knobWidth / 2;
      float levelPos = knobPos + knobWidth / 2 + levelWidth / 2;

      hpText.SetPositionX(hpPos);
      knob.SetPositionX(knobPos);
      levelText.SetPositionX(levelPos);
    }

    private void ShowTapToStart()
    {
      tapToStart.SetFade(1);
      tapToStart.Sequence(
          tapToStart.Fade(0, 1).SetEase(Ease.InSine),
          tapToStart.Fade(1, 0.2f).SetEase(Ease.InSine)
      ).SetLoops(-1);
    }

    private void ShowLogoHolder()
    {
      logoHolder.SetPositionY(500);
      logoHolder.Sequence(
          logoHolder.MoveY(-354, 0.5f).SetEase(Ease.OutBack)
      );
    }

    private void HideLogoHolder()
    {
      logoHolder.KillSequences();
      logoHolder.Sequence(
        logoHolder.MoveY(500, 0.3f).SetEase(Ease.OutBack)
    );
    }

    private void HideTapToStart()
    {
      tapToStart.KillSequences();
      tapToStart.Sequence(
          tapToStart.Fade(0, 0.2f).SetEase(Ease.InSine)
      );
    }
  }
}
