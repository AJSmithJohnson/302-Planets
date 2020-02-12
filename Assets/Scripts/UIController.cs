using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UIController : MonoBehaviour
{

    public GameObject mainCamera;

    public Transform posA;
    public Transform posB;
    public GameObject systemTransform;

    public Button pause;
    public Button play;
    public Button rewind;
    public Button fastForward;
    public Button prevPlanet;
    public Button nextPlanet;
    public Button wholeSystem;

    bool showingUI = true;

    bool moveButtonBack = false;

    bool doPause = true;

    public float lerpOverTime;
    // Start is called before the first frame update
    void Start()
    {
        RevealButtons();
       /* pause.onClick.AddListener(Pause);
        play.onClick.AddListener(Play);
        rewind.onClick.AddListener(Rewind);
        fastForward.onClick.AddListener(FastFoward);
        prevPlanet.onClick.AddListener(PrevPlanet);
        nextPlanet.onClick.AddListener(NextPlanet);
        wholeSystem.onClick.AddListener(UpdateCameraSystem);*/
    }

   

    private void FixedUpdate()
    {
       /* if (Input.GetKeyDown(KeyCode.Escape))
        {
            RevealButtons();
        }*/
    }

    private void Pause()
    {
            BehaviorProperties.Instance.SetPause();
          //  doPause = true;//Not sure I need this anymore
    }
    private void Play()
    {
        BehaviorProperties.Instance.SetPlay();
    }
    public void FastFoward()
    {
        BehaviorProperties.Instance.SetFastFoward();
    }
    public void Rewind()
    {
        BehaviorProperties.Instance.SetRewind();
    }
    public void PrevPlanet()
    {
        print("Here");
        BehaviorProperties.Instance.PrevPlanet();
        UpdateCamera();
    }
    public void NextPlanet()
    {
        print("In next planet");
        BehaviorProperties.Instance.NextPlanet();
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        mainCamera.GetComponent<CamFollow>().wantToTrack = true;
        mainCamera.GetComponent<CamFollow>().planetTransform = BehaviorProperties.Instance.PlanetTransform();
    }
    private void UpdateCameraSystem()
    {
        print("In update camera system");
        mainCamera.GetComponent<CameraController>().planetToFollow = systemTransform;
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

        play.gameObject.SetActive(false);
        play.enabled = false;

        rewind.gameObject.SetActive(false);
        rewind.enabled = false;

        fastForward.gameObject.SetActive(false);
        fastForward.enabled = false;

        prevPlanet.gameObject.SetActive(false);
        prevPlanet.enabled = false;

        nextPlanet.gameObject.SetActive(false);
        nextPlanet.enabled = false;
        //LerpButtons(pause.gameObject);
    }

    private void SpawnButtons()
    {
        pause.gameObject.SetActive(true);
        pause.enabled = true;

        play.gameObject.SetActive(true);
        play.enabled = true;

        rewind.gameObject.SetActive(true);
        rewind.enabled = true;

        fastForward.gameObject.SetActive(true);
        fastForward.enabled = true;

        prevPlanet.gameObject.SetActive(true);
        prevPlanet.enabled = true;

        nextPlanet.gameObject.SetActive(true);
        nextPlanet.enabled = true;
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
