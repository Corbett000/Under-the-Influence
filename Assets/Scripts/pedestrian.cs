using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedestrian : MonoBehaviour {
    public Sprite[] skins;
    public GameObject hitsprite;
    // Rigidbody2D carRigidbody2D;
    CircleCollider2D collider;
    SpriteRenderer spriteRenderer;
    SpriteRenderer subSpriteRenderer;
    void Awake() {
        // carRigidbody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        subSpriteRenderer = hitsprite.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled=true;
        subSpriteRenderer.enabled=false;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        spriteRenderer.enabled=false;
        subSpriteRenderer.enabled=true;
        Destroy(collider);
        Destroy(this);
    }
    void Start() {
        // spriteRenderer.sprite = skins[Random.Range(0,skins.Length)];
    }
    void Update() {
        // carRigidbody2D.velocity = transform.up*2;
    }
}
