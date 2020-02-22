using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    //The planet prefab object we use to spawn planets
    public GameObject planetPrefab;
    //Amount of planets to spawn
    [Range(8, 32)]public int planetNumber = 10;
    //Odds that a planet will have a moon
    [Range(0, 1)]public float moonOdds = .3f;
    //Amount of moons to spawn
    [Range(8, 32)] public int moonWanted = 10;

    
    public bool moreMoons;

    /// <summary>
    /// How many times a planet should roll for a moon
    /// </summary>
    public int moonRolls;

    /// <summary>
    /// The scale of the moons
    /// </summary>
    public float moonScale = .5f;

    /// <summary>
    /// The current amount of moons spawned
    /// </summary>
    private int currentMoons;

    void Start()
    {
        //Populate an array of planets
        int[] planetArray = new int[planetNumber];
        //Populate an array of moons
        int[] moonArray = new int[moonWanted];

        //Loop through the length of our planet array
        for(int i = 0; i < planetArray.Length; i++)
        {
            GameObject planet = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);
            AttachOrbitScript(planet);
            
            //IF THERE ARE BIG ERRORS FLIP THIS TO GREATER THAN OR EQUAL TO
            if(currentMoons <= moonWanted/2 && moreMoons == true)
            {
                for(int f = 0; f < moonRolls; f++)
                {
                    print("here");
                    float moonChanceB = Random.Range(.1f, .3f);
                    moonOdds += moonChanceB;
                    float moonChance = Random.Range(.6f, 1);
                    if (moonOdds > moonChance)
                    {
                        moonOdds = Random.Range(.1f, .4f);
                        HasMoon(planet);
                    }
                        
                }
                
            }
            if(currentMoons < moonWanted)
            {
                HasMoon(planet);
                currentMoons++;
            }
            
            
        }//End of for loop

    }

   


    void AttachOrbitScript(GameObject planet)
    {
        planet.AddComponent<Orbit>();

        BehaviorProperties.Instance.AddPlanetToList(planet);//Add planets to Behavior Properties list
    }


    void HasMoon(GameObject planetToAttachTo)
    {
        GameObject moon = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);

        BehaviorProperties.Instance.AddPlanetToList(moon);//Add planets to Behavior Properties list
        moon.name = "Moon";

        moonScale = Random.Range(.3f, .7f);
        moon.transform.localScale = new Vector3(moonScale, moonScale, moonScale);
        AttachOrbitScript(moon);
        moon.GetComponent<Orbit>().orbitCenter = planetToAttachTo.transform;
        moon.GetComponent<Orbit>().isMoon = true;
    }
}
