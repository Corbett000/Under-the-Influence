using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputhandler : MonoBehaviour
{

    //components
    carcontroller carcontroller;
    void Awake()
    {
        carcontroller = GetComponent<carcontroller>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

       carcontroller.SetInputVector(inputVector);
    }
}
