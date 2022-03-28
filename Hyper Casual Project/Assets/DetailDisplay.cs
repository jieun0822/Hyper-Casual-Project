using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailDisplay : MonoBehaviour
{
    public GameManager manager;
    public BookDisplay bookDisplay;

    public GameObject DetailWin;

    public Image animalImg;
    public Text animalName;
    public Image favorImg;

    public Text animalVitality;
    public Text animalSatiety;
    public Text animalLifecycle;

    public GameObject foodGroup;
    public GameObject feedGroup;

    public Sprite grayStar;
    public Sprite yellowStar;

    public Text CreatedTxt;
    private IEnumerator coroutine;
    Animal currentAnimal;

    public void OnButton(int index)
    {
        manager.soundManager.ClickUI();

        DetailWin.SetActive(true);
        bookDisplay.isDetailOpen = true;

        currentAnimal = bookDisplay.animalList[index];

        animalImg.sprite = currentAnimal.unlockImg;
        animalName.text = currentAnimal.name;
        if (currentAnimal.favorite) favorImg.sprite = yellowStar;
        else favorImg.sprite = grayStar;

        if(currentAnimal.payVitality > 999) animalVitality.text = $"{currentAnimal.payVitality}";
        else animalVitality.text = $"{currentAnimal.payVitality}.00";

        animalSatiety.text = $"{currentAnimal.satiety}";
        animalLifecycle.text = (currentAnimal.lifeCycle < 10) ?
            $"0{currentAnimal.lifeCycle}:00" :
             $"{currentAnimal.lifeCycle}:00";

        //ÃÊ±âÈ­.
        for (int j = 0; j < 3; j++)
        {
            var list = foodGroup.transform.GetChild(j).gameObject;
            list.SetActive(false);

            list = feedGroup.transform.GetChild(j).gameObject;
            list.SetActive(false);
        }

        //Food.
        int i = 0;
        foreach (var food in currentAnimal.eat)
        {
            var list = foodGroup.transform.GetChild(i).gameObject;
            list.SetActive(true);

            var foodAnimal = list.transform.GetChild(0).gameObject;
            var foodAnimalImg = foodAnimal.GetComponent<Image>();

            if (food.Equals("Seed")) foodAnimalImg.sprite = manager.detailManger.seedImg;
            else if (food.Equals("Grass")) foodAnimalImg.sprite = manager.detailManger.grassImg;
            else if (food.Equals("Apple")) foodAnimalImg.sprite = manager.detailManger.appleImg;
            else if (food.Equals("Banana")) foodAnimalImg.sprite = manager.detailManger.bananaImg;
            else if (food.Equals("Chicken")) foodAnimalImg.sprite = manager.animals["Cock"].largeImg;
            else foodAnimalImg.sprite = manager.animals[food].largeImg;

            foodAnimal = list.transform.GetChild(1).gameObject;
            var foodAnimalTxt = foodAnimal.GetComponent<Text>();

            if (food.Equals("Seed")) foodAnimalTxt.text = $"+10";
            else if (food.Equals("Grass")) foodAnimalTxt.text = $"+10";
            else if (food.Equals("Apple")) foodAnimalTxt.text = $"+22";
            else if (food.Equals("Banana")) foodAnimalTxt.text = $"+55";
            else foodAnimalTxt.text = $"+{currentAnimal.getVitality_feed}";

            i++;
        }

        //Feed.
        i = 0;
        foreach (var feed in currentAnimal.eaten)
        {
            if (!manager.animals.ContainsKey(feed)) continue;

            var list = feedGroup.transform.GetChild(i).gameObject;
            list.SetActive(true);

            var feedAnimal = list.transform.GetChild(0).gameObject;
            var feedAnimalImg = feedAnimal.GetComponent<Image>();

            if (feed.Equals("Chicken")) feedAnimalImg.sprite = manager.animals["Cock"].largeImg;
            else feedAnimalImg.sprite = manager.animals[feed].largeImg;
            
            i++;
        }
    }

    public void OnFavorite()
    {
        manager.soundManager.ClickUI();

        manager.animals[currentAnimal.name].favorite
            = !manager.animals[currentAnimal.name].favorite;

        //Favorite img
        if (currentAnimal.favorite) favorImg.sprite = yellowStar;
        else favorImg.sprite = grayStar;
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

        if (/*(manager.isOccupied[10] && manager.isOccupied[11] && manager.isOccupied[12] && currentAnimal.isFish)
            ||*/manager.fishNum == 3 && currentAnimal.isFish)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoWarnTxt();
            CreatedTxt.text = $"You can only create 3 fish.";
            CreatedTxt.fontSize = 15;
            StartCoroutine(coroutine);

            return;
        }

        if (manager.chickenNum == 5 && currentAnimal.name.Equals("Chick"))
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CoWarnTxt();
            CreatedTxt.text = $"You can only create 5 of the same animal.";
            CreatedTxt.fontSize = 15;
            StartCoroutine(coroutine);

            return;
        }

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = CoCreatedTxt();
        CreatedTxt.text = $"{currentAnimal.name} was created.";
        CreatedTxt.fontSize = 25;
        StartCoroutine(coroutine);

        manager.animals[currentAnimal.name].totalNum++;
        manager.animals[currentAnimal.name].activeNum++;
        manager.animals[currentAnimal.name].owned = true;

        if (currentAnimal.isFish) manager.fishNum++;
        else if (currentAnimal.name.Equals("Chick")) manager.chickenNum++;

        GameObject prefab;
        if (currentAnimal.isFish) prefab = Instantiate(currentAnimal.prefab, manager.detailManger.fishZone.transform.position, Quaternion.identity);
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
    }

    IEnumerator CoCreatedTxt()
    {
        CreatedTxt.enabled = true;
        CreatedTxt.color = new Color(CreatedTxt.color.r, CreatedTxt.color.g, CreatedTxt.color.b, 1);
        float progress = 0;
        float duration = 1f;
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

    public void Close()
    {
        manager.soundManager.ClickUI();

        if (!bookDisplay.isDetailOpen)
            DetailWin.SetActive(false);
    }
}
