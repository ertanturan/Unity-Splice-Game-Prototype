using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : SceneSingleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private TextMeshProUGUI[] _pointText;


    [SerializeField] private WindowUI _settingsWindow;
    [SerializeField] private WindowUI _failWindow;
    [SerializeField] private WindowUI _inGameWindow;

    private WaitForSeconds _waitFor = new WaitForSeconds(2);

    private void SetDistance(float dist)
    {
        _distanceText.text = dist.ToString("n2");
    }

    private void SetPoints(int point)
    {
        for (int i = 0; i < _pointText.Length; i++)
        {
            _pointText[i].text = point.ToString();

        }
    }

    private void Update()
    {
        SetDistance(GameManager.Instance.CalculateDistance());
        SetPoints(GameManager.Instance.CalculatePoint());
    }

    public void ShowFailWindow()
    {
        _inGameWindow.Hide();
        StartCoroutine(DelayedWindow(_failWindow));
    }

    private IEnumerator DelayedWindow(WindowUI window)
    {
        yield return _waitFor;
        window.Show();
    }

}
