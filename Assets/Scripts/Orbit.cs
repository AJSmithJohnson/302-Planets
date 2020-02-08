﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Orbit : MonoBehaviour
{
    /// <summary>
    /// rotation along the Y axis also referred to as ____________
    /// </summary>
    public float rotationY = 30;
    /// <summary>
    /// Rotation along the X axis also referred to as ___________
    /// </summary>
    public float rotationX = 15;
    /// <summary>
    /// Rotation along the Z axis also referred to as _______________
    /// </summary>
    public float rotationZ = 10;

    /// <summary>
    /// The transform that holds the center of the object to orbit
    /// </summary>
    public Transform orbitCenter;

    /// <summary>
    /// A true false value for if this is a moon or not
    /// </summary>
    public bool isMoon = false;

    /// <summary>
    /// A reference to our line renderer component
    /// </summary>
    public LineRenderer orbitPath;

    /// <summary>
    /// The magnitude X of the path
    /// </summary>
    [Range(1, 60)] public float magX = 6;
    /// <summary>
    /// The magnitude Y of the path
    /// </summary>
    [Range(1, 60)] public float magY = 6;

    /// <summary>
    /// The points along the path
    /// </summary>
    [Range(4, 32)] public int resolution = 30;


    /// <summary>
    /// The values this should interpolate between so we can scrub forward and back\
    /// </summary>
    public float interpA = 0;
    public float interpB = 2;


    /// <summary>
    /// The percentage we are along the path
    /// </summary>
    public float percentage = 0;



    // Start is called before the first frame update
    void Start()
    {
        #region OldCode For Reference
        /* if(!isMoon)
         {
             magX = Random.Range(10, 60);
             magY = Random.Range(10, 60);
         }
         else
         {
             magX = Random.Range(3, 8);
             magY = Random.Range(3, 8);
         }*/
        #endregion

        //Set values depending on if this is a moon or not
        SetMags(isMoon);
        //Set rotations for the path
        SetRotations();
        
        //Get the LineRenderer component and store it in the orbitPath variable
        orbitPath = GetComponent<LineRenderer>();
        //Make the orbitPath loop
        orbitPath.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        //this float radius is lerping from interpA to interpB using a time value inherited from BehaviorProperties
        float radius = AnimMath.Lerp(interpA, interpB, BehaviorProperties.Instance.GlobalTime());

        //We call the FindOrbitPoint method and pass in:
        //Our radius which is the point along the path we want to be right now
        //Our magX which is a random value that adjusts what the path looks like 
        //Our magY which is a random value that adjusts what the path looks like
        //Since both those values are variables I don't think we need to pass them in
        Vector3 pos = FindOrbitPoint(radius);

        //We set the Vector3 pos we get above to our transform position
        transform.position = pos;

        //We update the points in the path
        UpdatePoints();
    }


   
    /// <summary>
    /// This returns a vector 3 value that we use to
    /// A: Get our current position along the path and set our object to that
    /// B: Set our path points
    /// </summary>
    /// <param name="angle">A float value we get from interpolating between two points
    /// Used to determine how far along a path we are
    /// </param>
    /// <returns></returns>
    private Vector3 FindOrbitPoint(float angle)
    {
       
        //If the orbitCenter is null we set it to Vector3.Zero otherwise the Vector3 pos is set to orbitCenter.position
        Vector3 pos = (orbitCenter == null) ? Vector3.zero : orbitCenter.position;

        #region If we want a passive rotation on our path we should apply this
        //IN THE BELOW CODE VAR IS USED TO ROTATE THE ROTATION
        ///float var = Mathf.Atan2(pos.x * multiplyVar, pos.y * multiplyVar)  * Mathf.Rad2Deg;
        //float var = Mathf.Atan2(pos.x, pos.y);
        #endregion

        #region old reference code
        //var = var + multiplyVar * Mathf.Deg2Rad; //This would control the angle by which it rotates around
        //var = var + Time.time * Mathf.Deg2Rad * multiplyVar; //This would make something rotate around continually
        //float mag2 = Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y) * multiplyVar; //This does nothing that I want it t
        // pos.y +=Mathf.Sin(angle + var) * mag;    // This is old code I don't think I need anymore
        #endregion


        //Moves stuff around a circular path
        pos.x += Mathf.Cos(angle + rotationX) * magX;
        pos.z += Mathf.Sin(angle + rotationZ) * magY;
        pos.y += Mathf.Sin(angle + rotationY) * magY;

        //Return position as Vector3
        return pos;
    }//End of FindOrbitPoint Method


    ///A method used to set points in the line renderer
    void UpdatePoints()
     {
        
            //We create an array of Vector3's called points that is set to the length of the resolution variable
            //The higher the resolution the smoother our path
            Vector3[] points = new Vector3[resolution];

            //A for loop that cycles through up to points.Length
            for (int i = 0; i < points.Length; i++)
            {
                //so we want a percentage because we want to know exactly how far along the path we are
                float p = i / (float)points.Length; //we are getting a percentage by dividing the current point from the amount of points total
                //There are two pi radians in a circle
                float radians = p * Mathf.PI * 2;
                //points[i] is set to the Vector3 value that FindOrbitPoint(radians) returns
                points[i] = FindOrbitPoint(radians);
            }//End of for loop
           
            //the positionCount of the line renderer is set to resolution
            orbitPath.positionCount = resolution;
            //We set the path's positions equal to the points positions
            orbitPath.SetPositions(points);
     }//End of UpdatePoints() method

    /// <summary>
    /// Set the magnitude of the paths
    /// </summary>
    /// <param name="moon">A boolean value used to determine if this is a moon or not</param>
    void SetMags(bool moon)
    {
        //If we are not a moon we set higher ranges
        if(!moon)
        {
            magX = Random.Range(10, 60);
            magY = Random.Range(10, 60);
        }
        else//Otherwise we set smaller ranges
        {
            magX = Random.Range(3, 8);
            magY = Random.Range(3, 8);
        }
    }//End of SetMags() method

    /// <summary>
    /// We SetRotations() of the paths
    /// </summary>
    private void SetRotations()
    {
        //Sets a rotation x,y,z for the paths
        rotationX = Random.Range(0, 180);
        rotationY = Random.Range(0, 180);
        rotationZ = Random.Range(0, 180);
    }//End of SetRotations() method

}
