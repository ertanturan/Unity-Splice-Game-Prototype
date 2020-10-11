using UnityEngine;

using LightDev;

namespace MeshSlice
{
  public class GameManager : MonoBehaviour
  {
    private static bool isGameStarted;

    private void Awake()
    {
      Events.SceneLoaded += Reset;
      Events.PointerUp += OnPointerUp;
      Events.RequestFinish += OnRequestFinish;
      Events.RequestReset += Reset;
    }

    private void OnDestroy()
    {
      Events.SceneLoaded -= Reset;
      Events.PointerUp -= OnPointerUp;
      Events.RequestFinish -= OnRequestFinish;
      Events.RequestReset -= Reset;
    }

    private void OnRequestFinish()
    {
      FinishGame();
    }

    private void OnPointerUp()
    {
      if (isGameStarted == false)
      {
        StartGame();
      }
    }

    private void Reset()
    {
      isGameStarted = false;

      Events.PreReset.Call();
      Events.PostReset.Call();
    }

    private void StartGame()
    {
      isGameStarted = true;
      Events.GameStart.Call();
    }

    private void FinishGame()
    {
      Events.GameFinish.Call();
    }
  }
}
