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
    /// Decides if we want to swivel the space station
    /// </summary>
    public bool swivel;

    /// <summary>
    /// do we play the animation forward or backwards
    /// </summary>
    public bool isPlayingForward = true;

    /// <summary>
    /// Two rotation values that the SpaceStation can use to swivel along
    /// </summary>
    public float a;
    public float b;

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

    /// <summary>
    /// Floats used to keep track of animation 
    /// </summary>
    public float animationTime = 1;
    public float animationTimeCurrent = 0;

    //we use this value to evaluate where the curve is at a specific position
    [Range(0, 1)] public float percent;

    //creates an animation curve that we can use to store keyframes and other info
    public AnimationCurve curve;

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

        if(Input.GetKeyDown(KeyCode.W))
        {
            if(swivel) { swivel = false; }
            else { swivel = true; }
        }//End of Keydown W

        
        if(swivel)
        {
            AnimationUpdate();
        }//end of swivel 


        //dont need this just yet
        // AdjustAngles(flip);//We adjust our angles
        transform.Rotate(xAngle, yAngle, zAngle, Space.World);// we apply our rotation
    }

    private void AnimationUpdate()
    {
        if(isPlayingForward)
        {
            animationTimeCurrent += Time.deltaTime;//Tick up our animation time
            //if (animationTimeCurrent > animationTime) isPlayingForward = false;//If our current animation time gets too high we reverse the boolean and tick down
        }
        else
        {
            animationTimeCurrent -= Time.deltaTime;
            //if (animationTimeCurrent < 0) isPlayingForward = true;// if our current animation time gets too low we reverse the boolean and tick up
        }
        /*if(animationTimeCurrent > 1)
        {
            swivel = false;
        }*/
        percent = animationTimeCurrent / animationTime;
        SwivelPosition();
    }

    /// <summary>
    /// Uses lerp to 
    /// </summary>
    private void SwivelPosition()
    {
        //if (a == null || b == null) return; //These are float values by definition they will have a value

        float p = curve.Evaluate(percent);

        float targetValue = AnimMath.Lerp(a, b, p);
        xAngle = AnimMath.Dampen(0, targetValue, .5f);
        
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
