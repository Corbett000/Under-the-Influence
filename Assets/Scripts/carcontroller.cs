using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carcontroller : MonoBehaviour
{

    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f; 
    public float driftFactor = 0.95f;
    public float velocityCap = 10f;
    public Sprite[] carSkins;

    float accelerationInput = 0;
    float steeringInput = 0;
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
        carSpriteRenderer.sprite = carSkins[Random.Range(0,carSkins.Length-1)];
    }

    // Update is called once per frame
    void Update()
    {
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
        if (accelerationInput > 0)
        {
            accelerationFactor = velocityCap - carRigidbody2D.velocity.magnitude;
        }
        else
        {
            accelerationFactor = velocityCap - carRigidbody2D.velocity.magnitude;
            
        }
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
