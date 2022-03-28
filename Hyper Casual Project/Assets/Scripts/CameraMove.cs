using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 1f;

    public float distance = 10f;
    public float height = 5f;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //1.target ť���� transform find
        var playerObject = GameObject.FindGameObjectWithTag("Cube");
        target = playerObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //2.target follow
        float wantedRotationAngle = target.eulerAngles.y; //���� Ÿ���� y�� ���� ��.
        float wantedHeight = target.position.y + height; //���� Ÿ���� ���� + �츮�� �߰��� ���̰� ���� ����.

        float currentRotationAngle = transform.eulerAngles.y; //���� ī�޶��� y�� ���� ��.
        float currentHeight = transform.position.y;//���� ī�޶��� ���̰�.

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 3f * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, 2f * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        //3.
        transform.LookAt(target);
    }
}

