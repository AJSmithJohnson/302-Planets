using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform currentTransform;
    [SerializeField] private Transform planetTransform;

    [Range(0, 1)] public float percent;

    public float animationTime = 1;

    public float animationTimeCurrent = 0;

    bool wantToTrack = true;

    public AnimationCurve curve;

    public Vector3 offSet;

    Vector3 currentEaseTarget;

    public float radius = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Radial detection for this garbage
        float distanceX = planetTransform.position.x - transform.position.x;
        float distanceY = planetTransform.position.y - transform.position.y;
        float distanceZ = planetTransform.position.z - transform.position.z;
        float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY + distanceZ * distanceZ);
        print(distance);

        //Need to tweak and make these values modular so that we can make this system a bit more rebust
        //Off the top of my head we should probably keep the array of planets
        //and then when I click the button I flip a variable to swap from thing to thing and blah blah blah blahblah blah blaaaahhh
        if(radius > distance)
        {
            wantToTrack = false;
        }
        if(wantToTrack)
        {
            animationTimeCurrent += Time.deltaTime;
            percent = animationTimeCurrent / animationTime;
            CalcPosition();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, planetTransform.transform.position, .5f);
            transform.LookAt(planetTransform.transform);
        }
        
    }

    public void CalcPosition()
    {
        if (currentTransform == null || planetTransform == null) return;

        float p = curve.Evaluate(percent);

        currentEaseTarget = AnimMath.Lerp(currentTransform.position, planetTransform.position, p);

        transform.position = AnimMath.Dampen(transform.position, currentEaseTarget, .1f);
        transform.LookAt(planetTransform);
    }
}
