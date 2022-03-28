using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailInfoManager : MonoBehaviour
{
    //같은 동물 5마리 제한
    //수중생물 3마리 제한
    //필드에 최대 20마리

    //상세정보.
    public GameObject DetailInfoWin;
    public GameObject PopupWin;
    public Text nameTxt;
    public Text requirementTxt;
    public GameObject specialIcon;
    public GameObject specialTxt;
    public GameObject specialInfo;
    public Image animalImg;

    public Image favoriteButImg;
    public Sprite[] favoriteImgs;

    public Text TotalNumTxt;
    public Text ActiveNumTxt;

    public GameObject eatImgs;
    public GameObject eatenImgs;

    public Sprite seedImg;
    public Sprite grassImg;
    public Sprite appleImg;
    public Sprite bananaImg;

    //
    int currentIndex;
    Dictionary<int, Animal> currentList;
    Animal currentAnimal;

    bool isopened;
    bool isclosed;
    bool openFoodInfo;
    public GameObject arrowButton;
    public Animator foodInfoAnim;

    public Image CreateButton;
    public Text NeedHeartTxt;
    public Text CreatedTxt;
    private IEnumerator coroutine;

    public GameObject fishZone;

    public GameManager manager;

    private void Start()
    {
        isopened = true;
    }

    public void Open(int currentNum, Dictionary<int, Animal> list)
    {
        currentList = list;
        currentIndex = currentNum;
        DetailInfoWin.SetActive(true);

        if (!currentList.ContainsKey(currentIndex)) return;

        ChangeAnimal();
    }

    public void OnNext()
    {
        if (currentIndex + 1 >= currentList.Count) return;

        manager.soundManager.ClickUI();

        currentIndex += 1;

        ChangeAnimal();
    }

    public void OnPrevious()
    {
        if (currentIndex - 1 < 0) return;

        manager.soundManager.ClickUI();

        currentIndex -= 1;

        ChangeAnimal();
    }

    void ChangeAnimal()
    {
        currentList.TryGetValue(currentIndex, out currentAnimal);
        nameTxt.text = currentAnimal.name;
        animalImg.sprite = currentAnimal.unlockImg;

        if (currentAnimal.isSpecial)
        {
            specialIcon.SetActive(true);
            specialTxt.SetActive(true);
        }
        else
        {
            specialIcon.SetActive(false);
            specialTxt.SetActive(false);
        }

        //Favorite img
        if (manager.animals[currentAnimal.name].favorite)
        {
            favoriteButImg.sprite = favoriteImgs[1];
        }
        else favoriteButImg.sprite = favoriteImgs[0];
        //
        bool check = !manager.animals[currentAnimal.name].isSpecial 
            && manager.animals[currentAnimal.name].name != "Hen"
            && manager.animals[currentAnimal.name].name != "Cock";

        Color color;
        NeedHeartTxt.color = (manager.vitality >= currentAnimal.payVitality && check) ? Color.white : Color.red;
        NeedHeartTxt.text = $"♥ {currentAnimal.payVitality.ToString()}.00";

        ColorUtility.TryParseHtmlString("#46c090ff", out color);
        CreateButton.color = (manager.vitality >= currentAnimal.payVitality && check) ? color : Color.gray;

        initImg();
        int eatIndex = 0;
        for (int i = 0; i < currentAnimal.eat.Length; i++)
        {
            if (!currentAnimal.eat[i].Equals("Seed") &&
                !currentAnimal.eat[i].Equals("Grass") &&
                !currentAnimal.eat[i].Equals("Apple") &&
                !currentAnimal.eat[i].Equals("Banana") &&
                (!manager.animals.ContainsKey(currentAnimal.eat[i])
                && !currentAnimal.eat[i].Equals("Chicken")))
            {
                //var parentObj = eatImgs.transform.GetChild(eatIndex).gameObject;
                //parentObj.GetComponent<ButtonInfo>().name = null;
                continue;
            }

            //씨앗일때.
            if (currentAnimal.eat[i].Equals("Seed"))
            {
                var parentObj = eatImgs.transform.GetChild(eatIndex).gameObject;
                //parentObj.GetComponent<ButtonInfo>().name = "Seed";
                var imgObj = parentObj.transform.GetChild(0).gameObject;
                imgObj.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.35f, 0.4f);

                var image = imgObj.GetComponent<Image>();
                image.enabled = true;
                image.sprite = seedImg;
                //image.sprite = manager.animals[currentAnimal.eat[i]].bookImg;

            }
            else if (currentAnimal.eat[i].Equals("Grass"))
            {
                var parentObj = eatImgs.transform.GetChild(eatIndex).gameObject;
                //parentObj.GetComponent<ButtonInfo>().name = "Grass";
                var imgObj = parentObj.transform.GetChild(0).gameObject;
                imgObj.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.35f, 0.6f);

                var image = imgObj.GetComponent<Image>();
                image.enabled = true;
                image.sprite = grassImg;
                //image.sprite = manager.animals[currentAnimal.eat[i]].bookImg;
            }
            else if (currentAnimal.eat[i].Equals("Apple"))
            {
                var parentObj = eatImgs.transform.GetChild(eatIndex).gameObject;
                //parentObj.GetComponent<ButtonInfo>().name = "Grass";
                var imgObj = parentObj.transform.GetChild(0).gameObject;
                imgObj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                var image = imgObj.GetComponent<Image>();
                image.enabled = true;
                image.sprite = appleImg;
                //image.sprite = manager.animals[currentAnimal.eat[i]].bookImg;
            }
            else if (currentAnimal.eat[i].Equals("Banana"))
            {
                var parentObj = eatImgs.transform.GetChild(eatIndex).gameObject;
                //parentObj.GetComponent<ButtonInfo>().name = "Grass";
                var imgObj = parentObj.transform.GetChild(0).gameObject;
                imgObj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                var image = imgObj.GetComponent<Image>();
                image.enabled = true;
                image.sprite = bananaImg;
                //image.sprite = manager.animals[currentAnimal.eat[i]].bookImg;
            }
            else
            {
                var parentObj = eatImgs.transform.GetChild(eatIndex).gameObject;
                //parentObj.GetComponent<ButtonInfo>().name = manager.animals[currentAnimal.eat[i]].name;

                var imgObj = parentObj.transform.GetChild(0).gameObject;
                imgObj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

                var image = imgObj.GetComponent<Image>();
                image.enabled = true;

                if (currentAnimal.eat[i].Equals("Chicken")) image.sprite = manager.animals["Cock"].bookImg;
                else image.sprite = manager.animals[currentAnimal.eat[i]].bookImg;
                image.type = Image.Type.Filled;
                image.fillMethod = Image.FillMethod.Horizontal;
                image.fillAmount = 1f;
            }
            eatIndex++;
        }

        int eatenIndex = 0;
        for (int i = 0; i < currentAnimal.eaten.Length; i++)
        {
            if (!manager.animals.ContainsKey(currentAnimal.eaten[i])) continue;

            var parentObj = eatenImgs.transform.GetChild(eatenIndex).gameObject;
            //parentObj.GetComponent<ButtonInfo>().name = manager.animals[currentAnimal.eat[i]].name;
            var imgObj = parentObj.transform.GetChild(0).gameObject;
            imgObj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            var image = imgObj.GetComponent<Image>();
            image.enabled = true;
            image.sprite = manager.animals[currentAnimal.eaten[i]].bookImg;
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Horizontal;
            image.fillAmount = 1f;
            eatenIndex++;
        }
    }

    void initImg()
    {
        for (int i = 0; i < 3; i++)
        {
            var parentObj = eatImgs.transform.GetChild(i).gameObject;
            var imgObj = parentObj.transform.GetChild(0).gameObject;
            imgObj.GetComponent<Image>().sprite = null;
            imgObj.GetComponent<Image>().enabled = false;

            parentObj = eatenImgs.transform.GetChild(i).gameObject;
            imgObj = parentObj.transform.GetChild(0).gameObject;
            imgObj.GetComponent<Image>().sprite = null;
            imgObj.GetComponent<Image>().enabled = false;
        }
    }

    public void OnClose()
    {
        manager.soundManager.ClickUI();

        DetailInfoWin.SetActive(false);

        if (manager.isFavoriteWin)
        {

            manager.OnFavoriteButton();
        }
    }

    public void OnFavorite()
    {
        //버튼 초기화.
        if (currentIndex > currentList.Count) return;

        manager.soundManager.ClickUI();

        manager.animals[currentAnimal.name].favorite
            = !manager.animals[currentAnimal.name].favorite;

        //Favorite img
        if (manager.animals[currentAnimal.name].favorite)
        {
            favoriteButImg.sprite = favoriteImgs[1];
        }
        else favoriteButImg.sprite = favoriteImgs[0];
    }

    public void OnStore()
    {
      
    }

    public void OnCreate()
    {
        manager.soundManager.ClickUI();

        if (manager.activeObjs.Count >= manager.maxAnimal)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoWarnTxt();
            CreatedTxt.text = $"You can only creat 20 in total.";
            CreatedTxt.fontSize = 15;
            StartCoroutine(coroutine);

            return;
        }
        if (currentAnimal.isLock)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoWarnTxt();
            CreatedTxt.text = $"This animal is locked.";
            CreatedTxt.fontSize = 15;
            StartCoroutine(coroutine);

            return;
        }
        if (manager.animals[currentAnimal.name].totalNum >= 5)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoWarnTxt();
            CreatedTxt.text = $"You can only create 5 of the same animal.";
            CreatedTxt.fontSize = 15;
            StartCoroutine(coroutine);

            return;
        }
        if ((manager.isOccupied[10] && manager.isOccupied[11] && manager.isOccupied[12] && currentAnimal.isFish)
            || manager.fishNum == 3 && currentAnimal.isFish)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoWarnTxt();
            CreatedTxt.text = $"You can only create 3 fish.";
            CreatedTxt.fontSize = 15;
            StartCoroutine(coroutine);

            return;
        }

        if(coroutine!=null)
            StopCoroutine(coroutine);
        coroutine = CoCreatedTxt();
        CreatedTxt.text = $"{currentAnimal.name} was created.";
        CreatedTxt.fontSize = 25;
        StartCoroutine(coroutine);

        manager.animals[currentAnimal.name].totalNum++;
        manager.animals[currentAnimal.name].activeNum++;
        manager.animals[currentAnimal.name].owned = true;

        if (currentAnimal.isFish) manager.fishNum++;

        GameObject prefab;
        if (currentAnimal.isFish) prefab = Instantiate(currentAnimal.prefab, fishZone.transform.position, Quaternion.identity);
        else prefab = Instantiate(currentAnimal.prefab, new Vector3(Random.Range(0, 10), 0f, Random.Range(0, 10)), Quaternion.identity);
        
        manager.activeObjs.Add(prefab);
        var script = prefab.GetComponent<AnimalController>();
        script.currentAnimal = currentAnimal;
        script.satiety = currentAnimal.satiety;
        script.lifeCycle = currentAnimal.lifeCycle;
        script.isSpecial = currentAnimal.isSpecial;
        script.manager = manager;
       
        manager.vitality -= currentAnimal.payVitality;
        foreach (var element in manager.animals)
        {
            if (!element.Value.isSpecial && element.Value.name != "Hen" && element.Value.name != "Cock")
            {
                if (manager.animals[element.Key].payVitality <= manager.vitality) manager.animals[element.Key].isLock = false;
                else manager.animals[element.Key].isLock = true;
            }
        }
        //if (manager.vitality < 0) manager.vitality = 0;

        //버튼 체인지.
        Color color;
        NeedHeartTxt.color = (manager.vitality >= currentAnimal.payVitality) ? Color.white : Color.red;
        NeedHeartTxt.text = $"♥ {currentAnimal.payVitality.ToString()}.00";

        ColorUtility.TryParseHtmlString("#46c090ff", out color);
        CreateButton.color = (manager.vitality >= currentAnimal.payVitality) ? color : Color.gray;
    
        
    }

    IEnumerator CoCreatedTxt()
    {
        CreatedTxt.enabled = true;
        CreatedTxt.color= new Color(CreatedTxt.color.r, CreatedTxt.color.g, CreatedTxt.color.b, 1);
        float progress = 0;
        float duration = 0.5f;
        float smoothness = 0.02f;
        float increment = smoothness / duration;

        while (CreatedTxt.color.a > 0)
        {
            CreatedTxt.color = Color.Lerp(Color.white, Color.clear, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        CreatedTxt.enabled = false;
    }

    IEnumerator CoWarnTxt()
    {
        CreatedTxt.enabled = true;
        CreatedTxt.color = new Color(CreatedTxt.color.r, CreatedTxt.color.g, CreatedTxt.color.b, 1);
        float progress = 0;
        float duration = 1f;
        float smoothness = 0.02f;
        float increment = smoothness / duration;

        while (CreatedTxt.color.a > 0)
        {
            CreatedTxt.color = Color.Lerp(Color.red, Color.clear, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        CreatedTxt.enabled = false;
    }

    public void OnFoodInfo()
    {
        manager.soundManager.ClickUI();

        openFoodInfo = !openFoodInfo;

        if (openFoodInfo)
        {
            PopupWin.SetActive(false);
            arrowButton.transform.rotation = Quaternion.Euler(0, 0, 180f);
        }
        else
        {
            PopupWin.SetActive(true);
            arrowButton.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnSpecialInfo()
    {
        manager.soundManager.ClickUI();

        specialInfo.SetActive(true);
    }

    public void CloseSpecialInfo()
    {
        manager.soundManager.ClickUI();

        specialInfo.SetActive(false);
    }
}
