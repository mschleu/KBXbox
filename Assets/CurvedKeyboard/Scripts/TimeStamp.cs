using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeStamp : MonoBehaviour {

    public DateTime epoch;
    public int t;

    public float startTime;
    public float buttonTime;
    public float currentTime;

    public float since_start;
    public float since_button;

    public bool started = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        t = (int)(DateTime.UtcNow - epoch).TotalSeconds;

        currentTime = Time.time;
        if(started)
        {
            since_start = currentTime - startTime;
            since_button = currentTime - buttonTime;
        }
    }
}
