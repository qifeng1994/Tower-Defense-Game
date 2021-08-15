using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;

    public Button upgradeButton;

    private Node target;

    public void SetTarget(Node _target) //
    {
        target = _target;
        transform.position = target.GetBuildPosition();

        if(!target.isUpgraded) //炮塔升级完之后，升级按钮不再有效
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost; //
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }
       
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode(); //只取消当前node的UI显示？
    }
}
