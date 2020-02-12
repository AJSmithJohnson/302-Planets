using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private Transform currentTransform;
    public GameObject planetTransform;

    [Range(0, 1)] public float percent;

    public float animationTime = 1;

    public float animationTimeCurrent = 0;

    public bool wantToTrack = true;

    public AnimationCurve curve;

    public Vector3 offSet;

    private Vector3 distance;

    private float distanceFloat;

    Vector3 currentEaseTarget;

    public float radius = 50;
    // Start is called before the first frame update
    void Start()
    {
        currentTransform = transform;
        wantToTrack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //print(planetTransform);
        ///print(distanceFloat);
        print(wantToTrack);
        if (wantToTrack)
        {
            if(planetTransform != null)
            {
                distance.x = planetTransform.transform.position.x - transform.position.x;
                distance.y = planetTransform.transform.position.y - transform.position.y;
                distance.z = planetTransform.transform.position.z - transform.position.z;
                distanceFloat = Mathf.Sqrt(distance.x * distance.x + distance.y * distance.y + distance.z * distance.z);
                print(distanceFloat);

            }
             
        }
        //Radial detection for this garbage
        

        //Need to tweak and make these values modular so that we can make this system a bit more rebust
        //Off the top of my head we should probably keep the array of planets
        //and then when I click the button I flip a variable to swap from thing to thing and blah blah blah blahblah blah blaaaahhh
       if(planetTransform != null && radius > distanceFloat)
       {
           //wantToTrack = false;
       }
        else if(wantToTrack)
        {
            animationTimeCurrent += Time.deltaTime;
            percent = animationTimeCurrent / animationTime;
            CalcPosition();
        }
       /* if(!wantToTrack)
        {
            print("In else block");
            

                print("Can't break into here?");
                transform.position = Vector3.Lerp(transform.position, planetTransform.transform.position - offSet, .5f);
                transform.LookAt(planetTransform.transform);
            
            
        }*/
        
    }

    public void CalcPosition()
    {
        if (currentTransform == null || planetTransform == null) return;

        float p = curve.Evaluate(percent);

        currentEaseTarget = AnimMath.Lerp(currentTransform.position, planetTransform.transform.position, p);

        transform.position = AnimMath.Dampen(transform.position, currentEaseTarget, .6f);
        transform.LookAt(planetTransform.transform.position);
    }
}
