using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour {
    
    public Sprite[] carSkins;
    Rigidbody2D carRigidbody2D;
    SpriteRenderer carSpriteRenderer;
    void Awake() {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        carSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(this);
    }

    void Start() {
        carSpriteRenderer.sprite = carSkins[Random.Range(0,carSkins.Length)];
    }
    void Update() {
        carRigidbody2D.velocity = transform.up*2;
    }
}
