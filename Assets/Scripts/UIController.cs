using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    public Transform posA;
    public Transform posB;

    public Button pause;
    public Button player;
    public Button rewind;
    public Button fastForward;

    bool showingUI;

    bool moveButtonBack = false;

    bool doPause = true;

    public float lerpOverTime;
    // Start is called before the first frame update
    void Start()
    {
        HideButtons();
        pause.onClick.AddListener(Pause);
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RevealButtons();
        }
    }

    private void Pause()
    {
        if(!doPause)
        {
            BehaviorProperties.Instance.SetPause(true);
            doPause = true;
        }
        else
        {
            BehaviorProperties.Instance.SetPause(false);
            doPause = false;
        }
        
    }

    private void RevealButtons()
    {
        if(showingUI)
        {
            showingUI = false;
            SpawnButtons();
        }
        else
        {
            showingUI = true;
            HideButtons();
        }
    }

    /// <summary>
    /// THE PROBLEM IS WE NEED TO ADJUST THE LERP STUFF HERE
    /// The EASIEST THING TO DO WOULD BE TO ADD AN UPDATE LOOP
    /// </summary>

    private void HideButtons()
    {
        pause.gameObject.SetActive(false);
        pause.enabled = false;
        //LerpButtons(pause.gameObject);
    }

    private void SpawnButtons()
    {
        pause.gameObject.SetActive(true);
        pause.enabled = true;
        //LerpButtons(pause.gameObject);

        //Get these guys working later
        //player.gameObject.SetActive(true);
        //rewind.gameObject.SetActive(true);
        //fastForward.gameObject.SetActive(true);
    }

    private void LerpButtons(GameObject buttonToMove)
    {
        if(!moveButtonBack)
        {
            for (int i = 0; i < 100; i++)
            {
                if(i != 0)
                {
                    i = i / 100;
                    Vector3 posMover = AnimMath.Lerp(posA.transform.position, posB.transform.position, i);
                    buttonToMove.gameObject.transform.position += new Vector3(0, posMover.y, 0);
                }
                
            }
            moveButtonBack = true;
        }
        else
        {
            for (int i = 100; i > 0; i--)
            {
                if(i != 0)
                {
                    i = i / 100;
                    Vector3 posMover = AnimMath.Lerp(posA.transform.position, posB.transform.position, i);
                    buttonToMove.gameObject.transform.position += new Vector3(0, posMover.y, 0);
                }
                
            }
            moveButtonBack = false;
        }
        
    }

   

    
}
