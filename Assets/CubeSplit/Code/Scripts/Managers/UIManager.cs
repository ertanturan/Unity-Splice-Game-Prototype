using TMPro;
using UnityEngine;

public class UIManager : SceneSingleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private TextMeshProUGUI _pointText;

    private void SetDistance(float dist)
    {
        _distanceText.text = dist.ToString("n2");
    }

    private void SetPoints(int point)
    {
        _pointText.text = point.ToString();
    }

    private void Update()
    {
        SetDistance(GameManager.Instance.CalculateDistance());
        SetPoints(GameManager.Instance.CalculatePoint());
    }
}
