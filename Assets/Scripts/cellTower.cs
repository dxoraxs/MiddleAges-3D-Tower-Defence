using UnityEngine;
using UnityEngine.EventSystems;

public class cellTower : MonoBehaviour
{
    private Material _sprite;
    [SerializeField] private Color _changeColor;
    private Color _defaultMaterial;

    private GameObject _ObjectTower;
    
    private float _time;
    void Start()
    {
        _sprite = GetComponent<Renderer>().material;
        _defaultMaterial = _sprite.color;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        _sprite.color = _changeColor;
    }
    private void OnMouseExit()
    {
        _sprite.color = _defaultMaterial;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        transform.parent.parent.GetComponent<UIChangePosition>().TransfromChangePosition(transform, _ObjectTower != null);
    }

    public void TowerBuilding(GameObject tower)
    {
        _ObjectTower = Instantiate(tower, transform.parent);
        _ObjectTower.transform.position = transform.position;
    }

    public void SellTowerDestroy()
    {
        Destroy(_ObjectTower);
        _ObjectTower = null;
    }

    public void UpgradeTower()
    {
        _ObjectTower.transform.Find("Tower").gameObject.SendMessage("ChangeTowerLevel");
    }

    public string[] NameLevelTower()
    {
        if (_ObjectTower.transform.Find("Tower").GetComponent<CrossbowTower>() !=null)
        {
            return _ObjectTower.transform.Find("Tower").GetComponent<CrossbowTower>().ReturnNameLevel();
        }
        else
            return _ObjectTower.transform.Find("Tower").GetComponent<ArtilleryTower>().ReturnNameLevel();
        //return _ObjectTower.transform.Find("Tower").gameObject.SendMessage("ChangeTowerLevel");
    }
}
