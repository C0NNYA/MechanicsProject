using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class coinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    public GameObject Gate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coin Count:" + coinCount.ToString();

        if(coinCount == 3)
        {
            Destroy(Gate);
        }
    }
}
