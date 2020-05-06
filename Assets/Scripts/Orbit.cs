using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Orbit : MonoBehaviour
{
    Renderer rend;

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
    /// A reference to our line renderer component
    /// </summary>
    private LineRenderer orbitPath;

    /// <summary>
    /// The values this should interpolate between so we can scrub forward and back\
    /// </summary>
    private float interpA = 0;
    private float interpB = 2;
    private float interpValue = 0;

    /// <summary>
    /// The percentage we are along the path
    /// </summary>
    //private float percentage = 0;

    /// <summary>
    /// The speed of the planets moving
    /// </summary>
    private float speed = .5f;

    /// <summary>
    /// Wiether or not our planet should be paused
    /// </summary>
    private bool pause;

    public float color1;
    public float color2;
    public float color3;
    public float scrollX;
    public float scrollY;
    public float emission;

    public float rotationSpeed;
    public float regularRotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Custom/PlanetShader");

        color1 = Random.Range(0.0f, 1.0f);
        color2 = Random.Range(0.0f, 1.0f);
        color3 = Random.Range(0.0f, 1.0f);
        scrollX = Random.Range(-10, 10);
        scrollY = Random.Range(-10, 10);
        emission = Random.Range(.2f, .5f);
        rotationSpeed = Random.Range(1, 20);
        regularRotationSpeed = rotationSpeed;
        if (isMoon)
        {
            emission = Random.Range(.5f, 2.0f);
        }
        rend.material.SetFloat("_Color1", color1);
        rend.material.SetFloat("_Color2", color2);
        rend.material.SetFloat("_Color3", color3);
        rend.material.SetFloat("_Emission", emission);
        rend.material.SetFloat("_ScrollX", scrollX);
        rend.material.SetFloat("_ScrollY", scrollY);

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


        UIController.triggerPause += Pause;
        UIController.normal += Normal;
        UIController.rewindPlanets += Rewind;
        UIController.speedUp += SpeedUp;


        //Set values depending on if this is a moon or not
        SetMags(isMoon);
        //Set rotations for the path
        SetRotations();
     

        //Get the LineRenderer component and store it in the orbitPath variable
        orbitPath = GetComponent<LineRenderer>();
        //Make the orbitPath loop
        orbitPath.loop = true;
    }//End of start method


    public void Pause()
    {
        pause = !pause;
    }

    public void Normal()
    {
        if (pause) pause = !pause;
        speed = .5f;
        rotationSpeed = regularRotationSpeed;
    }

    public void SpeedUp()
    {
        if (pause) pause = !pause;
        speed += .5f;
        rotationSpeed += .5f;
    }

    public void Rewind()
    {
        if (pause) pause = !pause;
        speed = - 1.5f;
        rotationSpeed = .5f;
    }

    public float InterpTime()
    {
        if (!pause)
        {
            interpValue += speed * Time.deltaTime;
        }
        return interpValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause)
        {
            //lerpPercent += planetSpeed * Time.deltaTime;
        }

        //this float radius is lerping from interpA to interpB using a time value inherited from BehaviorProperties
        //float radius = AnimMath.Lerp(interpA, interpB, BehaviorProperties.Instance.GlobalTime());
        float radius = AnimMath.Lerp(interpA, interpB, InterpTime());

        //We call the FindOrbitPoint method and pass in:
        //Our radius which is the point along the path we want to be right now
        //Our magX which is a random value that adjusts what the path looks like 
        //Our magY which is a random value that adjusts what the path looks like
        //Since both those values are variables I don't think we need to pass them in
        Vector3 pos = FindOrbitPoint(radius);

        //We set the Vector3 pos we get above to our transform position
        transform.position = pos;

        transform.Rotate(Time.deltaTime * rotationSpeed, Time.deltaTime * rotationSpeed, Time.deltaTime * rotationSpeed);

        //We update the points in the path
        UpdatePoints();
    }//End of Update method

   

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
