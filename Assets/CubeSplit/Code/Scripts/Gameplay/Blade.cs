using UnityEngine;

public class Blade : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {

        GameObject hitObj = col.gameObject;

        if (hitObj.GetInstanceID() == Player.Instance.gameObject.GetInstanceID())
        {
            Player.Instance.Hit(this);
        }
    }

}
