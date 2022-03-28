using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq; // To sort.

public class GameManager : MonoBehaviour
{
    //테스트용.
    public Text vitalityTxt;
    public Text treeLevelTxt;
    public Text expTxt;
    public Text totalExpTxt;

    public IndicatorManager indicatorManager;
    public SoundManager soundManager;

    //메인화면 버튼 for chat bubble
    public GameObject treeWinButton;
    public GameObject animalWinButton;
    public GameObject CameraWinButton;
    public GameObject SettingWinButton;
    public GameObject MenuButton;

    public Sprite returnImg;
    public Sprite menuImg;

    //New 창.
    public BookDisplay bookDisplay;
    public OptionDisplay optionDisplay;

    //창
    public bool isTreeWin;
    public bool isAnimalWin;
    public bool isSettingWin;

    public bool cameraMode;
    public Animal[] animalsInfo;

    //상자.
    public Transform[] boxSpawnPoint;
    public bool[] hasBox;
    public GameObject boxObj;
    GameObject currentBox;
    public bool isBoxExist;
    public float boxTimer;
    public float boxCycle;

    //생성된 동물들.
    public List<GameObject> activeObjs; //이거씀!
    public List<GameObject> activeSpecialObjs;
    public GameObject chatBubble;
    public int maxAnimal;
    public int fishNum;
    public int chickenNum;

    //생성된 씨앗.
    public GameObject seedObj;
    public List<GameObject> seedObjs;
    float seedTimer;
    public float seedCycle = 5f;
    public int maxSeeds = 10;

    //생성된 풀.
    //Grass 10초마다 젠, 맵에 최대 8개 
    public GameObject grassObj;
    public List<GameObject> grassObjs;
    float grassTimer;
    public float grassCycle = 10f;
    public int maxGrass = 8;

    //웨이포인트
    public Transform[] wayPoint;
    public bool[] isOccupied;
    int wayPointIndex;

    //Tap
    Touch touch;

    //public LevelManager levelManager;

    //EXP
    public int currentExp;
    public int totalExp;

    //생명력
    public int treeLevel;
    public int vitality; //생명력
    public Text vitalityNum;
    //생명력 문자.
    public GameObject vitalityTxtObj;

    //사과
    float appleTimer;
    public float appleCycle; //과일 열리는 주기
    public Transform tree;
    public Transform[] appleSpawnPos;
    public bool[] hasApple;
    public GameObject appleObj;
    public GameObject goldAppleObj;
    public Dictionary<int, GameObject> activeApples;
    public int maxApples;

    //바나나
    public BananaManager bananaManager;
    float bananaTimer;
    public float bananaCycle; //과일 열리는 주기
    public GameObject bananaObj;
    public Transform[] bananaSpawnPos;
    public bool[] hasBanana;
    public Dictionary<int, GameObject> activeBananas;
    public int maxBananas;

    public int touchPoint;
    public int upgradePoint;

    //처음에 사과 5개 열리게.
    public bool isFirst;

    //도감.
    //버튼.
    public GameObject Buttons;
    public GameObject AllButton;
    public GameObject OwnedButton;
    public GameObject NotOwnedButton;
    public GameObject FavoriteButton;

    public GameObject firstSelected;
    public bool isFavoriteWin;

    public Dictionary<string, Animal> animals; //동물마다의 특유값 저장하는 곳. 전체동물 정보.
    public List<string> specialAnimal; //스페셜 동물 이름만. for skill3.
    Dictionary<int, Animal> currentList; //도감 버튼마다 변화하는 리스트들.
    public Dictionary<string, List<Animal>> TotalAnimals; //소유한 동물.
    public Dictionary<string, List<GameObject>> activeAnimals; //게임상 동물.
    public GameObject contents;
    public GameObject illustratedBookWin;
    int totalNum;

    //상세자료.
    public DetailInfoManager detailManger;

    //트리 창.
    public GameObject treeWin;
    public TreeWinManager treeWinManager;//스킬 쿨타임 관리.
    public GameObject treeButton;
    public GameObject settingWin;

