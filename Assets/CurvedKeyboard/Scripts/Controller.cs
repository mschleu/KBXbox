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

            //PREVIOUS TIMESTAMP///////////////////////////////////////////////////////////////////////
            //manager.GetComponent<TextToFile>().storeFile("[MM-dd-yyyy HH:mm:ss:ms ]\r\n");
            //manager.GetComponent<TextToFile>().storeFile("[" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss:") + DateTime.Now.Millisecond.ToString() + "]       --Start--\r\n");
            ///////////////////////////////////////////////////////////////////////////////////////////

            //NEW TIMESTAMP FORMAT/////////////////////////////////////////////////////////////////////
            manager.GetComponent<TextToFile>().storeFile("[epoch | since start | since last button]\r\n");
            manager.GetComponent<TextToFile>().storeFile("[" + manager.GetComponent<TimeStamp>().t + " | " + 0 + " | " + 0 + "]       --Start--\r\n");
            manager.GetComponent<TimeStamp>().startTime = Time.time;
            manager.GetComponent<TimeStamp>().buttonTime = Time.time;
            manager.GetComponent<TimeStamp>().started = true;
            ///////////////////////////////////////////////////////////////////////////////////////////

            myCreator.GetComponent<KeyboardCreator>().currentTheme = "keyboard";
            myCreator.GetComponent<KeyboardCreator>().Initialize();
        }

        public void submitClicked()
        {
            blocker.SetActive(true);
            myInputField.text = "";

            //PREVIOUS TIMESTAMP///////////////////////////////////////////////////////////////////////
            //manager.GetComponent<TextToFile>().storeFile("[" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss:") + DateTime.Now.Millisecond.ToString() + "]       --Submit--\r\n");
            ///////////////////////////////////////////////////////////////////////////////////////////

            //NEW TIMESTAMP////////////////////////////////////////////////////////////////////////////
            manager.GetComponent<TextToFile>().storeFile("[" + manager.GetComponent<TimeStamp>().t + " | " + manager.GetComponent<TimeStamp>().since_start + " | " + manager.GetComponent<TimeStamp>().since_button + "]       --Submit--\r\n\n");
            manager.GetComponent<TimeStamp>().started = false;
            ///////////////////////////////////////////////////////////////////////////////////////////

            myCreator.GetComponent<KeyboardCreator>().currentTheme = "default";
            myCreator.GetComponent<KeyboardCreator>().Initialize();
        }
    }
//}
