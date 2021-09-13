using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ui_beerbottle : MonoBehaviour {
    public BeerInventory inventory;
    public int beerIndex;
    public Image img;
    void Start()
    {
        img = this.gameObject.GetComponent<Image>();
    }
    void Update() {
        img.enabled = inventory.beerInInventory>beerIndex;
    }
}
