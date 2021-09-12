using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carcontroller : MonoBehaviour
{

    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f; 
    public float driftFactor = 0.95f;
    public Sprite[] carSkins;
    public GameObject[] woahNellyPrefabs;

    float accelerationInput = 0;
    public float steeringInput = 0;
    float rotationAngle = 0;

    //components 
    Rigidbody2D carRigidbody2D;
    SpriteRenderer carSpriteRenderer;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        carSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //spawn with random car skin
        carSpriteRenderer.sprite = carSkins[Random.Range(0,carSkins.Length)];
    }

    // Update is called once per frame
    void Update() {
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Instantiate(woahNellyPrefabs[Random.Range(0,woahNellyPrefabs.Length)], transform.position, Quaternion.identity);
        Destroy(other.gameObject);
    }

    //frame rate independent stuff
    void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        //create a force
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        //apply that force
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {   
        //update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor;
        //rotate car 
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void KillOrthogonalVelocity()
    {
        //calculate forward and sideways velocity
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
}
