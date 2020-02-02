using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SmallPlanetOrbit : MonoBehaviour
{
    //what this planet should orbit around
    public Transform orbitCenter;

    //A reference to the linerenderer on this object
    public LineRenderer orbitPath;

    //The magnitude of the rotation
    [Range(1, 100)] public float magnitude = 6;

    //Resolution is the amount of points along the path
    [Range(4, 100)] public int resolution = 8;

    public float interpolatePointA = 0;

    public float interpolatePointB = 2;

    public float percentage = 0;

    // Start is called before the first frame update
    void Start()
    {
        orbitPath = GetComponent<LineRenderer>();
        orbitPath.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        float radius = AnimMath.Lerp(interpolatePointA, interpolatePointB, percentage);

        //We pass in time.time to make the planet move over time
        //We pass in magnitude to adjust how far around the planet the orbit is
        //So instead of time we can use 
        Vector3 pos = FindOrbitPoint(radius, magnitude);//This returns a vector 3 we want to use for our transform.position

        transform.position = pos;//we set this vector 3 equal to our position to move the planet around

        //We update the points along the line renderer
        UpdatePoints();
    }


    /// <summary>
    /// This finds the orbit path and returns a vector 3 we can use to adjust out planets position
    /// </summary>
    /// <param name="angle">The angle we move our planet along
    /// We can pass in Time.time above to move our planet as the update loop ticks alsong
    /// I can try to interpolate between two values then use that value to move the planets along
    /// </param>
    /// <param name="magnitude">How far from the recieved position up above we rotate</param>
    /// <returns></returns>
    private Vector3 FindOrbitPoint(float angle, float magnitude)
    {
        //If orbit center is null then Vector3 is zero otherwise orbitCenter is set to the 
        //reference position we get up above
        Vector3 pos = (orbitCenter == null) ? Vector3.zero : orbitCenter.position;

        //we use this variable to get a rotation value
        float var = Mathf.Atan2(pos.x, pos.y);

        //we modify the position to get the planets rotating
        pos.x += Mathf.Cos(angle) * 150;
        pos.y -= Mathf.Sin(angle) * magnitude;

        return pos;
        
    }

    /// <summary>
    /// This updates the points along the orbit path for the line renderer
    /// </summary>
    void UpdatePoints()
    {
        //The amount of points along our path
        Vector3[] points = new Vector3[resolution];

        for(int i = 0; i < points.Length; i ++)
        {
            //we want percentage because we want to know exactly how far along the path we are
            float p = i / (float)points.Length;
            //There are two pi radians in a circle
            float radians = p * Mathf.PI * 2;

            //so we are sending radians which is the angle along the circle
            //we are also sending in magnitude which is how far away from the center we want to be
            points[i] = FindOrbitPoint(radians, magnitude); //This sets the points position
        }//End of points.length for loop

        orbitPath.positionCount = resolution; //we set the position count equal to the resolution which is how defined we want the line renderer to be
        orbitPath.SetPositions(points); //then we set positions alongside the line equal to the points we get
    }
}
