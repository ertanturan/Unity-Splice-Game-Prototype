using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    private bool _isOn
    {
        get { return IsOn; }
        set { IsOn = value; OnValueChanged.Invoke(); }
    }

    public bool IsOn;


    [Header("Sprite")]
    public Sprite OnSprite;
    public Sprite offSprite;

    public Image toggleBgImage;

    [Header("Rect")]
    public RectTransform toggle;
    public GameObject handle;
    private RectTransform handleTransform;


    private float handleSize;
    private float onPosX;
    private float offPosX;
    [Header("Options")]
    public float handleOffset;

    public float speed;
    static float t = 0.0f;

    private bool switching = false;

    [Space]
    public UnityEvent OnValueChanged;

    void Awake()
    {
        handleTransform = handle.GetComponent<RectTransform>();
        RectTransform handleRect = handle.GetComponent<RectTransform>();
        handleSize = handleRect.sizeDelta.x;
        float toggleSizeX = toggle.sizeDelta.x;
        onPosX = (toggleSizeX / 2) - (handleSize / 2) - handleOffset;
        offPosX = onPosX * -1;

        
        
    }

    void Start()
    {
        if (_isOn)
        {
            toggleBgImage.sprite = OnSprite;
            handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
        }
        else
        {
            toggleBgImage.sprite = offSprite;
            handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
        }
    }

    void Update()
    {

        if (switching)
        {
            Toggle(_isOn);
        }
    }

    public void Switching()
    {
        switching = true;
    }

    public void Toggle(bool toggleStatus)
    {
        if (toggleStatus)
        {
            toggleBgImage.sprite = offSprite;
            handleTransform.localPosition = SmoothMove(handle, onPosX, offPosX);
        }
        else
        {
            toggleBgImage.sprite = OnSprite;
            handleTransform.localPosition = SmoothMove(handle, offPosX, onPosX);
        }
    }

    Vector3 SmoothMove(GameObject toggleHandle, float startPosX, float endPosX)
    {

        Vector3 position = new Vector3(Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);
        StopSwitching();
        return position;
    }

    void StopSwitching()
    {
        if (t > 1.0f)
        {
            switching = false;

            t = 0.0f;
            switch (_isOn)
            {
                case true:
                    _isOn = false;
                    break;

                case false:
                    _isOn = true;
                    break;
            }

        }
    }

}
