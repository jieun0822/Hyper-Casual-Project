using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdBubble : MonoBehaviour
{
    public GameManager manager;
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        var cameraPosition = Camera.main.transform.position;
        var vectorToItem = (gameObject.transform.position - cameraPosition);

        if (manager == null) return;
        if (!manager.bookDisplay.isOpen && !manager.optionDisplay.isOpen && !manager.optionDisplay.isAdWinOpen &&!manager.optionDisplay.isMmWinOpen)
        {
            if (Vector3.Angle(vectorToItem, Camera.main.transform.forward) > 90) //It's behind us
            {
                image.SetActive(false);
            }
            else
            {
                image.SetActive(true);
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            }
        }
        else image.SetActive(false);
    }
}
