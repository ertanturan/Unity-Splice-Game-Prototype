using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonComponent : MonoBehaviour
{
    private Button _button { get; set; }


    public virtual void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { OnButtonClick(); });
    }

    public abstract void OnButtonClick();
}
