using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMove : MonoBehaviour
{
    public float balloonSpeed;
	public bool newSpeed;
	public timeScript theTime;
	public float changeSpeed;
	public GameObject aTimer;

    void Update()
    { 
		theTime = GameObject.Find("Timer").GetComponent<timeScript>();
		changeSpeed -= Time.deltaTime;
		if(changeSpeed <= 0f)
		{
			//used to increase speed range every 50 seconds
			//used so that speed is not redefined every frame
			newSpeed = true;
			changeSpeed = 50.0f;
		}
		else
		{
			newSpeed = false;
		}
		if(newSpeed)
		{
			//sets the speed within a certain range depending on how much time is left
			if (theTime.times >= 150f)
			{
				balloonSpeed = Random.Range(-0.5f, 0.5f);
			}
			if (theTime.times < 150f && theTime.times >= 100f)
			{
				balloonSpeed = Random.Range(-2.0f, 2.0f);
			}
			if (theTime.times < 100f && theTime.times >= 50f)
			{
				balloonSpeed = Random.Range(-3.0f, 3.0f);
			}
			if (theTime.times < 50f)
			{
				balloonSpeed = Random.Range(-6.0f, 6.0f);
			}
		}
		//moves balloon position in spherical/circular motion
		//if balloon moves out of bounds, the position is changed so that it falls inside the room
		transform.position += new Vector3(Mathf.Cos(balloonSpeed*Time.time)*Time.deltaTime*balloonSpeed, Mathf.Sin(balloonSpeed*Time.time)*Time.deltaTime*balloonSpeed, Mathf.Sin(balloonSpeed*Time.time)*Time.deltaTime*balloonSpeed);
		if(transform.position.z < -7.2f)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, -7.2f);
		}
		if(transform.position.y < 1.2f)
		{
			transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
		}
		if(transform.position.x < -7.2f)
		{
			transform.position = new Vector3(-7.2f, transform.position.y, transform.position.z);
		}
		if(transform.position.x > 7.2f)
		{
			transform.position = new Vector3(7.2f, transform.position.y, transform.position.z);
		}
    }
}
