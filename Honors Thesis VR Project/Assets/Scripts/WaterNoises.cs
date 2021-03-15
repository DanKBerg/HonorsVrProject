using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNoises : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Lake");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
