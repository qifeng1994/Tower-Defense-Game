using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset; //在node部署的turret默认位置是node的中心，为了能移动到表面，需要定义一个偏移

    private GameObject turret;
    private Renderer rend;
    private Color startColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color; //脚本加载时先获取node的renderer组件，然后把颜色作为初始颜色
    }

    //当鼠标放在node上面时，变色
    private void OnMouseEnter()
    {
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
        if(turret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        //获得在buildmanager脚本中的turret对象
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
}
