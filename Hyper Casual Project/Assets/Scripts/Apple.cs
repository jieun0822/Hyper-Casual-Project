using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public int key;
    public bool isCollide;
    public bool isTouched;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        rigid = GetComponent<Rigidbody>();
        //if (other.gameObject.layer == LayerMask.NameToLayer("Tree"))
        //{
        //    rigid.useGravity = false;
        //    rigid.isKinematic = true;
        //}
        //else if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        //{
        //    rigid.useGravity = true;
        //    rigid.isKinematic = true;
        //}
        //else 
        //{
        //    rigid.useGravity = true;
        //    rigid.isKinematic = false;
        //}
        if (other.gameObject.layer == LayerMask.NameToLayer("Tree") && !isTouched)
        {
            rigid.useGravity = false;
            rigid.isKinematic = true;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            isCollide = true;
            rigid.useGravity = true;
            rigid.isKinematic = true;
        }
    }
}
