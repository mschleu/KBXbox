﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System;
using HoloLensKeyboard;

//namespace HoloLensKeyboard
//{
    /// <summary>
    /// Responsible for connecting the Keyboard to a component that can display text.
    /// </summary>
    public class KeyboardOutput : MonoBehaviour
    {
        public GameObject manager;

        [Tooltip("The maximum amount of characters")]
        public int MaximumInput = 30;
        [Tooltip("The component responsible for displaying the text")]
        public InputField Input;

        private void Start()
        {
            Assert.IsNotNull(Input);
        }

        public void OnBack()
        {
            if (Input.text.Length > 0)
            {
                Input.text = Input.text.Remove(Input.text.Length - 1, 1);
            }
        }

        public void OnKey(string input)
        {
            if (Input.text.Length < MaximumInput)
            {
                Input.text = Input.text + input;
                manager.GetComponent<TextToFile>().storeFile("[" + DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss:") + DateTime.UtcNow.Millisecond.ToString() + "]       " + Input.text + "\r\n");
            }
        }
    }
//}


