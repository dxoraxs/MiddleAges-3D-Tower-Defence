using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarControlled : MonoBehaviour
{
    [SerializeField]private RectTransform _barPrefab;
    public Vector2 SetSize;
    private RectTransform _barFilled, _barRed;
    public Color ColorSetColor;
    private float _fullSizeHealthBar;
    
    void Awake()
    {
        _barFilled = _barPrefab.Find("health_white").GetComponent<RectTransform>();
        _barRed = _barFilled.Find("health_red").GetComponent<RectTransform>();

        _barRed.sizeDelta = SetSize;
        _barFilled.sizeDelta = SetSize;
        _barRed.GetComponent<Image>().color = ColorSetColor;

        _fullSizeHealthBar = _barFilled.sizeDelta.x;
    }

    void Update()
    {
        _barPrefab.LookAt(Camera.main.transform);
    }

    public void SetHealth(float percent)
    {
        Debug.Log("HealthBar Set Health\n% = "+percent);
        if (percent > 0) _barRed.sizeDelta = new Vector2(_fullSizeHealthBar * percent, _barRed.sizeDelta.y);
    }
}
