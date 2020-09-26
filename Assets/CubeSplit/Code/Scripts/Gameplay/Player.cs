using UnityEngine;

public class Player : SceneSingleton<Player>
{

    private float _halfScale;

    private void Awake()
    {
        _halfScale = transform.localScale.x / 2;
    }

    public void Hit(Blade blade)
    {


        float hangOver = blade.transform.position.x - transform.position.x;

        if (hangOver > 0)
        {
            hangOver = Mathf.Clamp(hangOver, 0, _halfScale);
            hangOver = _halfScale - hangOver;
        }
        else
        {
            hangOver = Mathf.Clamp(hangOver, -_halfScale, 0);
            hangOver = _halfScale - hangOver;
        }

        hangOver -= 1;


        float dir = hangOver < 0 ? -1 : 1;
        //float dir = 1f;
        SplitCubeOnX(blade, hangOver, dir);




    }


    private void SplitCubeOnX(Blade blade, float hangOver, float dir)
    {

        float newXSize = (transform.localScale.x - blade.transform.localScale.x) - Mathf.Abs(hangOver);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = blade.transform.position.x + (hangOver / 2);

        if (newXSize < 0)
        {
            Debug.LogWarning("failed");
        }
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        float posDiff = transform.position.x - newXPosition;

        Debug.Log(transform.position.x);
        transform.position = new Vector3(transform.position.x + 2 * Mathf.Abs(posDiff), transform.position.y, transform.position.z);

        Debug.Log(transform.position.x);

        float cubeEdge = transform.position.x + (newXSize / 2) * dir;

        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2 * dir;

        Debug.Log(hangOver);
        Debug.Log(dir);

        SpawnDroppingCube(fallingBlockXPosition, fallingBlockSize);
    }



    private void SpawnDroppingCube(float xPos, float size)
    {

        Vector3 cubeScale = new Vector3(size, transform.localScale.y, transform.localScale.z);
        Vector3 cubePos = new Vector3(xPos, transform.position.y, transform.position.z);



        GameObject cube =
            ObjectPooler.Instance.SpawnFromPool(PooledObjectType.DroppedCube, cubePos, Quaternion.identity);
        cube.transform.localScale = cubeScale;
        Debug.Log(cubeScale);
        Rigidbody rb = cube.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        rb.AddForce(cube.transform.right * -1 * 100);

        //cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;


        //Destroy(cube.gameObject, 1f);

    }


}
