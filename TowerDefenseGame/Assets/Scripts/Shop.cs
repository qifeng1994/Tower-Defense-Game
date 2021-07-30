using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;

    BuildManager buildManager; //来自BuildManager类的变量

    private void Start()
    {
        buildManager = BuildManager.instance; //变量实例化
    }
    public void SelectStandardTurret() //当按图标时触发事件，调用该函数
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher() //当按图标时触发事件，调用该函数
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }
}
