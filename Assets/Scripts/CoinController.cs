using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [Header("金币旋转速度")]
    public float rotationSpeed = 45f;   // 金币旋转速度
    [Header("金币上下浮动速度")]
    public float floatSpeed = 0.5f;     // 金币上下浮动速度
    [Header("金币上下浮动幅度")]
    public float floatAmount = 0.1f;    // 金币上下浮动幅度

    private float startY;   // 初始Y轴位置

    void Start()
    {
        startY = transform.position.y;  // 记录初始Y轴位置
    }

    void Update()
    {
        // 计算新的旋转角度
        Quaternion rotate = Quaternion.Euler(0f, Time.time * rotationSpeed, 0f);
        transform.rotation = rotate;

        // 计算新的Y轴位置
        float newY = startY + (Mathf.Sin(Time.time * floatSpeed) * floatAmount);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
