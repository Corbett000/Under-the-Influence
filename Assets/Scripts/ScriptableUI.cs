using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptableUI : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        this.score.text = "Score: " + score;
    }
    
}
