//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;

//public class ScreenShot : MonoBehaviour
//{
//    void Start()
//    {
      
//    }

//    public void ClickScreenShot()
//    {
//        StartCoroutine(Screenshoot());
//    }

//    IEnumerator Screenshoot()
//    {
//        yield return new WaitForEndOfFrame();
//        Texture2D texture = new Texture2D(Screen.width / 2, Screen.height, TextureFormat.RGB24, false);

//        texture.ReadPixels(new Rect(Screen.width / 4, 0, Screen.width / 2, Screen.height), 0, 0);
//        texture.Apply();

//        string name = "Screenshot_ZooApp" + System.DateTime.Now.ToString();

//        NativeGallery.SaveImageToGallery(texture, "Myapp pictures", name);

//        Destroy(texture);
//    }
//}