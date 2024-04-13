using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("控制摄像机旋转的灵敏度")]
    public float mouseMoveSpeed = 2f;

    [Header("摄像机要跟随的物体")]
    public Transform playerTransfrom;

    [Header("被跟随物体与摄像机之间的距离")]
    public float direction = 5f;

    public PlayerController playerController;

    private float moveX;

    private float moveY;

    private void Update()
    {
        if(!playerController.gameOver)
        {
            moveX += Input.GetAxis("Mouse X") * mouseMoveSpeed;

            moveY -= Input.GetAxis("Mouse Y") * mouseMoveSpeed;

            moveY = Mathf.Clamp(moveY, 10f, 90f);//限制旋转角度的范围，使其不超过一定的最大值和最小值

            transform.eulerAngles = new Vector3(moveY, moveX, 0);

            transform.position = playerTransfrom.position - transform.forward * direction;
        }
    }
}
