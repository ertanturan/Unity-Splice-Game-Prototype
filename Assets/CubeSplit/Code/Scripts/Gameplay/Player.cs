using UnityEngine;

public class Player : SceneSingleton<Player>
{

    private float _initialScale;

    private void Awake()
    {
        _initialScale = transform.localScale.x;
    }

    public void Hit(Blade blade)
    {


        //float hangOver = blade.transform.position.x - transform.position.x;

        //if (hangOver > 0)
        //{
        //    hangOver = Mathf.Clamp(hangOver, 0, _halfScale);
        //    hangOver = _halfScale - hangOver;
        //}
        //else
        //{
        //    hangOver = Mathf.Clamp(hangOver, -_halfScale, 0);
        //    hangOver = _halfScale - hangOver;
        //}

        //hangOver -= 1;


        //float dir = hangOver < 0 ? -1 : 1;
        ////float dir = 1f;
        //SplitCubeOnX(blade, hangOver, dir);


        float hangOver = transform.position.x - blade.transform.position.x;
        hangOver = _initialScale - hangOver;
        Debug.Log(hangOver);
        SplitCubeOnX(blade, hangOver);



    }


    private void SplitCubeOnX(Blade blade, float hangOver)
    {





        float newXSize = transform.localScale.x - Mathf.Abs(hangOver);
        float fallingCubeSize = transform.localScale.x - newXSize;

        float cubeEdge = transform.position.x + (newXSize / 2) * -1;

        float newXPosition = blade.transform.position.x + 2 * hangOver;

        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float fallingCubePos = cubeEdge - fallingCubeSize / 2 * -1;

        Debug.Log(fallingCubePos);
        SpawnDroppingCube(fallingCubePos, fallingCubeSize, hangOver);



    }




    private void SpawnDroppingCube(float fallingCubePos, float fallingCubeSize, float hangOver)
    {



        Vector3 cubeScale = new Vector3(fallingCubeSize, transform.localScale.y, transform.localScale.z);
        Vector3 cubePos = new Vector3(fallingCubePos - 2 * hangOver, transform.position.y, transform.position.z);


        GameObject cube = ObjectPooler.Instance.SpawnFromPool(PooledObjectType.DroppedCube, cubePos, Quaternion.identity);
        cube.transform.localScale = cubeScale;

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        rb.AddForce(cube.transform.right * -1 * 100);



    }


}
