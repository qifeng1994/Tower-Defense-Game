using System.Collections;
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

    private GameObject turretToBuild;

    //一个公开的方法，可以在其他脚本中调用，用于获取私有变量
    public GameObject GetTurretToBuild()
    {
        return turretToBuild; 
    }

    //一个公开的方法用于给私有变量赋值
    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
