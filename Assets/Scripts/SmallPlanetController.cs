using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPlanetController : MonoBehaviour
{

    public float magPartA = 150;
    public float magPartB = 150;

    public float speed = 1;

    public float force = 5;
    public float mass = 5;

    public float maxAcceleration = 100;
    public float acceleration = 20;
    public Transform center;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //cache delta time in a value
        //use this value to change if stuff pauses
        Vector3 planetRotator = new Vector3();

        //float acceleration = force / mass;
       // if (acceleration >= maxAcceleration) acceleration = maxAcceleration;
        planetRotator.x = (magPartA * Mathf.Cos(Time.time * acceleration) + center.position.x);
        planetRotator.y = (magPartB * Mathf.Sin(Time.time * acceleration) + center.position.y);

        transform.position = planetRotator;

    }
}
