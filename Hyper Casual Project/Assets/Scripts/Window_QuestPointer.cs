using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_QuestPointer : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    // Start is called before the first frame update
    void Awake()
    {
        targetPosition = target.transform.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        //fromPosition.z = 0;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
