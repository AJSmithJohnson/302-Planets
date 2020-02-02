using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorProperties 
{
    public bool pause;

    public float interpValue;

    // Update is called once per frame
    public static float SpaceInterpManager()
    {
        if(!pause)
        {
            interpValue += Time.time;
        }
        
        return  interpValue;
    }
}
