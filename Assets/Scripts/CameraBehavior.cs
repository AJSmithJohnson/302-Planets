using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    private Transform currentTransform;
    public GameObject planetTransform;

    private float percent;

    public float animationTime = 2;

    public float animationTimeCurrent = 0;

    public bool tracking = true;

    public AnimationCurve curve;

    public Vector3 offset;

    private Vector3 rotateDirection;

    public float rotateSpeed;

    private float distanceFloat;

    Vector3 currentEaseTarget;

    //VECTOR3.SmoothDamp is something we should use for camera movement


    // Start is called before the first frame update
    void Start()
    {
        currentTransform = transform;
        planetTransform = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(tracking)
        {
            if(planetTransform != null)
            {
                print("Here");
                animationTimeCurrent += Time.deltaTime;
                percent = animationTimeCurrent / animationTime;
                CalcPosition();
                SetRotateDirection();
                //transform.LookAt(planetTransform.transform.position);
            }
        }
    }//End update

    private void SetRotateDirection()
    {
        rotateDirection = planetTransform.transform.position - transform.position;
        float speedStep = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateDirection, speedStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void GetNewTarget(GameObject planetToTrack)
    {
        currentTransform.position = transform.position;
        planetTransform = planetToTrack;
        tracking = true;
    }

    public void CalcPosition()
    {
        if (currentTransform == null || planetTransform == null) return;

        float p = curve.Evaluate(percent);

        currentEaseTarget = AnimMath.Lerp(currentTransform.position, planetTransform.transform.position, p);

        transform.position = AnimMath.Dampen(transform.position, currentEaseTarget, .1f);
        
    }
}
