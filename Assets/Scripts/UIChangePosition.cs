using UnityEngine;

public class UIChangePosition : MonoBehaviour
{
    [SerializeField] private GameObject _uiTower, _uiCell;
    //[HideInInspector]public GameObject SelectedTower;
    private Transform _cellTower;

    public void TransfromChangePosition(Transform cellPosition, bool engaged)
    {
        _cellTower = cellPosition;
        if (engaged)
        {
            _uiCell.SetActive(false);
            _uiTower.SetActive(false);
            _uiTower.transform.position = cellPosition.position;
            _uiTower.SetActive(true);
        }
        else
        {
            _uiTower.SetActive(false);
            _uiCell.transform.position = cellPosition.position;
            _uiCell.SetActive(true);
        }
    }

    public void OnClickButtonBuildTower(GameObject tower)
    {
        _cellTower.GetComponent<cellTower>().TowerBuilding(tower);
        _uiCell.SetActive(false);
    }

    public void OnClickButtonSellingTower()
    {
        _cellTower.GetComponent<cellTower>().SellTowerDestroy();
        _uiTower.SetActive(false);
    }

    public void OnClickButtonUpgradeTower()
    {
        _cellTower.GetComponent<cellTower>().UpgradeTower();
        _uiTower.SetActive(false);
    }

    public string[] NameAndLevelTowerReturn()
    {
        return _cellTower.GetComponent<cellTower>().NameLevelTower();
    }
}