using UnityEngine;

namespace MeshSlice
{
  [ExecuteInEditMode]
  public class SlicableObjectMaterial : MonoBehaviour
  {
    public Material material;

    [Header("Points")]
    public Transform firstPoint;
    public Transform secondPoint;

    private readonly int firstPointId = Shader.PropertyToID("_Point1");
    private readonly int secondPointId = Shader.PropertyToID("_Point2");

    private void Update()
    {
      if (firstPoint && secondPoint)
      {
        material.SetVector(firstPointId, firstPoint.position);
        material.SetVector(secondPointId, secondPoint.position);
      }
    }
  }
}
