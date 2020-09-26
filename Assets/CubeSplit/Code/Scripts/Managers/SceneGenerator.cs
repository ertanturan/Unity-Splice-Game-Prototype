using UnityEngine;

public class SceneGenerator : MonoBehaviour
{
    [SerializeField] private int _distanceBetween = 15;

    private bool _shouldSpawn = false;

    private float _targetDistance = 0f;

    Vector3 startPos = Vector3.zero;
    private void Start()
    {
        startPos.x += _distanceBetween;
        SpawnGoullotines();

    }


    private void SpawnGoullotines()
    {


        for (int i = 0; i < 5; i++)
        {
            GameObject obj = ObjectPooler.Instance.SpawnFromPool(PooledObjectType.Guillotine, startPos, Quaternion.identity);
            startPos.x += _distanceBetween;
            _targetDistance += _distanceBetween;
        }
    }

    private void Update()
    {

        float distance = Mathf.Abs(Player.Instance.transform.position.x - _targetDistance);

        if (distance < _distanceBetween * 2)
        {
            SpawnGoullotines();
        }


    }



}
