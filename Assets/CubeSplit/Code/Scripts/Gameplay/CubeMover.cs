using UnityEngine;

public class CubeMover : MonoBehaviour
{

    public Axes Axis;
    [SerializeField] private float _moveSpeed = 2f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float coef = Time.deltaTime * _moveSpeed;
        Vector3 currentPos = transform.position;
        switch (Axis)
        {
            case Axes.X:
                currentPos.x += coef;
                transform.position = currentPos;
                break;

            case Axes.Y:
                currentPos.x += coef;
                transform.position = currentPos;
                break;

            case Axes.Z:
                currentPos.x += coef;
                transform.position = currentPos;
                break;
        }
    }
}
