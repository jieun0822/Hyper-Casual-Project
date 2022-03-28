using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Management : MonoBehaviour
{
    public GameManager manager;
    public Image totalImg;
    public Text totalTxt;

    public Dropdown menu;
    public GameObject images;

    string selectedOption;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        //var dropdown = menu.GetComponent<Dropdown>();
        //menu.onValueChanged.AddListener(delegate { DropdownItemSelected(menu); });
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.optionDisplay.isMmWinOpen)
        {
            int index = menu.value;
            selectedOption = menu.options[index].text;

            //리스트 생성.
            menu.options.Clear();

            //사라진것 삭제.
            foreach (var element in menu.options)
            {
                bool isExist = false;
                foreach (var animal in manager.activeObjs)
                {
                    var script = animal.GetComponent<AnimalController>();
                    string name = script.currentAnimal.name;
                    if (name.Equals(element.text)) isExist = true;
                }

                if (!isExist)
                {
                    menu.options.Remove(element);
                }
            }

            Dropdown.OptionData allOption = new Dropdown.OptionData();
            allOption.text = "All";
            menu.options.Add(allOption);

            //새로 추가된것 추가.
            foreach (var animal in manager.activeObjs)
            {
                var script = animal.GetComponent<AnimalController>();
                string name = script.currentAnimal.name;

                bool isExist = false;
                foreach (var element in menu.options)
                {
                    if (name.Equals(element.text)) isExist = true;
                }

                if (!isExist)
                {
                    Dropdown.OptionData option = new Dropdown.OptionData();
                    option.text = name;
                    menu.options.Add(option);
                }
            }

            menu.RefreshShownValue();

            //선택한게 있다면.
            int j = 0;
            bool isRemain = false;
            foreach (var element in menu.options)
            {
                if (selectedOption.Equals(element.text))
                {
                    menu.value = j;
                    index = j;
                    isRemain = true;
                    break;
                }
                j++;
            }

            if (!isRemain) //선택한게 없다면.
            {
                //if (menu.options.Count <= index)
                //{
                menu.value = 0;
                index = 0;
                //}

                //index = menu.value;
                //var changedOption = menu.options[index].text;
                //if (!selectedOption.Equals(changedOption))
                //{
                //    menu.value = 0;
                //    index = 0;
                //}
            }

            string optionName = menu.options[index].text;

            //초기화.
            for (int i = 0; i < 20; i++)
            {
                var animalList = images.transform.GetChild(i).gameObject;
                animalList.SetActive(false);
            }

            if (optionName.Equals("All"))
            {
                int i = 0;
                foreach (var element in manager.activeObjs)
                {
                    var animalList = images.transform.GetChild(i).gameObject;
                    animalList.SetActive(true);

                    var script = element.GetComponent<AnimalController>();
                    var animalImg = animalList.transform.GetChild(0).gameObject;
                    var animalVitality = animalList.transform.GetChild(1).gameObject;
                    var animalSatiety = animalList.transform.GetChild(2).gameObject;

                    var image = animalImg.GetComponent<Image>();
                    image.sprite = script.currentAnimal.unlockImg;

                    var vitalitySlide = animalVitality.GetComponent<Slider>();
                    float totalLifeCycle = script.currentAnimal.lifeCycle;
                    vitalitySlide.value = script.lifeCycle / totalLifeCycle;


                    var satietySlide = animalSatiety.GetComponent<Slider>();
                    float totalSatiety = script.currentAnimal.satiety;
                    satietySlide.value = script.satiety / totalSatiety;

                    i++;
                }
            }
            else
            {
                int i = 0;
                foreach (var element in manager.activeObjs)
                {
                    var script = element.GetComponent<AnimalController>();
                    if (!script.currentAnimal.name.Equals(optionName)) continue;

                    var animalList = images.transform.GetChild(i).gameObject;
                    animalList.SetActive(true);

                    //Debug.Log($"{name} : {script.lifeCycle}");

                    var animalImg = animalList.transform.GetChild(0).gameObject;
                    var animalVitality = animalList.transform.GetChild(1).gameObject;
                    var animalSatiety = animalList.transform.GetChild(2).gameObject;

                    var image = animalImg.GetComponent<Image>();
                    image.sprite = script.currentAnimal.unlockImg;

                    var vitalitySlide = animalVitality.GetComponent<Slider>();
                    float totalLifeCycle = script.currentAnimal.lifeCycle;
                    vitalitySlide.value = script.lifeCycle / totalLifeCycle;

                    var satietySlide = animalSatiety.GetComponent<Slider>();
                    float totalSatiety = script.currentAnimal.satiety;
                    satietySlide.value = script.satiety / totalSatiety;
                    i++;
                }
            }

            totalImg.fillAmount = (float)manager.activeObjs.Count / 20;
            totalTxt.text = $"{manager.activeObjs.Count} / 20";
        }
    }

    //void DropdownItemSelected(Dropdown dropdown)
    //{
    //    int index = dropdown.value;
    //    string name = dropdown.options[index].text;

        

    //    manager.activeObjs.Sort((x, y) => string.Compare(x.name, y.name));

    //    //초기화.
    //    for (int i = 0; i < 5; i++)
    //    {
    //        var animalList = images.transform.GetChild(i).gameObject;
    //        animalList.SetActive(false);
    //    }

    //    if (name.Equals("All"))
    //    {
    //        int i = 0;
    //        foreach (var element in manager.activeObjs)
    //        {
    //            var animalList = images.transform.GetChild(i).gameObject;
    //            animalList.SetActive(true);

    //            var script = element.GetComponent<AnimalController>();
    //            var animalImg = animalList.transform.GetChild(0).gameObject;
    //            var animalVitality = animalList.transform.GetChild(1).gameObject;
    //            var animalSatiety = animalList.transform.GetChild(2).gameObject;

    //            var image = animalImg.GetComponent<Image>();
    //            image.sprite = script.currentAnimal.unlockImg;

    //            var vitalitySlide = animalVitality.GetComponent<Slider>();
    //            float totalLifeCycle = script.currentAnimal.lifeCycle;
    //            vitalitySlide.value = script.lifeCycle/totalLifeCycle;

    //            var satietySlide = animalSatiety.GetComponent<Slider>();
    //            float totalSatiety = script.currentAnimal.satiety;
    //            satietySlide.value = script.satiety/totalSatiety;
    //            i++;
    //        }
    //    }
    //    else
    //    {
    //        int i = 0;
    //        foreach (var element in manager.activeObjs)
    //        {
    //            var script = element.GetComponent<AnimalController>();
    //            if (!script.currentAnimal.name.Equals(name)) continue;

    //            var animalList = images.transform.GetChild(i).gameObject;
    //            animalList.SetActive(true);

    //            Debug.Log($"{name} : {script.lifeCycle}");

    //            var animalImg = animalList.transform.GetChild(0).gameObject;
    //            var animalVitality = animalList.transform.GetChild(1).gameObject;
    //            var animalSatiety = animalList.transform.GetChild(2).gameObject;

    //            var image = animalImg.GetComponent<Image>();
    //            image.sprite = script.currentAnimal.unlockImg;

    //            var vitalitySlide = animalVitality.GetComponent<Slider>();
    //            float totalLifeCycle = script.currentAnimal.lifeCycle;
    //            vitalitySlide.value = script.lifeCycle / totalLifeCycle;

    //            var satietySlide = animalSatiety.GetComponent<Slider>();
    //            float totalSatiety = script.currentAnimal.satiety;
    //            satietySlide.value = script.satiety / totalSatiety;
    //            i++;
    //        }
    //    }
    //}
}
