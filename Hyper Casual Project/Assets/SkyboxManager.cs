using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material skybox1;
    public Material skybox2;
    public Material skybox3;
    public Material skybox4;
    public GameObject rain;
    public bool isRain;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SkyBox1()
    {
        if (isRain)
        {
            isRain = false;
            rain.SetActive(false);
        }
        RenderSettings.skybox = skybox1;
    }

    public void SkyBox2()
    {
        if (isRain)
        {
            isRain = false;
            rain.SetActive(false);
        }
        RenderSettings.skybox = skybox2;
    }

    public void SkyBox3()
    {
        if (isRain)
        {
            isRain = false;
            rain.SetActive(false);
        }
        RenderSettings.skybox = skybox3;
    }

    public void SkyBox4()
    {
        isRain = true;
        rain.SetActive(true);
        RenderSettings.skybox = skybox4;
    }
}
