using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField]
    float red;
    [SerializeField]
    float green;
    [SerializeField]
    float blue;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(red / 255.0f, green / 255.0f, blue / 255.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
