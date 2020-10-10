using UnityEngine;

using LightDev;
using LightDev.Core;

using DG.Tweening;

using SliceFramework;

namespace MeshSlice
{
  public class Cutter : Base
  {
    [Header("Cutter")]
    public Base cutter;
    public float minCutterLocalPosZ;
    public float maxCutterLocalPosZ;

    [Header("Object To Slice")]
    public GameObject objectToSlice;
    public float objectToSliceMinLocalPosZ;
    public float objectToSliceMaxLocalPosZ;

    [Header("Sliced Objects")]
    public Transform slicedObjectLeftPos;
    public Transform slicedObjectRightPos;

    [Header("Slice Parameters")]
    public Transform slicePoint;
    public Material material;

    private bool canCut;

    private void OnValidate()
    {
      maxCutterLocalPosZ = Mathf.Max(minCutterLocalPosZ, maxCutterLocalPosZ);
      objectToSliceMaxLocalPosZ = Mathf.Max(objectToSliceMinLocalPosZ, objectToSliceMaxLocalPosZ);
    }

    private void Awake()
    {
      Events.PointerUp += OnPointerUp;
      Events.PostReset += OnPostReset;
      Events.GameStart += OnGameStart;
    }

    private void OnDestroy()
    {
      Events.PointerUp -= OnPointerUp;
      Events.PostReset -= OnPostReset;
      Events.GameStart -= OnGameStart;
    }

    private void Update()
    {
      if (canCut)
      {
        float cutterLocalZ = cutter.GetLocalPositionZ() - InputManager.GetHorizontal();
        cutterLocalZ = Mathf.Clamp(cutterLocalZ, minCutterLocalPosZ, maxCutterLocalPosZ);
        cutter.SetLocalPositionZ(cutterLocalZ);
      }
    }

    private void OnPostReset()
    {
      canCut = false;
      objectToSlice.SetActive(false);
      AnimateCutterToStartState();
    }

    private void OnGameStart()
    {
      AnimateCutterToGameState();
      AnimateNextMesh();
    }

    private void OnPointerUp()
    {
      if (canCut)
      {
        AnimateCut();
      }
    }

    private void AnimateNextMesh()
    {
      if (LevelsManager.HasNextMesh())
      {
        MeshInfo info = LevelsManager.GetNextMeshInfo();
        objectToSlice.GetComponent<MeshFilter>().mesh = info.mesh;
        Base b = objectToSlice.GetComponent<Base>();
        b.SetLocalPositionZ(Random.Range(objectToSliceMinLocalPosZ, objectToSliceMaxLocalPosZ));
        b.SetScale(0);
        b.SetRotationY(info.rotation.y - 180);
        b.Activate();
        b.Sequence(
          b.Scale(1, 0.4f).SetEase(Ease.OutBack)
        );
        b.Sequence(
          b.RotateY(info.rotation.y, 0.4f).SetEase(Ease.InSine),
          OnFinish(() => canCut = true)
        );
      }
      else
      {
        Events.RequestFinish.Call();
      }
    }

    private void AnimateCutterToStartState()
    {
      cutter.KillSequences();
      cutter.SetPosition(new Vector3(0, 1, 0));
      AnimateCutterIdle();
    }

    private void AnimateCutterToGameState()
    {
      cutter.KillSequences();
      cutter.Sequence(
        cutter.Move(new Vector3(0, 2f, 0), 0.5f).SetEase(Ease.InSine),
        OnFinish(() => AnimateCutterIdle())
      );
    }

    private void AnimateCutterIdle()
    {
      cutter.KillSequences();
      float upPosition = cutter.GetPositionY() + 0.14f;
      float position = cutter.GetPositionY();
      cutter.Sequence(
        cutter.MoveY(upPosition, 0.6f).SetEase(Ease.InOutQuad),
        cutter.MoveY(position, 0.6f).SetEase(Ease.InOutQuad)
      ).SetLoops(-1);
    }

