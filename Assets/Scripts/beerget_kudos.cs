using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beerget_kudos : MonoBehaviour {
    private Vector2 floatdir;
    // Start is called before the first frame update
    void Start() {
        var randdir = (float)Random.Range(0,3.14159f);
        floatdir = new Vector2(Mathf.Cos(randdir),Mathf.Sin(randdir));
        Destroy(this.gameObject,2);
        // var col = gameObject.GetComponent<Renderer>().material.color;
        // col.a = 0.5f;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(floatdir * Time.deltaTime * 2);
    }
}
