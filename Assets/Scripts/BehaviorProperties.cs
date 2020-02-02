using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorProperties : MonoBehaviour
{

    public bool pause;

    public float interpValue;

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



    public float GlobalTime(float valueToIncrease)
    {
        if (!pause)
        {
            valueToIncrease += Time.time;
            interpValue = valueToIncrease;
        }

        return interpValue;
    }

   
}
