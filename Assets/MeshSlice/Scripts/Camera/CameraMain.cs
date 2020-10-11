using UnityEngine;

using LightDev;
using LightDev.Core;

using DG.Tweening;

namespace MeshSlice
{
  public class CameraMain : Base
  {
    public CameraState startState;
    public CameraState gameState;
    public CameraState finishState;

    private new Camera camera;

    private void Awake()
    {
      camera = GetComponent<Camera>();

      ChangeStateInstantly(startState);

      Events.PreReset += OnPreReset;
      Events.GameStart += OnGameStart;
      Events.GameFinish += OnGameFinish;
      Events.SuccessfulSlice += OnSuccessfulCut;
    }

    private void OnDestroy()
    {
      Events.PreReset -= OnPreReset;
      Events.GameStart -= OnGameStart;
      Events.GameFinish -= OnGameFinish;
      Events.SuccessfulSlice -= OnSuccessfulCut;
    }

    private void OnPreReset()
    {
      ChangeState(startState);
    }

    private void OnGameStart()
    {
      ChangeState(gameState);
    }

    private void OnGameFinish()
    {
      ChangeState(finishState);
    }

    private void OnSuccessfulCut(int left, int right)
    {
      Sequence(
        camera.DOShakePosition(.4f, .1f, 14, 45)
      );
    }

    private void ChangeStateInstantly(CameraState state)
    {
      KillSequences();
      SetLocalPosition(state.position);
      SetLocalRotation(state.rotation);
      camera.fieldOfView = state.fov;
    }

    private void ChangeState(CameraState state)
    {
      KillSequences();
      Sequence(
        MoveLocal(state.position, state.duration).SetEase(state.ease)
      );
      Sequence(
        RotateLocal(state.rotation, state.duration).SetEase(state.ease)
      );
      Sequence(
        camera.DOFieldOfView(state.fov, state.duration).SetEase(state.ease)
      );
    }
  }
}
