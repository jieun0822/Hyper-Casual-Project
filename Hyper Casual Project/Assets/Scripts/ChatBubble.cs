using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    public Image image;
    private void Awake()
    {
        //this.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(this.transform.position);
        image.transform.position = Pos;
    }
}
