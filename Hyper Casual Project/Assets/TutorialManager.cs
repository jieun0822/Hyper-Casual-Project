using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameManager manager;
    public GameObject Step1Win;
    //public GameObject Step2Win;

    public Button footPrintButton;
    public Button menuButton;
    public bool isTutorial;
    //public bool isStep2;

    // Start is called before the first frame update
    void Start()
    {
        isTutorial = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorial) Step1Win.SetActive(true);
    }

    public void CompleteStep1()
    {
        isTutorial = false;
        menuButton.interactable = true;

        Step1Win.SetActive(false);
        //isStep2 = true;
    }

    //public void CompleteStep2()
    //{
    //    isStep2 = false;
    //    Step2Win.SetActive(false);
    //}
}
