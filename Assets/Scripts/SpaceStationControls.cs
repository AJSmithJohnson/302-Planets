using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationControls : MonoBehaviour
{
    /// <summary>
    /// Decides if we want to rotate to the left or right
    /// </summary>
    public bool flip;

    /// <summary>
    /// Two rotation values that the SpaceStation can use to swivel along
    /// </summary>
    public Quaternion a;
    public Quaternion b;

    /// <summary>
    /// Floats used to hold information on the angles of the SpaceStation
    /// </summary>
    float xAngle = 0;
    float yAngle = 0;
    float zAngle = 0;

    /// <summary>
    /// How fast we want to rotate the station 
    /// </summary>
    public float rotateSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //This can be marked for deletion once we the ability to flip the spacestation through
        //a button click or something
        yAngle = (rotateSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //dont need this just yet
       // AdjustAngles(flip);//We adjust our angles
        transform.Rotate(xAngle, yAngle, zAngle, Space.World);// we apply our rotation
    }

    /// <summary>
    /// This actually sets the rotation of the space station
    /// </summary>
    /// <param name="flip">A boolean we use to decide if we want to flip the rotation direction</param>
    private void AdjustAngles(bool flip)
    {
        if(flip == true)
        {
            yAngle = -(rotateSpeed * Time.deltaTime); //This is our yAngle allowing the spacestation to spin to the left
        }
        else
        {
            yAngle = (rotateSpeed * Time.deltaTime); //This is our yAngle allowing the spacestation to spin to the right
        }
    }//End of Adjust Angles method
}
