using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public Button upgradeButton;
    public Button sellButton;

    [Header("NodeUI text")]
    public Text upgradeText;
    public Text sellText;

    public void SetTarget (Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();

        //Update UI text
        if (!target.isUpgraded)
        {
            upgradeText.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeText.text = "Max upgrade";
            upgradeButton.interactable = false;
        }

        sellText.text = "$" + target.turretBlueprint.sellPrice();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
