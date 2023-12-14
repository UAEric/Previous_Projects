using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToShip : MonoBehaviour
{
    //get gameobjects prefabs for the inspector
    //for Sparkle and explosion effects and the ship prefab
    public GameObject SparksparticlePrefab;
    private GameObject SparksparticleInstance;
    public GameObject ExplosionPrefab;
    public GameObject explosionInstance;
    public GameObject Ship;

    //float timme for line renderer on the ship
    float time = 1f;

    // make ship spawn in a postion
    public GenerateShip spawnShip;
    Vector3 position;

    //make a list for asteroids and get game object
    public GameObject[] pointsPrefabs;
    public GameObject instAsteroid;
    int point;

    //get x posion for asteroids
    float xposition = -5f;
    
    //gets color of ray

    //line render is blue when game starts
    void Start()
    {
        transform.GetComponentInChildren<LineRenderer>().material.color = Color.blue;
    }
    
    
    void Update()
    {
        //get the line render and ray cast to show on screen
        Ship = spawnShip.instShip;
        RaycastHit hit;
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.transform.forward);

        //if linerender collides with ship its yellow and sparkle effect is playing
        if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Ship"))
        {
            transform.GetComponentInChildren<LineRenderer>().material.color = Color.yellow;
            Ship.GetComponent<MeshRenderer>().material.color = Color.yellow;
            if (SparksparticleInstance == null)
            {
                SparksparticleInstance = Instantiate(SparksparticlePrefab, hit.point, Quaternion.identity);
            }
            if (!SparksparticleInstance.GetComponent<ParticleSystem>().isPlaying)
                SparksparticleInstance.GetComponent<ParticleSystem>().Play();

            SparksparticleInstance.transform.position = hit.point;
        }
        else
        {
            //if line render is off of ship its blue and partcle effect stops
            //and the ship color changes 
            transform.GetComponentInChildren<LineRenderer>().material.color = Color.blue;
            Ship.GetComponent<MeshRenderer>().material.color = spawnShip.shipColor;
            time = 1;
            if (SparksparticleInstance != null && SparksparticleInstance.GetComponent<ParticleSystem>().isPlaying)
                SparksparticleInstance.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        //if line renderer is hitting ship for 3 seconds it destorys the ship
        //and a new one is spawned in and a random aastroid is spawned on the ground 
        // with a random color to the asteroid
        if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Ship"))
        {
            time += 1 * Time.deltaTime;
            if (time >= 3)
            {
                //cause explosion, destroy ship, spawn new ship, reset timer 
                StartCoroutine(UpdateExplosion());
                Destroy(hit.collider.gameObject);
                spawnShip.SpawningShip();
                time = 1;

                //Asteroids spawning
                position = new Vector3(xposition, 0, 2);
                point = Random.Range(0, pointsPrefabs.Length);
                instAsteroid = Instantiate(pointsPrefabs[point], position,Quaternion.identity);
                instAsteroid.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f),
                    Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                xposition += 1;
            }
        }
    
    }
    
    //spawns explosion particle and destroys after 2 seconds
    IEnumerator UpdateExplosion()
    {
        explosionInstance = Instantiate(ExplosionPrefab, Ship.transform.position, Ship.transform.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(explosionInstance);
    }

}
