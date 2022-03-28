using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    private TextMesh textMesh;
    private Transform lookTarget;

    public float duration = 3f;
    public float speed = 5f;

    private float destroyTime;

    private void Awake()
    {
        //¿Ø¸¸ÇÏ¸é °¹Àº ¿©±â¿¡.
        textMesh = GetComponent<TextMesh>();
        lookTarget = Camera.main.transform;
    }

    public void Init(int damage, Color color)
    {
        textMesh.text = damage.ToString();
        textMesh.color = color;
        destroyTime = Time.time + duration;
        //Destroy(gameObject, duration);
    }

    private void Update()
    {
        transform.LookAt(lookTarget);
        transform.position = transform.position + Vector3.up * speed * Time.deltaTime;

        if (Time.time > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
