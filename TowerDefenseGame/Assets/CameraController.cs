using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //这个脚本用于在游戏进行时控制镜头的移动和缩放
    //通过键盘或者鼠标靠近画面边界来让镜头移动
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float maxY = 80f;
    public float minY = 10f;

    private bool doMovement = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Esc作为开关控制镜头是否可以移动
            doMovement = !doMovement;

        if (!doMovement)
            return;

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); //
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World); //
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World); //
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World); //
        }

        //鼠标滑轮控制相机自己的坐标移动
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * scrollSpeed * Time.deltaTime*1000;
        pos.y = Mathf.Clamp(pos.y, minY, maxY); //设置边界，防止视角过大或过小
        transform.position = pos;

    }
}
