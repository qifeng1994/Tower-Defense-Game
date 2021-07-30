using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset; //在node部署的turret默认位置是node的中心，为了能移动到表面，需要定义一个偏移

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color; //脚本加载时先获取node的renderer组件，然后把颜色作为初始颜色
        buildManager = BuildManager.instance;
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

        rend.material.color = hoverColor;
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
        //当玩家没有选择turret图标时，buildmanger脚本无法获得变量，因此即使鼠标点击部署，也不会有反应
        if (!buildManager.CanBuild)
            return;

        if(turret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        //获得在buildmanager脚本中的turret对象,并将其实例化
        //GameObject turretToBuild = buildManager.GetTurretToBuild();
        //turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);

        buildManager.BuildTurretOn(this); //
    }

    //turret放置的位置是node的中心加上偏移
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
