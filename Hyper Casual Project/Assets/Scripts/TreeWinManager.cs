using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeWinManager : MonoBehaviour
{
    public GameObject skillButton1;
    public GameObject skillButton2;
    public GameObject skillButton3;
    public GameObject skillButton4;

    public float skill1Timer;
    public float skill2Timer;
    public float skill3Timer;
    public float skill4Timer;

    float skill1Cycle;
    float skill2Cycle;
    float skill3Cycle;
    float skill4Cycle;

    //bool isCoSkill1;
    //bool isCoSkill2;
    //bool isCoSkill3;
    //bool isCoSkill4;

    public bool isSkill1;
    public bool isSkill2;
    public bool isSkill3;
    public bool isSkill4;

    public Text treeLevelTxt;
    public Text touchPointTxt;

    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        int treeLevel = manager.treeLevel;
        treeLevelTxt.text = $"Lv. {treeLevel} Tree";

        int touchPoint = manager.touchPoint;
        touchPointTxt.text = $"♥{touchPoint}/Tap";

        skill1Cycle = 30 * 60f;
        skill2Cycle = 45 * 60f;
        skill3Cycle = 20 * 60f;
        skill4Cycle = 5 * 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSkill1) skill1Timer += Time.deltaTime;
        if (isSkill2) skill2Timer += Time.deltaTime;
        if (isSkill3) skill3Timer += Time.deltaTime;
        if (isSkill4) skill4Timer += Time.deltaTime;

        if (skill1Timer > skill1Cycle)
        {
            isSkill1 = false;
            skill1Timer = 0;
        }

        if (skill2Timer > skill2Cycle)
        {
            isSkill2 = false;
            skill2Timer = 0;
        }

        if (skill3Timer > skill3Cycle)
        {
            isSkill3 = false;
            skill3Timer = 0;
        }

        if (skill4Timer > skill4Cycle)
        {
            isSkill4 = false;
            skill4Timer = 0;
        }

        int treeLevel = manager.treeLevel;
        treeLevelTxt.text = $"Lv. {treeLevel} Tree";

        int touchPoint = manager.touchPoint;
        touchPointTxt.text = $"♥ {touchPoint} / Tap";
    }

    public void OnSkillButton1()
    {
        //if (manager.treeLevel < 100) return;
        if (isSkill1) return;

        isSkill1 = true;

            if (manager.activeApples.Count != manager.maxApples)
            {
                for (int i = manager.activeApples.Count; i < manager.maxApples; i++)
                {
                    for (; ; )
                    {
                        int randomIndex = Random.Range(0, manager.hasApple.Length);
                        if (!manager.hasApple[randomIndex])
                        {
                            manager.hasApple[randomIndex] = true;
                            var apple = Instantiate(manager.appleObj, manager.appleSpawnPos[randomIndex].position, Quaternion.identity);
                            manager.activeApples.Add(randomIndex, apple);
                            break;
                        }
                    }
                }
            }

            //StartCoroutine(CoSkill1());
    }

    //IEnumerator CoSkill1()
    //{
    //    isCoSkill1 = true;
    //    yield return new WaitForSeconds(30 * 60f);
    //    isCoSkill1 = false;
    //}

    public void OnSkillButton2()
    {
        //if (manager.treeLevel < 250) return;
        if (isSkill2) return;

        isSkill2 = true;

        int randomNum = Random.Range(0, 100);
            if (randomNum < 25)
            {
                Dictionary<int, GameObject> goldAppleObj = new Dictionary<int, GameObject>();

                for (int i = 0; i < manager.hasApple.Length; i++)
                {
                    if (!manager.activeApples.ContainsKey(i)) continue;

                    var goldApple = Instantiate(manager.goldAppleObj, manager.activeApples[i].transform.position, manager.activeApples[i].transform.rotation);
                    goldAppleObj.Add(i, goldApple);
                    var script = goldApple.GetComponent<Apple>();
                    script.key = i;

                    Destroy(manager.activeApples[i]);
                    manager.activeApples.Remove(i);
                }

                for (int i = 0; i < manager.hasApple.Length; i++)
                {
                    if (!goldAppleObj.ContainsKey(i)) continue;
                    manager.activeApples.Add(i, goldAppleObj[i]);
                }
            }

            //StartCoroutine(CoSkill2());
    }

    //IEnumerator CoSkill2()
    //{
    //    isCoSkill2 = true;
    //    yield return new WaitForSeconds(45 * 60f);
    //    isCoSkill2 = false;
    //}

    //스킬3.
    public void OnSkillButton3()
    {
        //if (manager.treeLevel < 500) return;
        if (isSkill3) return;

        isSkill3 = true;

        int randomNum = Random.Range(0, manager.specialAnimal.Count);
        string animalName = manager.specialAnimal[randomNum];

        GameObject prefab;
        prefab = Instantiate(manager.animals[animalName].prefab, 
            new Vector3(Random.Range(0, 10), 0f, Random.Range(0, 10)), Quaternion.identity);

        Animal currentAnimal = manager.animals[animalName];

        manager.activeObjs.Add(prefab);
        var script = prefab.GetComponent<AnimalController>();
        script.currentAnimal = currentAnimal;
        script.satiety = currentAnimal.satiety;
        script.lifeCycle = currentAnimal.lifeCycle;
        script.isSpecial = currentAnimal.isSpecial;
        script.manager = manager;


        //StartCoroutine(CoSkill3());
    }

    //IEnumerator CoSkill3()
    //{
    //    isCoSkill3 = true;
    //    yield return new WaitForSeconds(20 * 60f);
    //    isCoSkill3 = false;
    //}

    //스킬4.
    public void OnSkillButton4()
    {
        //if (manager.treeLevel < 1000) return;
        if (isSkill4) return;

        isSkill4 = true;

        //StartCoroutine(CoSkill4());
    }

    //IEnumerator CoSkill4()
    //{
    //    isCoSkill4 = true;

    //    //광고시청 구현.
    //    isCoSkill1 = false;
    //    isCoSkill2 = false;
    //    isCoSkill3 = false;

    //    yield return new WaitForSeconds(5 * 60f);
    //    isCoSkill4 = false;
    //}
}
