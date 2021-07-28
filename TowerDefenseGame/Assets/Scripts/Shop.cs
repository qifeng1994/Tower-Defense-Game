using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager; //来自BuildManager类的变量

    private void Start()
    {
        buildManager = BuildManager.instance; //变量实例化
    }
    public void PurchaseStandardTurret() //当按图标时触发事件，为接下来要部署的turret赋值
    {
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
    }
}
