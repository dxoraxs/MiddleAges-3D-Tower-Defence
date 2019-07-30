using UnityEngine;
using UnityEngine.UI;

public class TowerUISetText : MonoBehaviour
{
    [SerializeField] private Text _textName, _textLevel;
    private string[] array;
    private void OnEnable()
    {
        array=transform.GetComponentInParent<UIChangePosition>().NameAndLevelTowerReturn();
        _textName.text = array[0];
        _textLevel.text = array[1];
    }
}
