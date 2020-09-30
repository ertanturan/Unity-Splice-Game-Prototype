using System.Collections;
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
        GenerateScene();

    }


    private void GenerateScene()
    {


        for (int i = 0; i < 25; i++)
        {

            SpawnGoillotine(i, startPos);
            SpawnSideEnvironment();

            startPos.x += _distanceBetween;
            _targetDistance += _distanceBetween;


        }
    }

    private void Update()
    {

        float distance = Mathf.Abs(Player.Instance.transform.position.x - _targetDistance);

        if (distance < _distanceBetween * 10)
        {
            GenerateScene();
        }


    }


    private void SpawnSideEnvironment()
    {
        GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
        primitive.transform.position = new Vector3(startPos.x, 1, 6);
    }

    private void SpawnGoillotine(int index, Vector3 start)
    {
        StartCoroutine(DelayedSpawn(index, start));
    }

    private IEnumerator DelayedSpawn(int time, Vector3 start)
    {
        float randomAdd = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds((time / 2) + randomAdd);
        ObjectPooler.Instance.SpawnFromPool(PooledObjectType.Guillotine, start, Quaternion.identity);

    }


}
