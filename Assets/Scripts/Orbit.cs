using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Orbit : MonoBehaviour
{
    //How to rotate something along the Y axis
    public float rotationY = 30;

    public float rotationX = 15;

    public float rotationZ = 10;

    public Transform orbitCenter;

    public bool isMoon = false;

    public LineRenderer orbitPath;
    [Range(1, 60)] public float magX = 6;
    [Range(1, 60)] public float magY = 6;
    [Range(4, 32)] public int resolution = 30;

    public float interpA = 0;
    public float interpB = 2;

    public float percentage = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(!isMoon)
        {
            magX = Random.Range(10, 60);
            magY = Random.Range(10, 60);
        }
        else
        {
            magX = Random.Range(3, 8);
            magY = Random.Range(3, 8);
        }


        rotationX = Random.Range(0, 180);
        rotationY = Random.Range(0, 180);
        rotationZ = Random.Range(0, 180);

        orbitPath = GetComponent<LineRenderer>();
        orbitPath.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        float radius = AnimMath.Lerp(interpA, interpB, BehaviorProperties.Instance.GlobalTime());

        Vector3 pos = FindOrbitPoint(radius, magX, magY);
        transform.position = pos;
        UpdatePoints();
    }

    private Vector3 FindOrbitPoint(float angle, float magX, float magY)
    {
       
        Vector3 pos = (orbitCenter == null) ? Vector3.zero : orbitCenter.position;

        //IN THE BELOW CODE VAR IS USED TO ROTATE THE ROTATION
        ///float var = Mathf.Atan2(pos.x * multiplyVar, pos.y * multiplyVar)  * Mathf.Rad2Deg;
        float var = Mathf.Atan2(pos.x, pos.y);


         //var = var + multiplyVar * Mathf.Deg2Rad; //This would control the angle by which it rotates around
        //var = var + Time.time * Mathf.Deg2Rad * multiplyVar; //This would make something rotate around continually
        //float mag2 = Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y) * multiplyVar; //This does nothing that I want it t

        //Rotates stuff around
        pos.x += Mathf.Cos(angle + rotationX) * magX;
        pos.z += Mathf.Sin(angle + rotationZ) * magY;


        //Do we want verticle
        // pos.y +=Mathf.Sin(angle + var) * mag;       
        pos.y +=Mathf.Sin(angle + rotationY) * magY;

        //Return position as Vector3
        return pos;
        
    }

    ///A method used to set points in the line renderer
    void UpdatePoints()
     {
        
            Vector3[] points = new Vector3[resolution];

            for (int i = 0; i < points.Length; i++)
            {
            //so we want a percentage because we want to know exactly how far along the path we are
            float p = i / (float)points.Length; //we are getting a percentage by dividing the current point from the amount of points total
            //There are two pi radians in a circle
            float radians = p * Mathf.PI * 2;
            points[i] = FindOrbitPoint(radians, magX, magY);

            }//End of for loop
           
            orbitPath.positionCount = resolution;
            orbitPath.SetPositions(points);
     }

        
    
}
