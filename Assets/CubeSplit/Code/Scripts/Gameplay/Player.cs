using UnityEngine;

public class Player : SceneSingleton<Player>
{



    private void Awake()
    {
        //GetComponent<Renderer>().material.SetColor("_Color", GameManager.Instance.GetRandomColor());
    }

    public void Hit(Blade blade)
    {
        Debug.Log(CheckAngle(blade));
        //if (CheckAngle(blade))
        //{
        //    //OLD

        //float hangOver = transform.position.x - blade.transform.position.x;
        //hangOver = transform.localScale.x - hangOver;
        ////Debug.Log(hangOver);
        //SplitCubeOnX(blade, hangOver);


        SliceManager.Instance.Slice(gameObject, GetComponent<Renderer>().material, blade.transform);


        GameManager.Instance.Fail();
        //}


    }

    private void SplitCubeOnX(Blade blade, float hangOver)
    {

        float newXSize = transform.localScale.x - Mathf.Abs(hangOver);
        float fallingCubeSize = transform.localScale.x - newXSize;

        float cubeEdge = transform.position.x + (newXSize / 2) * -1;

        float newXPosition = blade.transform.position.x + newXSize;

        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float fallingCubePos = cubeEdge - fallingCubeSize / 2 * -1;

        SpawnDroppingCube(fallingCubePos, fallingCubeSize, hangOver);

    }

    private void SpawnDroppingCube(float fallingCubePos, float fallingCubeSize, float hangOver)
    {



        Vector3 cubeScale = new Vector3(fallingCubeSize, transform.localScale.y, transform.localScale.z);
        Vector3 cubePos = new Vector3(fallingCubePos - fallingCubeSize, transform.position.y, transform.position.z);


        GameObject cube = ObjectPooler.Instance.SpawnFromPool(PooledObjectType.DroppedCube, cubePos, Quaternion.identity);
        cube.transform.localScale = cubeScale;

        cube.GetComponent<Renderer>().material.color = transform.GetChild(0).GetComponent<Renderer>().material.color;


        transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", GameManager.Instance.IntensifyColor(transform.GetChild(0).GetComponent<Renderer>().material.color));

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        rb.AddForce(cube.transform.right * -1 * 100);
        rb.AddForce(cube.transform.up * 50);
    }

    private bool CheckAngle(Blade blade)
    {


        Vector3 dir = blade.transform.position - transform.position;

        float angle = Vector3.Angle(dir, transform.right);

        Debug.Log(angle);

        if (angle < 90)
        {
            return false;
        }
        else
        {
            return true;
        }


    }

}
