using UnityEngine;

namespace MeshSlice
{
  [System.Serializable]
  public class MeshInfo
  {
    public Mesh mesh;
    public Vector3 rotation;

    public MeshInfo(Mesh mesh, Vector3 rotation)
    {
      this.mesh = mesh;
      this.rotation = rotation;
    }
  }
}