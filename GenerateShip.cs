using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShip : MonoBehaviour
{
    public GameObject ship;
	public Vector3 spawnPos;
	public float rot;
	public GameObject instShip;
	public int winCond;
	public Color shipColor;
	
	//spawns first ship along imaginary cylinder with random speed and positon
	//also gets original ship color and accounts for win condition
	void Start()
	{
		spawnPos = Random.insideUnitCircle.normalized*10;
        instShip = Instantiate(ship, new Vector3(spawnPos.x, Random.Range(1.0f, 6.0f), spawnPos.y), Quaternion.Euler(0, 0, 0));
		instShip.transform.forward = Vector3.down;
		instShip.tag = "Ship";		
		rot = Random.Range(-40.0f, -10.0f);
		shipColor = ship.GetComponent<MeshRenderer>().sharedMaterial.color;
		winCond++;		
	}
	
	//spawns rest of ships
	//after all 10 ships have been spawned, ships are untagged so that ray can not interact with it
   	public void SpawningShip()
    {
		spawnPos = Random.insideUnitCircle.normalized*10;
        instShip = Instantiate(ship, new Vector3(spawnPos.x, Random.Range(1.0f, 6.0f), spawnPos.y), Quaternion.Euler(0f,0f,0f));
		rot = Random.Range(-40.0f, -10.0f);
		instShip.transform.forward = Vector3.down;
		if (winCond < 10)
		{
			instShip.tag = "Ship";
			winCond++;
		}
		else
		{
			instShip.tag = "Untagged";
		}
    }
	
	//rotates ship around origin
	void Update()
	{
		instShip.transform.RotateAround(Vector3.zero, Vector3.up, rot*Time.deltaTime);
	}
}