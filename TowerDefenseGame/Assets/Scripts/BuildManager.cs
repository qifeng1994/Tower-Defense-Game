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

    //public GameObject standardTurretPrefab;
    //public GameObject missileLauncherPrefab;

    //private void Start()
    //{
    //    turretToBuild = standardTurretPrefab;
    //}

    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI; //当node上面有炮塔时，再次点击node会弹出nodeUI

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

        DeselectNode();
    }

    public void SelectNode(Node node) //
    {
        if(selectedNode == node) //再次点击，隐藏UI
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public bool CanBuild { get { return turretToBuild != null; } } //属性为只读
    public bool HasMoney { get { return PlayerStatus.Money >= turretToBuild.cost; } }

    //
//    public void BuildTurretOn(Node node)
//    {
//        //
//        if(PlayerStatus.Money < turretToBuild.cost)
//        {
//            Debug.Log("not enough money to build");
//            return;
//        }

//        PlayerStatus.Money -= turretToBuild.cost; //

//       GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity); //transform.rotation的区别是什么？
//        node.turret = turret;

//        Debug.Log("turret build, money left:" + PlayerStatus.Money); //
//    }
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
