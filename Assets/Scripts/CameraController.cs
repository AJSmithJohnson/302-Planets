using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject planetToFollow = null;
    public float offSetValue = 0.25f;
    public Vector3 offset;

    private bool wantToTrackPlanets = false;

    public float cameraSpeed = 15;
    
    public float sensitivity = 100f;

    float xRotation = 0f;

    public Transform body;

    public bool wantCursor = false;
    // Start is called before the first frame update
    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SetTrackingPlanets();

        }
        print(wantToTrackPlanets);
        if (!wantToTrackPlanets)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            // body.Rotate(transform.up * mouseX);
            //body.rotation= Quaternion.Euler(0, mouseX, 0);//This sucks don't use it
            body.Rotate(Vector3.up * mouseX);

           

            if (Input.GetKey(KeyCode.S))
            {
                body.transform.position -= (cameraSpeed * transform.forward) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                body.transform.position += (cameraSpeed * transform.forward) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                body.transform.position += (cameraSpeed * Vector3.up) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                body.transform.position -= (cameraSpeed * Vector3.up) * Time.deltaTime;
            }
        }
    }

    private void SetTrackingPlanets()
    {
        if(wantToTrackPlanets)
        {
            wantToTrackPlanets = false;
            //CursorControl(true);
        }
        else
        {
            wantToTrackPlanets = true;
            //CursorControl(false);
        }
    }

    private void CursorControl(bool cursorStatus)
    {
        if (cursorStatus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
           
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

      if(wantToTrackPlanets)
        {
            if (planetToFollow != null)
            {

                body.transform.position = Vector3.Lerp(body.transform.position, planetToFollow.transform.position - offset, offSetValue);
                transform.LookAt(planetToFollow.transform.position);
            }

        }//End of want to track planets statement

      


    }
}