    private void AnimateCut()
    {
      canCut = false;
      SlicedHull hull = objectToSlice.Slice(slicePoint.position, slicePoint.up, material);

      cutter.KillSequences();
      cutter.Sequence(
        cutter.MoveY(2.5f, 0.3f).SetEase(Ease.InSine),
        cutter.MoveY(0, 0.5f).SetEase(Ease.InSine),
        cutter.OnFinish(() =>
        {
          Cut(hull);
        }),
        cutter.MoveY(2, 0.6f).SetEase(Ease.InOutQuad),
        cutter.OnFinish(() =>
        {
          AnimateCutterIdle();
          if (hull == null)
          {
            canCut = true;
          }
        })
      );
    }

    private void Cut(SlicedHull sliceController)
    {
      if (sliceController == null) return;

      objectToSlice.SetActive(false);

      Base left = sliceController.CreateLowerHull(objectToSlice, material).AddComponent(typeof(Base)) as Base;
      Base right = sliceController.CreateUpperHull(objectToSlice, material).AddComponent(typeof(Base)) as Base;

      left.SetRotationY(objectToSlice.transform.eulerAngles.y);
      right.SetRotationY(objectToSlice.transform.eulerAngles.y);

      slicedObjectLeftPos.SetPositionY(left.GetPositionY());
      slicedObjectRightPos.SetPositionY(right.GetPositionY());

      AnimateSlicedObjectMovement(left, slicedObjectLeftPos.position);
      AnimateSlicedObjectMovement(right, slicedObjectRightPos.position, () => AnimateNextMesh());

      int leftPercentage, rightPercentage;
      CalculateSlicePercentage(left.GetComponent<MeshFilter>().mesh, right.GetComponent<MeshFilter>().mesh, out leftPercentage, out rightPercentage);
      IncreaseGameProgress(leftPercentage, rightPercentage);
      Events.SuccessfulSlice.Call(leftPercentage, rightPercentage);
    }

    private void AnimateSlicedObjectMovement(Base obj, Vector3 finishPos, System.Action onFinish = null)
    {
      obj.Sequence(
        obj.Move(finishPos, 0.5f).SetEase(Ease.InSine),
        obj.MoveY(-4, 0.4f).SetEase(Ease.InSine),
        obj.OnFinish(() =>
        {
          onFinish?.Invoke();
          Destroy(obj.gameObject);
        })
      );
    }

    private void CalculateSlicePercentage(Mesh mesh1, Mesh mesh2, out int firstPercentage, out int secondPercentage, int deviation = 2)
    {
      float leftArea = mesh1.GetArea();
      float rightArea = mesh2.GetArea();
      float areaSum = leftArea + rightArea;

      firstPercentage = (int)leftArea.Map(0, areaSum, 0, 100);
      secondPercentage = 100 - firstPercentage;

      if (firstPercentage < secondPercentage)
      {
        firstPercentage = Mathf.Clamp(firstPercentage + deviation, 0, 50);
        secondPercentage = Mathf.Clamp(secondPercentage - deviation, 50, 100);
      }
      else
      {
        secondPercentage = Mathf.Clamp(secondPercentage + deviation, 0, 50);
        firstPercentage = Mathf.Clamp(firstPercentage - deviation, 50, 100);
      }
    }

    private void IncreaseGameProgress(int leftPercentage, int rightPercentage)
    {
      float percentageDelta = Mathf.Abs(leftPercentage - rightPercentage);
      float koef = (100 - percentageDelta) / 100;
      HPManager.IncreaseProgress(koef);
    }

#if UNITY_EDITOR
    /**
     * This is for Visual debugging purposes in the editor 
     */
    public void OnDrawGizmos()
    {
      if(slicePoint == null) return;

      SliceFramework.Plane cuttingPlane = new SliceFramework.Plane();
      cuttingPlane.Compute(slicePoint);
      cuttingPlane.OnDebugDraw();
    }
#endif
  }
}
