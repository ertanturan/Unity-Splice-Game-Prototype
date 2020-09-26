using UnityEngine;

public class SceneGenerator : MonoBehaviour
{
    [SerializeField] private int _distanceBetween = 15;


    private void Start()
    {
        SpawnGoullotines();
    }

    private void SpawnGoullotines()
    {
        Vector3 startPos = Vector3.zero;

        startPos.x += _distanceBetween;

        for (int i = 0; i < 20; i++)
        {
            GameObject obj = ObjectPooler.Instance.SpawnFromPool(PooledObjectType.Guillotine, startPos, Quaternion.identity);
            startPos.x += _distanceBetween;
        }
    }


}
