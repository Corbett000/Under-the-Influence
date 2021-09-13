using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class carcontroller : MonoBehaviour {
    public carcontroller controller;
    public BeerInventory inventory;

    public float drunkfactor = .3f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 11f; 
    public float driftFactor = 0.95f;
    public Sprite[] carSkins;
    public GameObject[] woahNellyPrefabs;
    public float velocityCap = 6f;
    public Vector2 postition = new Vector2(0, 0);
    public int score = 0;

    float accelerationInput = 0;
    public float steeringInput = 0;
    float rotationAngle = 0;

    //components 
    Rigidbody2D carRigidbody2D;
    SpriteRenderer carSpriteRenderer;
    public ScriptableUI sUI;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        carSpriteRenderer = GetComponent<SpriteRenderer>();
        inventory = this.gameObject.GetComponent<BeerInventory>();
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
        if (inventory.beerInInventory < 5)
        {
            inventory.beerInInventory += 1;
        }
    }

    //frame rate independent stuff
    void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
        CheckScore();
        drunkfactor -= .0001f;
        if (drunkfactor<.1) {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }

    void ApplyEngineForce()
    {
        if (accelerationInput > 0)
        {
            accelerationFactor = velocityCap - carRigidbody2D.velocity.magnitude;
        }
        else
        {
            accelerationFactor = velocityCap + carRigidbody2D.velocity.magnitude;
        }
        //create a force
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        //apply that force
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {   
        //update the rotation angle based on input
        rotationAngle -= Mathf.Min(carRigidbody2D.velocity.magnitude,1) * steeringInput * turnFactor * drunkfactor;
        //rotate car 
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x + (float)(.25*drunkfactor * Mathf.Sin(Time.time));
        accelerationInput = inputVector.y;
    }

    void KillOrthogonalVelocity()
    {
        //calculate forward and sideways velocity
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void CheckScore()
    {
        postition = this.gameObject.transform.position;
        score = (int) Mathf.Max(score, postition.magnitude);

        sUI.UpdateScore(score);
    }
}
