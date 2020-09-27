using UnityEngine;

public class DroppedCube : MonoBehaviour, IPooledObject
{
    public PooledObjectType PoolType { get; set; }

    private bool _seenOnce = false;
    private bool _shoulCount = false;

    public void Init()
    {

    }

    public void OnObjectSpawn()
    {
    }

    public void OnObjectDespawn()
    {
        transform.localScale = Vector3.one;
        _seenOnce = false;
        _shoulCount = false;
    }

    public void Despawn()
    {
        ObjectPooler.Instance.Despawn(gameObject);
    }


    private void Update()
    {
        if (!_seenOnce)
        {

            _seenOnce = GameManager.Instance.IsTargetVisible(UserCamera.Instance.MainCamera, gameObject);
            if (_seenOnce)
            {
                _shoulCount = true;
            }
        }


        if (_shoulCount)
        {
            if (!GameManager.Instance.IsTargetVisible(UserCamera.Instance.MainCamera, gameObject))
            {
                Despawn();
            }
        }
    }



}
