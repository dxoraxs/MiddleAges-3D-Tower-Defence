using UnityEngine;
using UnityEngine.UI;

public class CanvasTowerButton : MonoBehaviour
{
    [SerializeField] private Text nameTower, levelTower;

    public void QuitButton()
    {
        gameObject.SetActive(false);
    }

    public void UpgradeButton()
    {

    }

    public void SellingButton()
    {

    }
}
