﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public HealthManager health;

    private Text text;


    private void Awake()
    {
        text = gameObject.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //updates the canvas text on screen to show player health in percent form
        text.text = string.Format("Health: {0}%", Mathf.RoundToInt(health.OverallHealthPercent * 100));
    }


}