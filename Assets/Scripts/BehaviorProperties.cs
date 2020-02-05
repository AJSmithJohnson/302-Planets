using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorProperties : MonoBehaviour
{
    List<GameObject> planets = new List<GameObject>();

    public Vector3 foci = Vector3.zero;

    public int fociNumber = 0;

    public int totalFoci = 0;

    public bool pause = false;

    public bool rewind = false;

    public bool fastF = false;

    public float interpValue;

    public float cachedTime;

    public float speed = .01f;

    private static BehaviorProperties instance;
    public float fastSpeed = .05f;

    public static BehaviorProperties Instance
    {
        get
        {
            if (instance == null) Debug.LogError("bad");

            return instance;
        }
    }

    public void AddPlanetToList(GameObject planet)
    {
        planets.Add(planet);
    }
    public void SetFociTotal()
    {
        totalFoci = planets.Count;
    }
    public void NextPlanet()
    {
        
        if(fociNumber < planets.Count-1)
        {
            fociNumber++;
            print(fociNumber);
        }
        else
        {
            fociNumber = 0;
        }
    }
    public void PrevPlanet()
    {
        if(fociNumber > 0)
        {
            fociNumber--;
            print(fociNumber);
        }
        else
        {
            fociNumber = planets.Count-1;
        }
    }

    public void SetFoci()
    {

    }

    public GameObject PlanetTransform()
    {
        if (planets[fociNumber] != null)
        {
            return planets[fociNumber];
        }
        else return null;
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetPause()
    {
        
        pause = true;
        fastF = false;
        rewind = false;
    }

    public void SetPlay()
    {
        pause = false;
        fastF = false;
        rewind = false;
    }

    public void SetRewind()
    {
        pause = false;
        fastF = false;
        rewind = true;
    }

    public void SetFastFoward()
    {
        pause = false;
        fastF = true;
        rewind = false;
    }

    public float GlobalTime()
    {
        if(rewind)
        {
            interpValue -= speed * Time.deltaTime;
        }
        else if(fastF)
        {
            interpValue += fastSpeed * Time.deltaTime;
        }
        else if (!pause)
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
