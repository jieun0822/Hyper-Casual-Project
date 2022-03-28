using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookDisplay : MonoBehaviour
{
    public GameManager manager;
    public GameObject bookWin;

    public int level;
    public GameObject buttons;
    public GameObject closeArea;

    //버튼들.
    public GameObject menu_one;
    public GameObject menu_two;
    public GameObject menu_three;
    public GameObject menu_four;
    public GameObject menu_five;

    public Dictionary<int, Animal> animalList;

    //bool isFirst;
    public bool isOpen;
    public bool isDetailOpen;
    GameObject firstSelected;

    IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        animalList = new Dictionary<int, Animal>();
        level = 1;
        firstSelected = menu_one;

        //초기화.
        for (int i = 0; i < 8; i++)
        {
            var button = buttons.transform.GetChild(i).gameObject;
            button.SetActive(false);
        }

        int j = 0;
        foreach (var element in manager.animals)
        {
            if (level == 1 && !(element.Value.level == 0 || element.Value.level == 1)) continue;
            else if (level != 1 && element.Value.level != level) continue;
            else if (element.Value.name.Equals("Cock") || element.Value.name.Equals("Hen")) continue;

            var button = buttons.transform.GetChild(j).gameObject;
            button.SetActive(true);
            var script = button.GetComponent<ButtonId>();
          
            var imgObj = button.transform.GetChild(0).gameObject;
            var image = imgObj.GetComponent<Image>();

            if(element.Value.name.Equals("Cock") || element.Value.name.Equals("Hen"))
                image.sprite = manager.animals[element.Key].lockImg;

            else
                image.sprite = 
                    (manager.animals[element.Key].payVitality>manager.vitality) ? manager.animals[element.Key].lockImg : manager.animals[element.Key].unlockImg;

            var textObj = button.transform.GetChild(1).gameObject;
            var animalTxt = textObj.GetComponent<Text>();
            animalTxt.text = manager.animals[element.Key].name;

            animalList.Add(j, manager.animals[element.Key]);
            
            j++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firstSelected != null && isOpen)
            EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void OnClick(int index)
    {
        //manager.soundManager.ClickUI();

        level = index;

        if (index == 1) firstSelected = menu_one;
        else if (index == 2) firstSelected = menu_two;
        else if (index == 3) firstSelected = menu_three;
        else if (index == 4) firstSelected = menu_four;
        else if (index == 5) firstSelected = menu_five;

        //초기화.
        animalList.Clear();
        for (int i = 0; i < 8; i++)
        {
            var button = buttons.transform.GetChild(i).gameObject;
            button.SetActive(false);
        }

        int j = 0;
        foreach (var element in manager.animals)
        {
            if (level == 1 && !(element.Value.level == 0 || element.Value.level == 1)) continue;
            else if (level != 1 && element.Value.level != level) continue;
            else if (element.Value.name.Equals("Cock") || element.Value.name.Equals("Hen")) continue;

            Debug.Log($"name : {element.Value.name}");

            var button = buttons.transform.GetChild(j).gameObject;
            button.SetActive(true);
            var script = button.GetComponent<ButtonId>();

            var imgObj = button.transform.GetChild(0).gameObject;
            var image = imgObj.GetComponent<Image>();
            image.sprite =
                (manager.animals[element.Key].isLock) ? manager.animals[element.Key].lockImg : manager.animals[element.Key].unlockImg;

            var textObj = button.transform.GetChild(1).gameObject;
            var animalTxt = textObj.GetComponent<Text>();
            animalTxt.text = manager.animals[element.Key].name;

            animalList.Add(j, manager.animals[element.Key]);

            j++;
        }
    }

    public void TabFootPrint()
    {
        manager.soundManager.ClickUI();

        if (manager.tutorialManager.isTutorial)
        {
            manager.tutorialManager.CompleteStep1();
            //manager.tutorialManager.Step2Win.SetActive(true);
        }

        if (!isDetailOpen)
        {
            isOpen = !isOpen;
            if (isOpen) Open();
            else Close();
        }
        else
        {
            isDetailOpen = false;
        }
    }

    public void Open()
    {
        manager.soundManager.ClickUI();

        if (coroutine != null) StopCoroutine(coroutine);
        
        bookWin.SetActive(true);
        var anim = bookWin.GetComponent<Animator>();
        anim.SetBool("isOpen", true);

        closeArea.SetActive(true);
        //Invoke("ActiveClose", 1);
    }

    //void ActiveClose()
    //{
    //    closeArea.SetActive(true);
    //}

    public void Close()
    {
        manager.soundManager.ClickUI();
        closeArea.SetActive(false);

        var anim = bookWin.GetComponent<Animator>();
        anim.SetBool("isOpen", false);

        coroutine = CoClose();
        StartCoroutine(coroutine);
    }

    IEnumerator CoClose()
    {
        yield return new WaitForSeconds(1f);
        bookWin.SetActive(false);
    }
}
