using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorProperties : MonoBehaviour
{

    public bool pause;

    public float interpValue;

    public float cachedTime;

    public float speed = .01f;

    private static BehaviorProperties instance;

    public static BehaviorProperties Instance
    {
        get
        {
            if (instance == null) Debug.LogError("bad");

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetPause(bool value)
    {
        
        pause = value;

       
       
    }

    public float GlobalTime()
    {
        if (!pause)
        {
            interpValue += speed * Time.deltaTime;
        }   
        return interpValue;
    }

    /*
    public float GlobalTime(float valueToIncrease)
    {
        
        if (!pause)
        {
            //valueToIncrease +=  Time.time;
            //interpValue = valueToIncrease;
            interpValue += speed * Time.deltaTime;
        }
        if(pause)
        {
            cachedTime += speed * Time.deltaTime;
        }
       

        return interpValue;
    }
    */


}
