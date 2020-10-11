using UnityEngine;

using LightDev;
using LightDev.UI;

using DG.Tweening;

namespace MeshSlice.UI
{
  public class SlicePercantage : CanvasElement
  {
    [Header("References")]
    public BaseText text;

    public override void Subscribe()
    {
      Events.GameStart += Show;
      Events.GameFinish += Hide;
      Events.SuccessfulSlice += OnSuccessfulCut;
    }

    public override void Unsubscribe()
    {
      Events.GameStart -= Show;
      Events.GameFinish -= Hide;
      Events.SuccessfulSlice -= OnSuccessfulCut;
    }

    protected override void OnStartShowing()
    {
      text.SetFade(0);
    }

    private void OnSuccessfulCut(int left, int right)
    {
      text.SetText($"{GetImpression(Mathf.Abs(left - right))}\n{left}/{right}");
      text.Sequence(
        text.Fade(1, 0.2f).SetEase(Ease.InSine),
        text.Delay(1),
        text.Fade(0, 0.3f).SetEase(Ease.InSine)
      );
    }

    private string[] awesome = {"AWESOME", "BEAUTIFUL", "STUNNING", "CRAZY" };
    private string[] notBad = {"NOT BAD", "GOOD", "LIKE "};
    private string[] bad = {"TRY MORE", "NOT GOOD", "UPS.." };

    private string GetImpression(int delta)
    {
      if(delta < 5)
        return awesome[Random.Range(0, awesome.Length)];
      if(delta < 15)
        return notBad[Random.Range(0, notBad.Length)];

      return bad[Random.Range(0, bad.Length)];
    }
  }
}
