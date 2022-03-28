using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public GameManager manager;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //var script = other.gameObject.GetComponent<AnimalController>();
        //if (script == null) return;
        //if (script.satiety > 30) return;

        //var animal = script.currentAnimal;
        //foreach (var element in animal.eat)
        //{
        //    //æææ—.
        //    if (name.Equals("Seed"))
        //    {
        //        if (element.Equals("Seed"))
        //        {
        //            script.satiety += 50;
        //            manager.seedObjs.Remove(gameObject);

        //            StartCoroutine(CoDestroy(other));
        //        }
        //    }
        //    //¿‚√ .
        //    if (name.Equals("Grass"))
        //    {
        //        if (element.Equals("Grass"))
        //        {
        //            script.satiety += 50;
        //            manager.seedObjs.Remove(gameObject);

        //            StartCoroutine(CoDestroy(other));
        //        }
        //    }
        //}
    }

    //IEnumerator CoDestroy(Collider other)
    //{
        //var script = other.gameObject.GetComponent<AnimalController>();
        //script.isWalk = false;

        //var anim = other.gameObject.GetComponentInChildren<Animator>();
        //anim.SetBool("isPeck", true);
    
        //yield return new WaitForSeconds(3f);
        
        //anim.SetBool("isPeck", false);
        //Destroy(gameObject);
    //}
}
