using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerControls : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float cameraSpeed;
    public Camera main;  
    private float rotX;
    private float rotY;
    private float minValue = -30f;
    private float maxValue = 30f;
    public GameObject other;
    public Countdown youWin;
    public GameObject youWinTxt;
	public delegate void rotCam();
    public delegate void TimeDel();
    public LightsOn lso;
    public GameObject theLight;
	void Start()
    {
        //turns lights on beginning of game after the game is completed once in runtime
        rb = GetComponent<Rigidbody>();
        if (lso.turnOn == true)
        {
            theLight.GetComponent<Light>().intensity = 1.0f;
        }
        else
        {
         	theLight.GetComponent<Light>().intensity = lso.intense;
        }
    }
    //moves the player forward based on WASD
    void Update()
    {
		rotCam del = RotateCamera;
        Vector3 movement = main.transform.forward * (Input.GetAxis("Vertical") * speed) + main.transform.right * (Input.GetAxis("Horizontal") * speed);
        float mag = movement.magnitude;
        movement = movement.normalized * mag;
        rb.velocity = movement;
        del();
    }
	//rotates the camera
    void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
			TimeDel del = delegate()
			{
			rotY += cameraSpeed * Input.GetAxis("Mouse X");
            rotX -= cameraSpeed * Input.GetAxis("Mouse Y");

            rotX = Mathf.Clamp(rotX, minValue, maxValue);

            main.transform.eulerAngles = new Vector3(rotX, rotY, 0);
			};
			del();
        }
    }
	//Detects collision with end goal, will pop up "you win" message and then take back to home screen
    //Sets bool of lso to true
    void OnCollisionEnter(Collision col)
    {
        other = col.gameObject;
        if (other.GetComponent<IsLevelEnd>() != null)
        {
            StartCoroutine(youWin.EndGame(youWinTxt));
            lso.turnOn = true;
        }
    }
}
