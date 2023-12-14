using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Rigidbody rb;
    private float speed = 10f;
    private GameObject[] wallGameObjects;
    private BoxCollider wallCollider;
    private BoxCollider carCollider;
    [SerializeField] private Transform ray1Origin;
    [SerializeField] private Transform ray2Origin;
    [SerializeField] private Transform ray3Origin;
    private bool isBlocking = false;
    private bool wasBlocked = false;
    [SerializeField] private WheelController wheelController;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        carCollider = gameObject.GetComponent<BoxCollider>();

        wallGameObjects = GameObject.FindGameObjectsWithTag("InvisibleWalls");

        foreach (GameObject wall in wallGameObjects) {
            wallCollider = wall.GetComponent<BoxCollider>();
            Physics.IgnoreCollision(carCollider, wallCollider, true);
        }
    }

    void FixedUpdate() 
    {
        RaycastHit hit;

        // ray 1
        if (Physics.Raycast(ray1Origin.position, ray1Origin.TransformDirection(Vector3.forward), out hit, 10f)) {
            isBlocking = true;
            wasBlocked = true;
            wheelController.rotationSpeed = 0;
        } else {
            Debug.DrawRay(ray1Origin.position, ray1Origin.TransformDirection(Vector3.forward) * 10f, Color.white);
            isBlocking = false;
        }

        // ray 2
         if (Physics.Raycast(ray2Origin.position, ray2Origin.TransformDirection(Vector3.forward), out hit, 10f)) {
            isBlocking = true;
            wasBlocked = true;
            wheelController.rotationSpeed = 0;
        } else {
            Debug.DrawRay(ray2Origin.position, ray2Origin.TransformDirection(Vector3.forward) * 10f, Color.white);
            isBlocking = false;
        }

        // ray 3
         if (Physics.Raycast(ray3Origin.position, ray3Origin.TransformDirection(Vector3.forward), out hit, 10f)) {
            isBlocking = true;
            wasBlocked = true;
            wheelController.rotationSpeed = 0;
        } else {
            Debug.DrawRay(ray3Origin.position, ray3Origin.TransformDirection(Vector3.forward) * 10f, Color.white);
            isBlocking = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBlocking && wasBlocked) {
            StartCoroutine(DriveAgain());
        }
        
        if (!isBlocking && !wasBlocked) {
            wheelController.rotationSpeed = 10;
            rb.velocity = transform.forward * speed;
        }
    }

    IEnumerator DriveAgain() 
    {
        Debug.Log("started coroutine");
        yield return new WaitForSeconds(2);
        isBlocking = false;
        wasBlocked = false;
    }
}
