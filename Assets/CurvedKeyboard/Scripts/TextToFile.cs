using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class TextToFile : MonoBehaviour
{

    string file;

    void Start()
    {
        //KeyStrokeLog-yyyy-MM-dd-HH-mm-ss-ms
        file = Application.persistentDataPath + "/KeyStrokeLog-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + DateTime.Now.Millisecond.ToString() + ".txt";
    }

    public void storeFile(string mes)
    {
        try
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, mes);
            }
            else
            {
                File.AppendAllText(file, mes);
            }
        }

        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
