using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using HoloLensKeyboard;

//namespace HoloLensKeyboard
//{
    public class Controller : MonoBehaviour
    {
        public GameObject manager;
        public GameObject myCreator;
        public GameObject blocker;
        public InputField myInputField;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void startClicked()
        {
            blocker.SetActive(false);
            manager.GetComponent<TextToFile>().storeFile("[MM-dd-yyyy HH:mm:ss:ms ]\r\n");
            manager.GetComponent<TextToFile>().storeFile("[" + DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss:") + DateTime.UtcNow.Millisecond.ToString() + "]       Start\r\n");
            myCreator.GetComponent<KeyboardCreator>().currentTheme = "keyboard";
            myCreator.GetComponent<KeyboardCreator>().Initialize();
        }

        public void submitClicked()
        {
            blocker.SetActive(true);
            myInputField.text = "";
            manager.GetComponent<TextToFile>().storeFile("[" + DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss:") + DateTime.UtcNow.Millisecond.ToString() + "]       Submit\r\n");
            myCreator.GetComponent<KeyboardCreator>().currentTheme = "default";
            myCreator.GetComponent<KeyboardCreator>().Initialize();
        }
    }
//}
