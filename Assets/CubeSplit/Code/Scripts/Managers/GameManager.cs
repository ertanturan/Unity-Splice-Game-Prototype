using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SceneSingleton<GameManager>
{
    private int _currentPoint = 0;
    private float _currentDistance;

    private bool _isPlaying = false;

    public bool IsPlaying
    {
        get { return _isPlaying; }
        private set { _isPlaying = value; }
    }

    private void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public bool IsTargetVisible(Camera c, GameObject go)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    public Color GetRandomColor()
    {
        float r = Mathf.InverseLerp(0, 255, Random.Range(0, 255));
        float g = Mathf.InverseLerp(0, 255, Random.Range(0, 255));
        float b = Mathf.InverseLerp(0, 255, Random.Range(0, 255));
        return new Color(r, g, b);
    }

    public Color IntensifyColor(Color color)
    {


        float r = (int)(color.r * 255);
        r += 3;
        float g = (int)(color.g * 255);
        g += 3;
        float b = (int)(color.b * 255);
        b += 3;

        r = Mathf.InverseLerp(0, 255, r);
        g = Mathf.InverseLerp(0, 255, g);
        b = Mathf.InverseLerp(0, 255, b);

        return new Color(r, g, b);
    }

    public int CalculatePoint()
    {

        if (_currentDistance != CalculateDistance())
        {
            int calculatedPoint = (int)(Player.Instance.transform.localScale.x * (CalculateDistance()));
            _currentPoint += calculatedPoint * 10;
            _currentPoint /= 2;
            _currentDistance = CalculateDistance();
        }
        return _currentPoint;
    }

    public float CalculateDistance()
    {
        return Player.Instance.transform.position.x - 1;
    }

    public void StartGame()
    {
        IsPlaying = true;

    }

    public void Fail()
    {
        StopGame();
        UIManager.Instance.ShowFailWindow();
    }

    private void StopGame()
    {
        IsPlaying = false;

    }

    public void RestartGmae()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
