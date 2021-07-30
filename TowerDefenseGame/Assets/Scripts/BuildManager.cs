﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //single partern 单例模式 ???
    public static BuildManager instance;

    

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;

    //private void Start()
    //{
    //    turretToBuild = standardTurretPrefab;
    //}

    private TurretBlueprint turretToBuild;

    //一个公开的方法，可以在其他脚本中调用，用于获取私有变量
    //public GameObject GetTurretToBuild()
    //{
    //    return turretToBuild; 
    //}

    //一个公开的方法用于给私有变量赋值
    //public void SetTurretToBuild(GameObject turret)
    //{
    //    turretToBuild = turret;
    //}

    //把序列化的变量赋值给私有变量
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public bool CanBuild { get { return turretToBuild != null; } } //属性为只读

    //
    public void BuildTurretOn(Node node)
    {
        //
        if(PlayerStatus.Money < turretToBuild.cost)
        {
            Debug.Log("not enough money to build");
            return;
        }

        PlayerStatus.Money -= turretToBuild.cost; //

       GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity); //transform.rotation的区别是什么？
        node.turret = turret;

        Debug.Log("turret build, money left:" + PlayerStatus.Money); //
    }
}
