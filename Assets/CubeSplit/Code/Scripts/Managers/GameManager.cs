using UnityEngine.SceneManagement;

public class GameManager : SceneSingleton<GameManager>
{

    //public bool IsFailed(float hangover, Blade blade)
    //{
    //    return true;
    //    //if (Mathf.Abs(hangover) >= blade.transform.localScale.x)
    //    //{
    //    //    //Reload();
    //    //    return true;
    //    //}

    //    //return false;
    //}

    private void Reload()
    {
        SceneManager.LoadScene(0);
    }

}
