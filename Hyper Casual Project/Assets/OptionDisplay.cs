using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionDisplay : MonoBehaviour
{
    public GameManager manager;
    public GameObject optionWin;
    public GameObject alertWin;
    public GameObject adWin;
    public GameObject managementWin;
    public GameObject creditsWin;
    public GameObject howtoWin;

    public bool isOpen;
    public bool isAdWinOpen;
    public bool isMmWinOpen;
    public bool isCreditsWinOpen;
    public bool isHowtoWinOpen;

    public void OpenOptionWin()
    {
        manager.soundManager.ClickUI();
        optionWin.SetActive(true);
        isOpen = true;
    }

    public void CloseOptionWin()
    {
        manager.soundManager.ClickUI();
        optionWin.SetActive(false);
        isOpen = false;
    }

    public void OpenAlertWin()
    {
        manager.soundManager.ClickUI();
        alertWin.SetActive(true);
    }

    public void CloseAlertWin()
    {
        manager.soundManager.ClickUI();
        alertWin.SetActive(false);
    }

    public void OpenAdWin()
    {
        if (isAdWinOpen) return;
        manager.soundManager.ClickUI();
        adWin.SetActive(true);
        isAdWinOpen = true;
    }

    public void CloseAdWin()
    {
        manager.soundManager.ClickUI();
        adWin.SetActive(false);
        isAdWinOpen = false;
    }

    public void OpenManagementWin()
    {
        if (isMmWinOpen) return;
        manager.soundManager.ClickUI();
        managementWin.SetActive(true);
        isMmWinOpen = true;
    }

    public void CloseManagementWin()
    {
        manager.soundManager.ClickUI();
        managementWin.SetActive(false);
        isMmWinOpen = false;
    }

    public void OpenCreditsWin()
    {
        if (isCreditsWinOpen) return;
        manager.soundManager.ClickUI();
        creditsWin.SetActive(true);
        isCreditsWinOpen = true;
    }

    public void CloseCreditsWin()
    {
        manager.soundManager.ClickUI();
        creditsWin.SetActive(false);
        isCreditsWinOpen = false;
    }

    public void OpenHowtoWin()
    {
        if (isHowtoWinOpen) return;
        manager.soundManager.ClickUI();
        howtoWin.SetActive(true);
        isHowtoWinOpen = true;
    }

    public void CloseHowtoWin()
    {
        manager.soundManager.ClickUI();
        howtoWin.SetActive(false);
        isHowtoWinOpen = false;
    }
}
