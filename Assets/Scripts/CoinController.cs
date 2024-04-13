using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [Header("�����ת�ٶ�")]
    public float rotationSpeed = 45f;   // �����ת�ٶ�
    [Header("������¸����ٶ�")]
    public float floatSpeed = 0.5f;     // ������¸����ٶ�
    [Header("������¸�������")]
    public float floatAmount = 0.1f;    // ������¸�������

    private float startY;   // ��ʼY��λ��

    void Start()
    {
        startY = transform.position.y;  // ��¼��ʼY��λ��
    }

    void Update()
    {
        // �����µ���ת�Ƕ�
        Quaternion rotate = Quaternion.Euler(0f, Time.time * rotationSpeed, 0f);
        transform.rotation = rotate;

        // �����µ�Y��λ��
        float newY = startY + (Mathf.Sin(Time.time * floatSpeed) * floatAmount);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
