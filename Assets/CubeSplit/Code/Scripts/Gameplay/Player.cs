using UnityEngine;

public class Player : SceneSingleton<Player>
{
    public void Hit(Blade blade)
    {
        float hangOver = transform.position.x - blade.transform.position.x;

        float dir = hangOver < 0 ? 1 : -1;


        SplitCubeOnX(blade, hangOver, dir);
        //transform.localScale = new Vector3(1-hangOver,);
        //ObjectPooler.Instance.SpawnFromPool(PooledObjectType.DroppedCube, targetPos, Quaternion.identity);






    }


    private void SplitCubeOnX(Blade blade, float hangOver, float dir)
    {
        float newXSize = (transform.localScale.x - blade.transform.localScale.x) - Mathf.Abs((hangOver));
        float fallingBlockSize = transform.localScale.x - newXSize;


        float newXPosition = blade.transform.position.x + (hangOver / 2);


        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);


        float cubeEdge = transform.position.x + (newXSize / 2) * dir;

        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2 * dir;
        Debug.Log(fallingBlockSize);
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
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        //cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;


        //Destroy(cube.gameObject, 1f);

    }


}
