using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class ScreenShootGrabber : MonoBehaviour
{
    public RawImage imageFrames;
    public string imagepath = "screenshots";
    public string[] framesLocation = new string[12];
    int indexFrame = 0;
    int texWidth = 1280, textHeight = 720;
    // Start is called before the first frame update
    void Start()
    {
        imagepath = UnityEngine.Application.persistentDataPath + "/" + imagepath;
        
        System.IO.DirectoryInfo di = new DirectoryInfo(imagepath);
        if (!Directory.Exists(imagepath)){
            Directory.CreateDirectory(imagepath);
        }   
        else {
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

    }

    void grabScreenShot() {
        string currentFrame = imagepath+ "/" + indexFrame + "pic" + ".png";
        ScreenCapture.CaptureScreenshot(currentFrame);
        framesLocation[indexFrame] = currentFrame;
        indexFrame++;
    }

    void showNewImage(int i) {
        Debug.ClearDeveloperConsole();
        Texture2D thisTexture = new Texture2D(texWidth, textHeight); 
        string fileName = framesLocation[i];
        Debug.Log("looking at image: " + fileName);
        try
        {
            byte[] bytes = File.ReadAllBytes(fileName);
            thisTexture.LoadImage(bytes);
            thisTexture.name = fileName;
            imageFrames.texture = thisTexture;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        Debug.Log("loadning image: " + fileName);
    }

    
}
