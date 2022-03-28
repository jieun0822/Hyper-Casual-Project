using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeMove : MonoBehaviour
{
    //private CharacterController controller;
    //public GameObject[] spheres;
    //float maxDistance;
    //float minDistance = Mathf.Infinity;
    NavMeshAgent agent;
    public GameObject target;
    public bool check;
    private void Start()
    {
        //controller = gameObject.AddComponent<CharacterController>();
         agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (check)
        {
            agent.destination = target.transform.position;
            gameObject.transform.LookAt(target.transform.position);
        }//var hAxis = Input.GetAxisRaw("Horizontal");
        //var vAxis = Input.GetAxisRaw("Vertical");
        //var moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //Debug.Log($"{hAxis}");
        //transform.position += moveVec * 10 * Time.deltaTime;

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //controller.Move(move * Time.deltaTime * 10);
        //var speed = 10f;
        //var h = Input.GetAxis("Horizontal");
        //var v = Input.GetAxis("Vertical");

        //var upDown = Input.GetAxis("UpDown");

        //transform.position += (transform.forward * h + transform.up * v) * Time.deltaTime * speed;
        //transform.Rotate(0f, h * 360 * Time.deltaTime, 0f);

        //maxDistance = 0;
        //minDistance = Mathf.Infinity;

        //foreach (var sphere in spheres)
        //{
        //    Vector3 diff = sphere.transform.position - transform.position;
        //    float dist = diff.sqrMagnitude;
        //    if (maxDistance < dist) maxDistance = dist;
        //    if (minDistance > dist) minDistance = dist;
        //}

        //foreach (var sphere in spheres)
        //{
        //    Vector3 diff = sphere.transform.position - transform.position;
        //    float dist = diff.sqrMagnitude;
        //    if (dist == minDistance)
        //    {
        //        var ren = sphere.GetComponent<MeshRenderer>();
        //        ren.material.color = new Color(1, 1, 1);
        //    }
        //    else if (dist == maxDistance)
        //    {
        //        var ren = sphere.GetComponent<MeshRenderer>();
        //        ren.material.color = new Color(0, 0, 0);
        //    }
        //    else
        //    {
        //        float newColor = (dist - minDistance) / (maxDistance - minDistance);

        //        var ren = sphere.GetComponent<MeshRenderer>();
        //        //ren.material.color = new Color(newColor, newColor, newColor);
        //        ren.material.color = Color.Lerp(Color.white, Color.black, newColor);
        //    }
        //}
    }
}



