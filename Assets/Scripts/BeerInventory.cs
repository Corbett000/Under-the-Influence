using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerInventory : MonoBehaviour
{
    public int beerInInventory = 45;
    carcontroller controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.GetComponent<carcontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (beerInInventory > 0)
            {
                controller.drunkfactor += .1f;
                beerInInventory -= 1;
                Debug.Log(beerInInventory);
            }
        }
    }
}
