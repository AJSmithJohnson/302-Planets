using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    //Amount of planets to spawn
    [Range(8, 32)]public int planetNumber = 10;
    //Odds that a planet will have a moon
    [Range(0, 1)]public float moonOdds = .3f;
    //Amount of moons to spawn
    [Range(8, 32)] public int moonWanted = 10;

    public int currentMoons;

    public bool moreMoons;

    public int moonRolls;

    public float moonScale = .5f;
    
    void Start()
    {
        int[] planetArray = new int[planetNumber];
        int[] moonArray = new int[moonWanted];

        for(int i = 0; i < planetArray.Length; i++)
        {
            GameObject planet = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);
            AttachOrbitScript(planet);
            
            if(moonWanted < 0 && moreMoons == true)
            {
                for(int f = 0; f < moonRolls; f++)
                {
                    float moonChanceB = Random.Range(.1f, .9f);
                    moonOdds += moonChanceB;
                    float moonChance = Random.Range(.5f, .7f);
                    if (moonOdds > moonChance)
                    {
                        HasMoon(planet);
                    }
                        
                }
                
            }
            if(currentMoons < moonWanted)
            {
                HasMoon(planet);
            }
            
            
        }//End of for loop

    }

   


    void AttachOrbitScript(GameObject planet)
    {
        planet.AddComponent<Orbit>();
        
    }


    void HasMoon(GameObject planetToAttachTo)
    {
        GameObject moon = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);
        moon.name = "Moon";

        moonScale = Random.Range(.3f, .7f);
        moon.transform.localScale = new Vector3(moonScale, moonScale, moonScale);
        AttachOrbitScript(moon);
        moon.GetComponent<Orbit>().orbitCenter = planetToAttachTo.transform;
        moon.GetComponent<Orbit>().isMoon = true;
    }
}
