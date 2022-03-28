using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorManager : MonoBehaviour
{
    public GameManager gameManager;
    public static IndicatorManager manager;

    public GameObject prefab;
    public RectTransform container;

    public Dictionary<TrackObject, GameObject> prefabs =
        new Dictionary<TrackObject, GameObject>();
    public Dictionary<TrackObject, RectTransform> indicators =
        new Dictionary<TrackObject, RectTransform>();

    private void Awake()
    {
        manager = this;
    }

    private void LateUpdate()
    {
        foreach (var pair in indicators)
        {
            pair.Value.anchoredPosition = GetCanvasPosition(pair.Key);
        }

        foreach (var pair in prefabs)
        {
            if (gameManager.bookDisplay.isOpen || gameManager.optionDisplay.isOpen || gameManager.optionDisplay.isAdWinOpen || gameManager.optionDisplay.isMmWinOpen)
            {
                pair.Value.SetActive(false);
            }
            else
            {
                var cameraPosition = Camera.main.transform.position;
                var boxObj = pair.Key.gameObject;
                var vectorToItem = (boxObj.transform.position - cameraPosition);

                if (Vector3.Angle(vectorToItem, Camera.main.transform.forward) > 90) //It's behind us
                {
                    pair.Value.SetActive(false);
                }
                else pair.Value.SetActive(true);
            }
        }
    }

    private Vector2 GetCanvasPosition(TrackObject target)
    {
        var point = Camera.main.WorldToViewportPoint(target.transform.position);

        //point.x = Mathf.Clamp(point.x, 0f, 1f)
        point.x = Mathf.Clamp01(point.x);
        point.y = Mathf.Clamp01(point.y);

        var canvas = container.GetComponentInParent<Canvas>();
        var canvasRectTr = canvas.GetComponent<RectTransform>();
        point *= canvasRectTr.sizeDelta;

        return point;
    }

    public void Add(TrackObject target)
    {
        if (indicators.ContainsKey(target))
            return;

        var indicator = Instantiate(prefab, container);
        prefabs.Add(target, indicator);
        var indicatorRectTr = indicator.GetComponent<RectTransform>();

        indicatorRectTr.pivot = new Vector2(0.5f, 0.5f);
        indicatorRectTr.anchorMin = Vector2.zero;
        indicatorRectTr.anchorMax = Vector2.zero;
        indicatorRectTr.anchoredPosition = GetCanvasPosition(target);

        indicators.Add(target, indicatorRectTr);
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