    bool openTreeWin;
    bool isClosedButtons;

    //카메라.
    public Text CameraModeTxt;
    public Sprite swipeImg;
    public Sprite rotateImg;
    private IEnumerator coroutine;

    bool isNewStart;

    //설정창.
    public GameObject alertWin;
    //광고창.
    public GameObject adWin;
    bool isReadyAd;
    public bool isReceiveReward;
    //튜토리얼창.
    public TutorialManager tutorialManager;

    // Start is called before the first frame update
    void Start()
    {
        boxCycle = 2*60f;
        //boxCycle = 15 * 60f;

        animals = new Dictionary<string, Animal>();
        currentList = new Dictionary<int, Animal>();
        TotalAnimals = new Dictionary<string, List<Animal>>();
        activeAnimals = new Dictionary<string, List<GameObject>>();

        activeObjs = new List<GameObject>();
        activeSpecialObjs = new List<GameObject>();
        seedObjs = new List<GameObject>();
        maxAnimal = 20;

        vitality = 100;
        treeLevel = 1;
        touchPoint = 10;

        //동물 정보 저장.
        for (int i = 0; i < animalsInfo.Length; i++)
        {
            animalsInfo[i].isLock = true;
            animalsInfo[i].owned = false;
            animalsInfo[i].favorite = false;

            animalsInfo[i].totalNum = 0;
            animalsInfo[i].activeNum = 0;

            animals.Add(animalsInfo[i].name, animalsInfo[i]);
            if (animalsInfo[i].isSpecial) specialAnimal.Add(animalsInfo[i].name);

            totalNum++;
        }

        animals = animals.OrderBy(x => x.Value.level).ToDictionary(x => x.Key, x => x.Value);

        isFirst = true;
        activeApples = new Dictionary<int, GameObject>();
        maxApples = 5;
        appleCycle = 15f;

        activeBananas = new Dictionary<int, GameObject>();
        maxBananas = 3;
        bananaCycle = 15f;

        LoadPlayer();

        if (isFirst)
        {
            //상자 하나 생성.
            int ranBoxIndex = Random.Range(0, boxSpawnPoint.Length);

            currentBox = Instantiate(boxObj, boxSpawnPoint[ranBoxIndex].position, Quaternion.identity);

            var script = currentBox.GetComponent<AdBubble>();
            script.manager = this;

            var addScript = currentBox.GetComponent<TrackObject>();
            indicatorManager.Add(addScript);

            hasBox[ranBoxIndex] = true;
            isBoxExist = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //테스트용.
        vitalityNum.text = $"{vitality}";

        //처음에 사과 5개.
        if (isFirst)
        {
            while (activeApples.Count < maxApples)
            {
                int randomIndex = Random.Range(0, hasApple.Length);
                if (!hasApple[randomIndex])
                {
                    hasApple[randomIndex] = true;
                    var apple = Instantiate(appleObj, appleSpawnPos[randomIndex].position, Quaternion.identity);
                    var script = apple.GetComponent<Apple>();
                    script.key = randomIndex;
                    activeApples.Add(randomIndex, apple);
                }
            }

            isFirst = false;
        }

        //자료 저장.
        if(!isNewStart) SavePlayer();

        if (isReceiveReward) ReceiveReward();

        //상자.
        if(!isBoxExist) boxTimer += Time.deltaTime;
        if (boxTimer > boxCycle)
        {
            if (!isBoxExist)
            {
                int ranBoxIndex = Random.Range(0, boxSpawnPoint.Length);

                currentBox = Instantiate(boxObj, boxSpawnPoint[ranBoxIndex].position, Quaternion.identity);

                var script = currentBox.GetComponent<AdBubble>();
                script.manager = this;

                var addScript = currentBox.GetComponent<TrackObject>();
                indicatorManager.Add(addScript);

                hasBox[ranBoxIndex] = true;
                isBoxExist = true;
                boxTimer = 0f;
            }
        }

        //씨앗.
        seedTimer += Time.deltaTime;
        if (seedTimer > seedCycle)
        {
            if (seedObjs.Count < maxSeeds)
            {
                seedTimer = 0f;

                var seed = Instantiate(seedObj, new Vector3(Random.Range(0, 30), 0.6f, Random.Range(0, 30)), Quaternion.identity);
                var script = seed.GetComponent<SeedController>();
                script.manager = this;
                script.name = "Seed";
                seedObjs.Add(seed);
            }
        }

        //풀.
        grassTimer += Time.deltaTime;
        if (grassTimer > grassCycle)
        {
            if (grassObjs.Count < maxGrass)
            {
                grassTimer = 0f;

                var grass = Instantiate(grassObj, new Vector3(Random.Range(0, 30), 0.6f, Random.Range(0, 30)), Quaternion.identity);
                var script = grass.GetComponent<SeedController>();
                script.manager = this;
                script.name = "Grass";
                grassObjs.Add(grass);
            }
        }

        appleTimer += Time.deltaTime;
        if (appleTimer > appleCycle)
        {
            if (activeApples.Count < maxApples)
            {
                while (true)
                {
                    int randomIndex = Random.Range(0, hasApple.Length);
                    if (!hasApple[randomIndex])
                    {
                        hasApple[randomIndex] = true;
                        var apple = Instantiate(appleObj, appleSpawnPos[randomIndex].position, Quaternion.identity);
                        var script = apple.GetComponent<Apple>();
                        script.key = randomIndex;
                        activeApples.Add(randomIndex, apple);
                        break;
                    }
                }

                appleTimer = 0f;
            }
        }
        
        bananaTimer += Time.deltaTime;
        if (bananaTimer > bananaCycle)
        {
            //Debug.Log($"{hasBanana.Length}");
            bananaManager.Create(this);
            bananaTimer = 0f;
        }

        //도감.
        if (firstSelected != null)
            EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    //도감 오픈.
    public void OnOpenBook()
    {
        if (isTreeWin) return;
        if (isSettingWin) return;
        soundManager.ClickUI();

        isAnimalWin = true;
        Buttons.SetActive(false);
        illustratedBookWin.SetActive(true);
        OnAllButton();
    }

    //도감 메뉴 버튼.
    public void OnAllButton()
    {
        soundManager.ClickUI();

        InitButton();
        firstSelected = AllButton;

        if (currentList != null) currentList.Clear();

        if (totalNum == contents.transform.childCount)
        {
            int i = 0;
            foreach (var element in animals)
            {
                var button = contents.transform.GetChild(i);
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<Text>().text = element.Value.name;

                if (animals[element.Key].isLock)
                    button.GetChild(0).GetComponent<Image>().sprite = element.Value.lockImg;
                else button.GetChild(0).GetComponent<Image>().sprite = element.Value.unlockImg;
                //button.GetComponentInChildren<Image>().sprite = element.Value.unlockImg;
                currentList.Add(i, element.Value);
                Debug.Log($"{i} : {element.Value}");
                i++;
            }
        }
        else if (animals.Count < contents.transform.childCount)
        {
            int i = 0;
            if (currentList != null) currentList.Clear();

            foreach (var element in animals)
            {
                var button = contents.transform.GetChild(i);
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<Text>().text = element.Value.name;
                button.GetChild(0).GetComponent<Image>().sprite = element.Value.unlockImg;
                //button.GetComponentInChildren<Image>().sprite = element.Value.unlockImg;
                currentList.Add(i, element.Value);
                i++;
            }

            for (int j = i; j < contents.transform.childCount; j++)
            {
                var button = contents.transform.GetChild(j);
                button.gameObject.SetActive(true);
            }
        }
    }

    public void OnOwnedButton()
    {
        soundManager.ClickUI();

        InitButton();
        if (currentList != null) currentList.Clear();

        firstSelected = OwnedButton;

        int i = 0;
        foreach (var element in animals)
        {
            if (element.Value.owned)
            {
                var button = contents.transform.GetChild(i);
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<Text>().text = element.Value.name;
                button.GetChild(0).GetComponent<Image>().sprite = element.Value.unlockImg;
                //button.GetComponentInChildren<Image>().sprite = element.Value.unlockImg;
                currentList.Add(i, element.Value);
                i++;
            }
        }
    }

    public void OnNotOwnedButton()
    {
        soundManager.ClickUI();

        InitButton();
        if (currentList != null) currentList.Clear();

        firstSelected = NotOwnedButton;

        int i = 0;
        foreach (var element in animals)
        {
            if (!element.Value.owned)
            {
                var button = contents.transform.GetChild(i);
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<Text>().text = element.Value.name;
                button.GetChild(0).GetComponent<Image>().sprite = element.Value.unlockImg;
                //button.GetComponentInChildren<Image>().sprite = element.Value.unlockImg;
                currentList.Add(i, element.Value);
                i++;
            }
        }
    }

    public void OnFavoriteButton()
    {
        soundManager.ClickUI();

        InitButton();
        isFavoriteWin = true;
        if (currentList != null) currentList.Clear();

        firstSelected = FavoriteButton;

        int i = 0;
        foreach (var element in animals)
        {
            if (element.Value.favorite)
            {
                var button = contents.transform.GetChild(i);
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<Text>().text = element.Value.name;
                button.GetChild(0).GetComponent<Image>().sprite = element.Value.unlockImg;
                //button.GetComponentInChildren<Image>().sprite = element.Value.unlockImg;
                currentList.Add(i, element.Value);
                i++;
            }
        }
    }

    void InitButton()
    {
        isFavoriteWin = false;
        for (int i = 0; i < contents.transform.childCount; i++)
        {
            var button = contents.transform.GetChild(i);
            button.gameObject.SetActive(false);
        }
    }

    public void CloseBook()
    {
        soundManager.ClickUI();

        isAnimalWin = false;
        firstSelected = null;
        illustratedBookWin.SetActive(false);
        Buttons.SetActive(true);
    }

    public void DetailInfo(int number)
    {
        soundManager.ClickUI();

        //Debug.Log($"{currentList.Count}");
        if (currentList.ContainsKey(number))
        {
            detailManger.Open(number, currentList);
        }
    }

    //트리창.
    public void OnTreeWin()
    {
        soundManager.ClickUI();

        isTreeWin = true;
        Buttons.SetActive(false);
        treeWin.SetActive(true);
        firstSelected = treeButton;
        var anim = treeWin.GetComponent<Animator>();
        anim.SetBool("isOpen", true);
    }

    public void CloseTreeWin()
    {
        soundManager.ClickUI();

        isTreeWin = false;
        var anim = treeWin.GetComponent<Animator>();
        anim.SetBool("isOpen", false);
        Buttons.SetActive(true);
        //treeWin.SetActive(false);
    }

    //카메라창.
    public void OnCameraMode()
    {
        if (isSettingWin) return;
        soundManager.ClickUI();

        cameraMode = !cameraMode;

        if (cameraMode)
        {
            var image = CameraWinButton.GetComponent<Image>();
            image.sprite = swipeImg;

            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoCameraModeTxt();
            CameraModeTxt.text = $"Rotate Mode";
            StartCoroutine(coroutine);
        }
        else
        {
            var image = CameraWinButton.GetComponent<Image>();
            image.sprite = rotateImg;

            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoCameraModeTxt();
            CameraModeTxt.text = $"Swipe Mode";
            StartCoroutine(coroutine);
        }
    }

    IEnumerator CoCameraModeTxt()
    {
        CameraModeTxt.enabled = true;
        CameraModeTxt.color = new Color(CameraModeTxt.color.r, CameraModeTxt.color.g, CameraModeTxt.color.b, 1);
        float progress = 0;
        float duration = 1f;
        float smoothness = 0.02f;
        float increment = smoothness / duration;

        while (CameraModeTxt.color.a > 0)
        {
            CameraModeTxt.color = Color.Lerp(Color.white, Color.clear, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        CameraModeTxt.enabled = false;
    }

    //설정창.
    public void OnSettingWin()
    {
        soundManager.ClickUI();

        isSettingWin = true;
        settingWin.SetActive(true);
    }

    public void CloseSettingWin()
    {
        soundManager.ClickUI();

        isSettingWin = false;
        settingWin.SetActive(false);
    }

    public void OnAlertWin()
    {
        soundManager.ClickUI();

        alertWin.SetActive(true);
    }

    public void OnResumeWin()
    {
        soundManager.ClickUI();

        alertWin.SetActive(false);
    }

    //메뉴창
    public void OnMenuButton()
    {
        soundManager.ClickUI();

        isClosedButtons = !isClosedButtons;

        if (isClosedButtons)
        {
            treeWinButton.SetActive(false);
            animalWinButton.SetActive(false);
            CameraWinButton.SetActive(false);
            SettingWinButton.SetActive(false);

            var image = MenuButton.GetComponent<Image>();
            image.sprite = menuImg;
        }
        else
        {
            treeWinButton.SetActive(true);
            animalWinButton.SetActive(true);
            CameraWinButton.SetActive(true);
            SettingWinButton.SetActive(true);

            var image = MenuButton.GetComponent<Image>();
            image.sprite = returnImg;
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data == null) return;

        treeLevel = data.treeLevel;
        vitality = data.vitality;

        for (int i = 0; i < data.hasBox.Length; i++)
        {
            if (data.hasBox[i])
            {
                GameObject Obj;
                Obj = Instantiate(boxObj, boxSpawnPoint[i].position, Quaternion.identity);

                var script = Obj.GetComponent<AdBubble>();
                script.manager = this;

                var addScript = Obj.GetComponent<TrackObject>();
                indicatorManager.Add(addScript);

                hasBox[i] = true;
                isBoxExist = true;
            }
        }
        boxTimer = data.boxTimer;

        int[] levelInfo = new int[4];

        for (int i = 0; i < data.animalNum.Length; i++)
        {
            animals[data.animalName[i]].totalNum = data.animalNum[i];
            animals[data.animalName[i]].isLock = data.isLock[i];
            animals[data.animalName[i]].owned = data.isOwned[i];
            animals[data.animalName[i]].favorite = data.isFavorite[i];
        }

        fishNum = data.fishNum;
        chickenNum = data.chickenNum;

        StartCoroutine(CoCreate(data));

        //사과 관리.
        isFirst = data.isFirst;
        for (int i = 0; i < data.hasApple.Length; i++)
        {
            if (!data.hasApple[i]) continue;

            hasApple[i] = true;
            var apple = Instantiate(appleObj, appleSpawnPos[i].position, Quaternion.identity);
            var script = apple.GetComponent<Apple>();
            script.key = i;
            activeApples.Add(i, apple);
        }
        //바나나관리.
        for (int i = 0; i < data.hasBanana.Length; i++)
        {
            if (!data.hasBanana[i]) continue;

            hasBanana[i] = true;
            var banana = Instantiate(bananaObj, bananaSpawnPos[i].position, Quaternion.identity);
            var script = banana.GetComponent<Apple>();
            script.key = i;
            activeBananas.Add(i, banana);
        }

        //스킬 쿨타임 관리.
        treeWinManager.skill1Timer = data.skill1Timer;
        treeWinManager.skill2Timer = data.skill2Timer;
        treeWinManager.skill3Timer = data.skill3Timer;
        treeWinManager.skill4Timer = data.skill4Timer;

        treeWinManager.isSkill1 = data.isSkill1;
        treeWinManager.isSkill2 = data.isSkill2;
        treeWinManager.isSkill3 = data.isSkill3;
        treeWinManager.isSkill4 = data.isSkill4;

        //튜토리얼 체크.
        tutorialManager.isTutorial = data.isTutorial;
    }


    IEnumerator CoCreate(PlayerData data)
    {
        int[] ranX = new int[data.animalLifeCycle.Length];
        int[] ranY = new int[data.animalLifeCycle.Length];

        for (int i = 0; i < data.animalLifeCycle.Length; i++)
        {
            Animal currentAnimal = animals[data.activeAnimalName[i]];
            GameObject prefab;
           
            ranX[i] = Random.Range(0, 30);
            ranY[i] = Random.Range(0, 30);
            if (currentAnimal.isFish) prefab = Instantiate(currentAnimal.prefab, detailManger.fishZone.transform.position, Quaternion.identity);
            else prefab = Instantiate(currentAnimal.prefab, new Vector3(ranX[i], 0f, ranY[i]), Quaternion.identity);

            if (currentAnimal.isFish) fishNum++;
            activeObjs.Add(prefab);
            var script = prefab.GetComponent<AnimalController>();
            script.currentAnimal = currentAnimal;
            script.satiety = data.animalSatiety[i];
            script.lifeCycle = data.animalLifeCycle[i];
            script.isSpecial = currentAnimal.isSpecial;
            script.manager = this;
           
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < data.specialAnimalLifeCycle.Length; i++)
        {
            Animal currentAnimal = animals[data.activeSpecialAnimalName[i]];
            
            GameObject prefab = Instantiate(currentAnimal.prefab, new Vector3(Random.Range(0, 30), 0f, Random.Range(0, 30)), Quaternion.identity);

            activeSpecialObjs.Add(prefab);
            var script = prefab.GetComponent<AnimalController>();
            script.currentAnimal = currentAnimal;
            script.satiety = script.currentAnimal.satiety;
            script.lifeCycle = data.specialAnimalLifeCycle[i];
            script.isSpecial = currentAnimal.isSpecial;
            script.manager = this;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void CreateSpecialAnimal(Vector3 pos, int index)
    {
        int randomNum = Random.Range(0, specialAnimal.Count);
        string animalName = specialAnimal[randomNum];

        GameObject prefab;
        if(index == 0 || index == 3) prefab = Instantiate(animals[animalName].prefab, pos, Quaternion.Euler(0f, 180f, 0f));
        else prefab = Instantiate(animals[animalName].prefab,pos, Quaternion.identity);

        Animal currentAnimal = animals[animalName];

        activeSpecialObjs.Add(prefab);
        var script = prefab.GetComponent<AnimalController>();
        script.currentAnimal = currentAnimal;
        script.satiety = currentAnimal.satiety;
        script.lifeCycle = currentAnimal.lifeCycle;
        script.isSpecial = currentAnimal.isSpecial;
        script.manager = this;
        script.playSpecialEffect = true;
    }

    public void ReStart()
    {
        soundManager.ClickUI();

        isNewStart = true;
        SaveSystem.NewStartPlayer();
    }

    //테스트용.
    public void ReceiveReward()
    {
        //Debug.Log("보상을 받았습니다.");
        if (currentBox == null) currentBox = GameObject.FindGameObjectWithTag("Box");

        var trackObject = currentBox.GetComponentInChildren<TrackObject>();
        indicatorManager.indicators.Remove(trackObject);

        Destroy(indicatorManager.prefabs[trackObject]);
        indicatorManager.prefabs.Remove(trackObject);

        int index = 0;
        for (int i = 0; i < hasBox.Length; i++)
        {
            if (!hasBox[i]) continue;
            index = i;
        }

        soundManager.appearSound.Play();
        var boxPos = new Vector3(currentBox.transform.position.x, 0, currentBox.transform.position.z);
        //var boxPos = new Vector3(Random.Range(0,10), 0, Random.Range(0, 10));
        CreateSpecialAnimal(boxPos, index);

        hasBox[index] = false;
        isBoxExist = false;

        Destroy(currentBox);
        isReceiveReward = false;
    }
}