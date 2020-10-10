using UnityEngine;

namespace MeshSlice
{
  public static class Extensions
  {
    public static void SetPositionY(this Transform transform, float y)
    {
      Vector3 pos = transform.position;
      pos.y = y;
      transform.position = pos;
    }

    public static float Map(this float s, float a1, float a2, float b1, float b2)
    {
      return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public static float GetArea(this Mesh mesh)
    {
      Vector3[] vertices = mesh.vertices;
      int[] triangles = mesh.triangles;

      float result = 0f;
      for (int p = 0; p < triangles.Length; p += 3)
      {
        result += (Vector3.Cross(
          vertices[triangles[p + 1]] - vertices[triangles[p]],
          vertices[triangles[p + 2]] - vertices[triangles[p]])
        ).magnitude;
      }

      return result;
    }
  }
}
