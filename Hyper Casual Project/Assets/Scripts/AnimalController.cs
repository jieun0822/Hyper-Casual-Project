using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    //파티클.
    public GameObject birthEffect;
    bool playBirthEffect;
    float birthEffectTime;

    public GameObject waterEffect;
    public bool playWaterEffect;
    float waterEffectTime;
    bool isEatFish;

    public GameObject specialEffect;
    public bool playSpecialEffect;
    float specialEffectTime;

    public Animal currentAnimal;

    public float satiety; // 포만감.
    float satietyTimer;//포만감 줄어드는 시간.
    public float lifeCycle;
    float lifeTimer;//생명 줄어드는 시간.
    public bool isSpecial;
    //float chickenTimer;//닭으로 변화하는 시간.

    public GameManager manager;
    public float stayTimer;//머물러 있는 시간.
    public float moveCycle;
    int waypointIndex = -1;
    NavMeshAgent agent;

    public int randomIndex;

    public Animator anim;
    public bool isWalk;
    public bool isSpin;
    public bool isRoll;
    public bool isPeck;
    public bool isBounce;
    public bool isClicked;
    public bool isFear;
    public bool isIdle_B;
    public bool isIdle_C;
    public bool isJump;
    public bool isFly;
    public bool isSwim;

    public bool isDie;
    public bool isStarve;

    public GameObject enemy;
    GameObject food;

    public Image chatBubble;
    Image chatImg;
    //병아리

    bool isCoEat;
    bool isChase;
    float originY;

    Sprite[] eatImgs;
    int displayIndex;
    bool isCoFoodDisplay;

    bool eatSeed;
    bool eatGrass;
    bool eatApple;
    bool eatBanana;
    bool eatAnimal;

    bool eatGrassApple;
    bool eatGrassApple_grass;
    bool eatGrassApple_apple;
   
    bool eatAppleBanana;
    bool eatAppleBanana_apple;
    bool eatAppleBanana_banana;

    float min;//포만감 계산용.

    // Start is called before the first frame update
    void Start()
    {
        moveCycle = 10f;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        var chatImgObj = chatBubble.gameObject.transform.GetChild(0);
        chatImg = chatImgObj.GetComponent<Image>();

        originY = gameObject.transform.position.y;

        //if (currentAnimal == null)
        //{
        //    var script = gameObject.GetComponent<AnimalName>();
        //    currentAnimal = manager.animals[script.name];
        //}
        eatImgs = new Sprite[currentAnimal.eat.Length];

        foreach (var element in currentAnimal.eat)
        {
            if (element.Equals("Seed")) eatSeed = true;
            else if (element.Equals("Grass")) eatGrass = true;
            else if (element.Equals("Apple")) eatApple = true;
            else if (element.Equals("Banana")) eatBanana = true;
            else eatAnimal = true;
        }

        if (eatGrass && eatApple)
        {
            eatGrass = false;
            eatApple = false;
            eatGrassApple = true;
        }
        else if (eatApple && eatBanana)
        {
            eatAppleBanana = true;
            eatApple = false;
            eatBanana = false;
        }
        min = int.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        satietyTimer += Time.deltaTime;
        lifeTimer += Time.deltaTime;
        stayTimer += Time.deltaTime;

        //탄생 이펙트.
        if (playBirthEffect) birthEffectTime += Time.deltaTime;
        
        if (birthEffectTime > 0)
        {
            birthEffect.SetActive(true);
        }
        if (birthEffectTime > 1)
        {
            playBirthEffect = false;
            birthEffectTime = 0f;
            birthEffect.SetActive(false);
        }

            //물 이펙트.
        if (playWaterEffect)
        {
            waterEffect.SetActive(true);
            waterEffectTime += Time.deltaTime;
        }
        if (waterEffectTime > 1)
        {
            playWaterEffect = false;
            waterEffectTime = 0f;
            waterEffect.SetActive(false);
        }

        if (playSpecialEffect)
        {
            specialEffect.SetActive(true);
            specialEffectTime += Time.deltaTime;
        }
        if (specialEffectTime > 1f) specialEffect.SetActive(false);

        if (!isDie && !isStarve)
        {
            //움직임. 적이 없을 때.
            if (stayTimer > moveCycle && enemy == null)
            {
                stayTimer = 0f;

                randomIndex = -1;
                if (currentAnimal.isFish) randomIndex = Random.Range(10, 13);
                else if (currentAnimal.name.Equals("Chick") ||
                   currentAnimal.name.Equals("Hen") || currentAnimal.name.Equals("Cock") ||
                   currentAnimal.name.Equals("Mouse") || currentAnimal.name.Equals("Squirrel")||
                   currentAnimal.name.Equals("Rabbit") || currentAnimal.name.Equals("Mole"))
                    randomIndex = Random.Range(0, 10);
                else randomIndex = Random.Range(0, 13);

                for (int i = randomIndex; i < manager.isOccupied.Length; i++)
                {
                    if (manager.isOccupied[i]) continue;
                    if (!currentAnimal.swimmable && (i == 10 || i == 11 || i == 12)) continue;
                    if (currentAnimal.isFish && !(i == 10 || i == 11 || i == 12)) continue;
                    if ((currentAnimal.name.Equals("Chick") ||
                   currentAnimal.name.Equals("Hen") || currentAnimal.name.Equals("Cock") ||
                   currentAnimal.name.Equals("Mouse") || currentAnimal.name.Equals("Squirrel") ||
                   currentAnimal.name.Equals("Rabbit") || currentAnimal.name.Equals("Mole")) &&
                   (i == 10 || i == 11 || i == 12)) continue;
                    if (waypointIndex != -1) manager.isOccupied[waypointIndex] = false;
                    waypointIndex = i;
                    manager.isOccupied[i] = true;
                    break;
                }

                if (waypointIndex != -1)
                {
                    agent.enabled = true;
                    agent.destination = manager.wayPoint[waypointIndex].position;
                }

                //랜덤 idle 정하기.
                anim.SetBool("isWalk", false);
                if (!currentAnimal.isFish)  anim.SetBool("isSpin", false);
                anim.SetBool("isRoll", false);
                anim.SetBool("isPeck", false);
                anim.SetBool("isBounce", false);
                anim.SetBool("isClicked", isClicked);
                anim.SetBool("isFear", false);
                anim.SetBool("isIdle_B", false);
                anim.SetBool("isIdle_C", false);
                anim.SetBool("isJump", false);
                anim.SetBool("isFly", false);
                anim.SetBool("isSwim", false);

                if (waypointIndex == 10 || waypointIndex == 11 || waypointIndex == 12)
                {
                    isSwim = true;
                }
                else
                {
                    int randomNum = Random.Range(0, 100);

                    if (randomNum < 5)
                    {
                        if (currentAnimal.isFish) isBounce = true;
                        else isSpin = true;
                    }
                    else if (randomNum < 10) isRoll = true;
                    else if (randomNum < 20) isBounce = true;
                    else if (randomNum < 30) isJump = true;//isClicked = true;
                    else if (randomNum < 40) isFear = true;
                    else if (randomNum < 50) isIdle_B = true;
                    else if (randomNum < 60) isIdle_C = true;
                    else if (randomNum < 70) isJump = true;
                    else if (randomNum < 80)
                    {
                        if (currentAnimal.flyable) isFly = true;
                    }
                }
            }

            //적이 쫓아오지 않을 때.
            else if (stayTimer > moveCycle && enemy != null && Vector3.Distance(transform.position, enemy.transform.position) >= 6f)
            {
                stayTimer = 0f;

                int randomIndex = Random.Range(0, 4);
                for (int i = randomIndex; i < manager.isOccupied.Length; i++)
                {
                    if (manager.isOccupied[i]) continue;

                    if (waypointIndex != -1) manager.isOccupied[waypointIndex] = false;
                    waypointIndex = i;
                    manager.isOccupied[i] = true;
                    break;
                }

                agent.enabled = true;
                agent.destination = manager.wayPoint[waypointIndex].position;
            }

            if (waypointIndex != -1 &&
                Vector3.Distance(transform.position, manager.wayPoint[waypointIndex].position) >= 2f)
            {

                anim.SetBool("isSpin", false);
                anim.SetBool("isRoll", false);
                anim.SetBool("isBounce", false);
                anim.SetBool("isClicked", isClicked);
                anim.SetBool("isFear", false);
                anim.SetBool("isIdle_B", false);
                anim.SetBool("isIdle_C", false);
                anim.SetBool("isJump", false);
                anim.SetBool("isFly", false);
                anim.SetBool("isSwim", false);

                if (!isClicked) anim.SetBool("isWalk", true);
            }
            else if(waypointIndex != -1)
            {
                anim.SetBool("isWalk", false);

                anim.SetBool("isSpin", isSpin);
                anim.SetBool("isRoll", isRoll);
                anim.SetBool("isBounce", isBounce);
                anim.SetBool("isClicked", isClicked);
                anim.SetBool("isFear", isFear);
                anim.SetBool("isIdle_B", isIdle_B);
                anim.SetBool("isIdle_C", isIdle_C);
                anim.SetBool("isJump", isJump);
                anim.SetBool("isFly", isFly);
                anim.SetBool("isSwim", isSwim);
            }

            //생명&포만감 감소.
            if (lifeTimer > 1f) //1초마다 줆.
            {
                lifeTimer = 0f;
                lifeCycle -= 1;
                //수명 다 됐을때.
                if (lifeCycle <= 0)
                {
                    isDie = true;

                    if (currentAnimal.name == "Chick")
                    {
                        manager.animals[currentAnimal.name].totalNum--;

                        Animal chicken;
                        int ranNum = Random.Range(0, 10);
                        if (ranNum < 5) manager.animals.TryGetValue("Hen", out chicken);
                        else manager.animals.TryGetValue("Cock", out chicken);

                        var chickenObj = Instantiate(chicken.prefab, transform.position, transform.rotation);
                        var chickenScript = chickenObj.GetComponent<AnimalController>();
                        chickenScript.currentAnimal = chicken;
                        chickenScript.satiety = chicken.satiety;
                        chickenScript.lifeCycle = chicken.lifeCycle;
                        chickenScript.isSpecial = chicken.isSpecial;
                        chickenScript.manager = manager;
                        manager.activeObjs.Add(chickenObj);
                        manager.animals[chickenScript.currentAnimal.name].totalNum++;

                        manager.activeObjs.Remove(gameObject);
                        Destroy(gameObject);
                    }
                    else if (!currentAnimal.isSpecial)
                    {
                        agent.enabled = false;
                        manager.activeObjs.Remove(gameObject);
                        manager.animals[currentAnimal.name].totalNum--;
                        if (currentAnimal.isFish) manager.fishNum--;
                        anim.SetBool("isDead", true);

                        StartCoroutine(CoDead(gameObject));
                    }
                    else
                    {
                        agent.enabled = false;
                        manager.activeSpecialObjs.Remove(gameObject);
                       
                        anim.SetBool("isDead", true);
                        Destroy(gameObject, 1f);
                    }

                    if (waypointIndex != -1) manager.isOccupied[waypointIndex] = false;
                    waypointIndex = -1;
                }//lifeCycle<0
            }

            if (!currentAnimal.isSpecial)
            {
                if (satietyTimer > 10f) //10초마다 줆.
                {
                    satietyTimer = 0f;
                    satiety -= 1;
                }
                //포만감 일정수치 이하.
                if (satiety <= currentAnimal.satiety*0.5)
                {
                    if (!manager.bookDisplay.isOpen && !manager.optionDisplay.isOpen && !manager.optionDisplay.isAdWinOpen &&!manager.optionDisplay.isMmWinOpen)
                    {
                        chatBubble.enabled = true;
                        chatImg.enabled = true;
                    }
                    else
                    {
                        chatBubble.enabled = false;
                        chatImg.enabled = false;
                    }

                    var chatPos = chatBubble.GetComponent<RectTransform>();
                    var buttonPos = manager.animalWinButton.GetComponent<RectTransform>();
                    if (chatPos.position.y - chatPos.sizeDelta.y * chatPos.localScale.y < buttonPos.position.y)
                    {
                        chatBubble.enabled = false;
                        chatImg.enabled = false;
                    }

                    //씨앗.
                    if (eatSeed)
                    {
                        float seedMin = int.MaxValue;
                        foreach (var elementObj in manager.seedObjs)
                        {
                            if (seedMin > Vector3.Distance(transform.position, elementObj.transform.position))
                            {
                                seedMin = Vector3.Distance(transform.position, elementObj.transform.position);
                                food = elementObj;
                                chatImg.sprite = manager.detailManger.seedImg;
                            }
                        }
                    }
                    else if (eatGrass)
                    {
                        float grassMin = int.MaxValue;
                        foreach (var elementObj in manager.grassObjs)
                        {
                            if (grassMin > Vector3.Distance(transform.position, elementObj.transform.position))
                            {
                                grassMin = Vector3.Distance(transform.position, elementObj.transform.position);
                                food = elementObj;
                                chatImg.sprite = manager.detailManger.grassImg;
                            }
                        }
                    }
                    else if (eatApple)
                    {
                        float appleMin = int.MaxValue;
                        foreach (var elementObj in manager.activeApples)
                        {
                            var script = elementObj.Value.GetComponent<Apple>();
                            if (!script.isCollide) continue;
                            if (appleMin > Vector3.Distance(transform.position, elementObj.Value.transform.position))
                            {
                                appleMin = Vector3.Distance(transform.position, elementObj.Value.transform.position);
                                food = elementObj.Value;
                                chatImg.sprite = manager.detailManger.appleImg;
                            }
                        }
                    }
                    else if (eatGrassApple)
                    {
                        float grassMin = int.MaxValue;
                        GameObject grass = null;
                        foreach (var elementObj in manager.grassObjs)
                        {
                            if (grassMin > Vector3.Distance(transform.position, elementObj.transform.position))
                            {
                                grassMin = Vector3.Distance(transform.position, elementObj.transform.position);
                                grass = elementObj;
                            }
                        }

                        float appleMin = int.MaxValue;
                        GameObject apple = null;
                        foreach (var elementObj in manager.activeApples)
                        {
                            var script = elementObj.Value.GetComponent<Apple>();
                            if (!script.isCollide) continue;
                            if (appleMin > Vector3.Distance(transform.position, elementObj.Value.transform.position))
                            {
                                appleMin = Vector3.Distance(transform.position, elementObj.Value.transform.position);
                                apple = elementObj.Value;
                            }
                        }

                        if (grassMin < appleMin)
                        {
                            eatGrassApple_grass = true;
                            food = grass;
                            chatImg.sprite = manager.detailManger.grassImg;
                        }
                        else
                        {
                            eatGrassApple_apple = true;
                            food = apple;
                            chatImg.sprite = manager.detailManger.appleImg;
                        }
                    }
                    else if (eatAppleBanana)
                    {
                        float appleMin = int.MaxValue;
                        GameObject apple = null;
                        foreach (var elementObj in manager.activeApples)
                        {
                            var script = elementObj.Value.GetComponent<Apple>();
                            if (!script.isCollide) continue;
                            if (appleMin > Vector3.Distance(transform.position, elementObj.Value.transform.position))
                            {
                                appleMin = Vector3.Distance(transform.position, elementObj.Value.transform.position);
                                apple = elementObj.Value;
                            }
                        }

                        float bananaMin = int.MaxValue;
                        GameObject banana = null;
                        foreach (var elementObj in manager.activeBananas)
                        {
                            var script = elementObj.Value.GetComponent<Apple>();
                            if (!script.isCollide) continue;
                            if (bananaMin > Vector3.Distance(transform.position, elementObj.Value.transform.position))
                            {
                                bananaMin = Vector3.Distance(transform.position, elementObj.Value.transform.position);
                                banana = elementObj.Value;
                            }
                        }

                        if (bananaMin < appleMin)
                        {
                            eatAppleBanana_banana = true;
                            food = banana;
                            chatImg.sprite = manager.detailManger.bananaImg;
                        }
                        else
                        {
                            eatAppleBanana_apple = true;
                            food = apple;
                            chatImg.sprite = manager.detailManger.appleImg;
                        }

                    }
                    else if (eatAnimal)
                    {
                        //동물.
                        min = int.MaxValue;
                        foreach (var elementObj in manager.activeObjs)
                        {
                            foreach (var element in currentAnimal.eat)
                            {
                                var script = elementObj.GetComponent<AnimalController>();
                                if (script.currentAnimal.name != element) continue;
                                if (min > currentAnimal.level - script.currentAnimal.level)
                                {
                                    min = currentAnimal.level - script.currentAnimal.level;
                                    food = elementObj;
                                    chatImg.sprite = script.currentAnimal.unlockImg;
                                }
                            }
                        }
                    }

                    if (food != null)
                    {
                        if (waypointIndex != -1) manager.isOccupied[waypointIndex] = false;
                        waypointIndex = -1;

                        anim.SetBool("isSpin", false);
                        anim.SetBool("isRoll", false);
                        anim.SetBool("isBounce", false);
                        anim.SetBool("isClicked", isClicked);
                        anim.SetBool("isFear", false);
                        anim.SetBool("isIdle_B", false);
                        anim.SetBool("isIdle_C", false);
                        anim.SetBool("isJump", false);
                        anim.SetBool("isFly", false);
                        anim.SetBool("isSwim", false);
                        anim.SetBool("isWalk", true);

                        agent.enabled = true;
                        agent.destination = food.transform.position;
                        agent.speed = 5f;
                        transform.LookAt(food.transform.position);

                        StopCoroutine(CoFoodDisplay());

                        //포식자가 잡아먹음.
                        if (Vector3.Distance(transform.position, food.transform.position) < 3f)
                        {
                            if (eatAnimal)
                            {
                                for (int i = 0; i < manager.activeObjs.Count; i++)
                                {
                                    if (manager.activeObjs[i] == food)
                                    {
                                        var preyAnim = manager.activeObjs[i].GetComponentInChildren<Animator>();
                                        preyAnim.SetBool("isWalk", false);
                                        preyAnim.SetBool("isDead", true);

                                        Destroy(manager.activeObjs[i], 1f);

                                        var preyScript = manager.activeObjs[i].GetComponent<AnimalController>();
                                        var preyName = preyScript.currentAnimal.name;

                                        Animal preyAnimal = preyScript.currentAnimal;

                                        if (preyScript.currentAnimal.isFish)
                                        {
                                            isEatFish = true;
                                            manager.fishNum--;
                                        }
                                        else if (preyScript.currentAnimal.name.Equals("Chick")||
                                           preyScript.currentAnimal.name.Equals("Cock") || preyScript.currentAnimal.name.Equals("Hen"))
                                        {
                                            manager.chickenNum--;
                                        }

                                        manager.animals[preyName].totalNum--;

                                        manager.activeObjs.RemoveAt(i);

                                        satiety += (1 / (float)min) * 100;

                                        if (!isCoEat)
                                            StartCoroutine(CoEatAnimal(preyAnimal));
                                    }
                                }
                            }
                            else if (eatSeed)
                            {
                                //씨앗.
                                for (int i = 0; i < manager.seedObjs.Count; i++)
                                {
                                    if (manager.seedObjs[i] == food)
                                    {
                                        Destroy(manager.seedObjs[i], 1f);
                                        manager.seedObjs.RemoveAt(i);

                                        satiety += 10;

                                        if (!isCoEat)
                                            StartCoroutine(CoEat());
                                    }
                                }
                            }
                            else if (eatGrass)
                            {
                                //풀.
                                for (int i = 0; i < manager.grassObjs.Count; i++)
                                {
                                    if (manager.grassObjs[i] == food)
                                    {
                                        Destroy(manager.grassObjs[i], 1f);
                                        manager.grassObjs.RemoveAt(i);

                                        satiety += 10;

                                        if (!isCoEat)
                                            StartCoroutine(CoEat());
                                    }
                                }
                            }
                            else if (eatApple)
                            {
                                foreach (var element in manager.activeApples)
                                {
                                    if (element.Value != food) continue;

                                    Destroy(element.Value, 1f);
                                    manager.activeApples.Remove(element.Key);
                                    manager.hasApple[element.Key] = false;

                                    satiety += 22;

                                    if (!isCoEat)
                                        StartCoroutine(CoEat());
                                    break;
                                }
                            }
                            else if (eatGrassApple)
                            {
                                if (eatGrassApple_grass)
                                {
                                    for (int i = 0; i < manager.grassObjs.Count; i++)
                                    {
                                        if (manager.grassObjs[i] != food) continue;

                                        Destroy(manager.grassObjs[i], 1f);
                                        manager.grassObjs.RemoveAt(i);

                                        satiety += 10;

                                        if (!isCoEat)
                                            StartCoroutine(CoEat());
                                    }
                                }
                                else if (eatGrassApple_apple)
                                {
                                    foreach (var element in manager.activeApples)
                                    {
                                        if (element.Value != food) continue;

                                        Destroy(element.Value, 1f);
                                        manager.activeApples.Remove(element.Key);
                                        manager.hasApple[element.Key] = false;

                                        satiety += 22;

                                        if (!isCoEat)
                                            StartCoroutine(CoEat());
                                        break;
                                    }
                                }

                                eatGrassApple_grass = false;
                                eatGrassApple_apple = false;
                            }
                            else if (eatAppleBanana)
                            {
                                if (eatAppleBanana_banana)
                                {
                                    foreach (var element in manager.activeBananas)
                                    {
                                        if (element.Value != food) continue;

                                        Destroy(element.Value, 1f);
                                        manager.activeBananas.Remove(element.Key);
                                        manager.hasBanana[element.Key] = false;

                                        satiety += 55;

                                        if (!isCoEat)
                                            StartCoroutine(CoEat());
                                        break;
                                    }
                                }
                                else if (eatAppleBanana_apple)
                                {
                                    foreach (var element in manager.activeApples)
                                    {
                                        if (element.Value != food) continue;

                                        Destroy(element.Value, 1f);
                                        manager.activeApples.Remove(element.Key);
                                        manager.hasApple[element.Key] = false;

                                        satiety += 22;

                                        if (!isCoEat)
                                            StartCoroutine(CoEat());
                                        break;
                                    }
                                }

                                eatAppleBanana_banana = false;
                                eatAppleBanana_apple = false;
                            }

                            food = null;
                            agent.speed = 3.5f;
                        }
                    }//food!=null.
                    else
                    {
                        if (!isCoFoodDisplay)
                        {
                            int index = 0;
                            foreach (var eats in currentAnimal.eat)
                            {
                                if (eats.Equals("Seed")) eatImgs[index++] = manager.detailManger.seedImg;
                                else if (eats.Equals("Grass")) eatImgs[index++] = manager.detailManger.grassImg;
                                else if (eats.Equals("Apple")) eatImgs[index++] = manager.detailManger.appleImg;
                                else if (eats.Equals("Banana")) eatImgs[index++] = manager.detailManger.bananaImg;
                                else
                                {
                                    if (manager.animals.ContainsKey(eats))
                                    {
                                        eatImgs[index++] = manager.animals[eats].unlockImg;
                                    }
                                    else if(eats.Equals("Chicken")) eatImgs[index++] = manager.animals["Cock"].unlockImg;
                                }
                            }

                            StartCoroutine(CoFoodDisplay());
                            //food == null;
                        }
                    }
                }//포만감 일정수치 이하.
                else
                {
                    chatBubble.enabled = false;
                    chatImg.enabled = false;
                }
                //굶어죽었을 때.
                if (satiety <= 0)
                {
                    agent.enabled = false;
                    if (waypointIndex != -1) manager.isOccupied[waypointIndex] = false;
                    waypointIndex = -1;

                    isStarve = true;

                    manager.activeObjs.Remove(gameObject);
                    manager.animals[currentAnimal.name].totalNum--;
                    if (currentAnimal.isFish) manager.fishNum--;
                    else if (currentAnimal.name.Equals("Chick") || currentAnimal.name.Equals("Cock") || currentAnimal.name.Equals("Hen"))
                        manager.chickenNum--;
                    anim.SetBool("isDead", true);
                    Destroy(gameObject, 1f);
                }
            }//스페셜 동물이 아닐때.
            else
            {
                chatBubble.enabled = false;
                chatImg.enabled = false;
            }
        }
    }//Update.

    private void OnTriggerStay(Collider other)
    {
        if (!currentAnimal.isFish)
        {
            if (other.gameObject.layer.Equals("Animal"))
            {
                foreach (var element in currentAnimal.eaten)
                {
                    var enemyScript = other.GetComponent<AnimalController>();

                    if (enemyScript == null) return;
                    if (element == enemyScript.currentAnimal.name)
                    {
                        enemy = other.gameObject;

                        if (waypointIndex != -1) manager.isOccupied[waypointIndex] = false;
                        waypointIndex = -1;

                        anim = GetComponentInChildren<Animator>();
                        if (Vector3.Distance(enemy.transform.position, transform.position) > 3f)
                        {
                            anim.SetBool("isSpin", false);
                            anim.SetBool("isRoll", false);
                            anim.SetBool("isBounce", false);
                            anim.SetBool("isClicked", false);
                            anim.SetBool("isFear", false);
                            anim.SetBool("isIdle_B", false);
                            anim.SetBool("isIdle_C", false);
                            anim.SetBool("isJump", false);
                            anim.SetBool("isFly", false);
                            anim.SetBool("isSwim", false);
                            anim.SetBool("isWalk", true);
                        }
                        Vector3 dirToEnemy = transform.position - other.gameObject.transform.position;
                        Vector3 newPos = transform.position + dirToEnemy;
                        agent = GetComponent<NavMeshAgent>();
                        agent.enabled = true;
                        agent.SetDestination(newPos);
                    }
                }
            }
        }
    }

    IEnumerator CoEatAnimal(Animal preyAnimal)
    {
        isCoEat = true;

        anim.SetBool("isWalk", false);
        anim.SetBool("isPeck", true);
        
        yield return new WaitForSeconds(0.5f);
        if (isEatFish)
        {
            playWaterEffect = true;
            isEatFish = false;
        }
        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isPeck", false);

        if (manager.activeSpecialObjs.Count > 0) manager.vitality += 2 * preyAnimal.getVitality_feed;
        else manager.vitality += preyAnimal.getVitality_feed;

        var pos = transform.position;
        pos.y += 1f;

        var newGo = Instantiate(manager.vitalityTxtObj, pos, Quaternion.identity);
        var st = newGo.GetComponent<ScrollingText>();
        st.Init(preyAnimal.getVitality_feed, Color.white);

        food = null;
        stayTimer += moveCycle;

        isCoEat = false;
    }

    IEnumerator CoEat()
    {
        isCoEat = true;

        anim.SetBool("isWalk", false);
        anim.SetBool("isPeck", true);

        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isPeck", false);

        food = null;
        stayTimer += moveCycle;

        isCoEat = false;
    }

    IEnumerator CoDead(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        int getVitality;
        if(currentAnimal.name.Equals("Cock") || currentAnimal.name.Equals("Hen")) 
            getVitality = (int)(manager.animals["Chick"].payVitality * 0.2);
        else getVitality = (int)(currentAnimal.payVitality * 0.2);

        if(manager.activeSpecialObjs.Count > 0) manager.vitality += 2 * getVitality;
        else manager.vitality += getVitality;

        //생명력 획득.
        var pos = transform.position;
        pos.y += 1f;

        var newGo = Instantiate(manager.vitalityTxtObj, pos, Quaternion.identity);
        var st = newGo.GetComponent<ScrollingText>();
        st.Init(getVitality, Color.white);

        //생성.
        GameObject prefab;

        if (currentAnimal.name.Equals("Hen") || currentAnimal.name.Equals("Cock"))
        {
            prefab = Instantiate(manager.animals["Chick"].prefab, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);

            var script = prefab.GetComponent<AnimalController>();
            script.currentAnimal = manager.animals["Chick"];
            script.satiety = currentAnimal.satiety;
            script.lifeCycle = currentAnimal.lifeCycle;
            script.isSpecial = currentAnimal.isSpecial;
            script.manager = manager;
            script.playBirthEffect = true;

            manager.animals[script.currentAnimal.name].totalNum++;
            manager.activeObjs.Add(prefab);
        }
        else if(!currentAnimal.isSpecial)
        {
            prefab = Instantiate(currentAnimal.prefab, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);

            var script = prefab.GetComponent<AnimalController>();
            script.currentAnimal = currentAnimal;
            script.satiety = currentAnimal.satiety;
            script.lifeCycle = currentAnimal.lifeCycle;
            script.isSpecial = currentAnimal.isSpecial;
            script.manager = manager;
            script.playBirthEffect = true;

            manager.animals[script.currentAnimal.name].totalNum++;
            if (script.currentAnimal.isFish) manager.fishNum++;
            manager.activeObjs.Add(prefab);
        }
        
        Destroy(obj);
    }

    IEnumerator CoFoodDisplay()
    {
        isCoFoodDisplay = true;
        while (chatImg.enabled == true)
        {
            if (displayIndex >= eatImgs.Length) displayIndex = 0;
            chatImg.sprite = eatImgs[displayIndex++];
            yield return new WaitForSeconds(1f);
        }
        isCoFoodDisplay = false;
    }
}
