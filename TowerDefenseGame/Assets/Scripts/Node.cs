using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset; //在node部署的turret默认位置是node的中心，为了能移动到表面，需要定义一个偏移

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color; //脚本加载时先获取node的renderer组件，然后把颜色作为初始颜色
        buildManager = BuildManager.instance;
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStatus.Money < blueprint.cost)
        {
            Debug.Log("not enough money to build");
            return;
        }

        PlayerStatus.Money -= blueprint.cost; //

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity); //transform.rotation的区别是什么？
        turret = _turret;

        turretBlueprint = blueprint;

        // Debug.Log("turret build, money left:" + PlayerStatus.Money); 
    }

    //当鼠标放在node上面时，变色
    private void OnMouseEnter()
    {
        //当turret图标覆盖在node上面，鼠标无法点击图标下一层的node
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //当玩家没有选择turret图标时，不变色
        if (!buildManager.CanBuild)
            return;

        if(buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    //当鼠标离开node时，变为初始颜色
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    //鼠标点击时，实例化turret
    private void OnMouseDown()
    {
        //当turret图标覆盖在node上面，鼠标无法点击图标下一层的node
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if(turret != null) //当node上已经有炮塔时，把当前node的值返回
        {
            buildManager.SelectNode(this);
            return;
        }

        //获得在buildmanager脚本中的turret对象,并将其实例化
        //GameObject turretToBuild = buildManager.GetTurretToBuild();
        //turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);

        //当玩家没有选择turret图标时，buildmanger脚本无法获得变量，因此即使鼠标点击部署，也不会有反应
        if (!buildManager.CanBuild)
            return;

        //如果以上情况都不满足，则说明这个node在点击后可以实例化一个turret
        //buildManager.BuildTurretOn(this); 
        BuildTurret(buildManager.GetTurretToBuild());
    }

    //turret放置的位置是node的中心加上偏移
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void UpgradeTurret()
    {
        if (PlayerStatus.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("not enough money to upgrade");
            return;
        }

        PlayerStatus.Money -= turretBlueprint.upgradeCost;

        Destroy(turret); //get rid of the old turret and build a new one

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity); //transform.rotation的区别是什么？
        turret = _turret;

        isUpgraded = true;
    }

    public void SellTurret() //
    {
        PlayerStatus.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;
    }
}
