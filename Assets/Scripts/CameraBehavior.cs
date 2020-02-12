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

    public Vector3 currentEaseTarget;

    public float cameraSpeed;
    public Transform camBody;
    public float sensitivity;
    public float xRotation;
    public float yRotation;
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
        if (Input.GetMouseButtonDown(2))
        {
            BreakTarget();
        }
        if(!tracking)
        {
            GetPlayerInput();
        }

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

    private void GetPlayerInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -180, 180);

        camBody.localRotation = Quaternion.Euler(xRotation, mouseY, 0f);

        if(Input.GetKey(KeyCode.S))
        {
            camBody.transform.position -= (cameraSpeed * transform.forward) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.W))
        {
            camBody.transform.position += (cameraSpeed * transform.forward) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            camBody.transform.position += (cameraSpeed * Vector3.up) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            camBody.transform.position -= (cameraSpeed * Vector3.up) * Time.deltaTime;
        }

    }

    private void SetRotateDirection()
    {
        rotateDirection = planetTransform.transform.position - transform.position;
        float speedStep = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateDirection, speedStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void GetNewTarget(GameObject planetToTrack)
    {
        animationTimeCurrent = 0;
        currentTransform.position = transform.position;
        planetTransform = planetToTrack;
        tracking = true;
    }


    public void BreakTarget()
    {
        camBody = transform;
        animationTimeCurrent = 0;
        //need to store current transform on update constantly
        tracking = false;
    }

    public void CalcPosition()
    {
        if (currentTransform == null || planetTransform == null) return;

        float p = curve.Evaluate(percent);

        currentEaseTarget = AnimMath.Lerp(currentTransform.position, planetTransform.transform.position, p);

        transform.position = AnimMath.Dampen(transform.position, currentEaseTarget, .1f);
        
    }
}
