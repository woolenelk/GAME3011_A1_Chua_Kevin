using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModeButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    GridManager grid;
    [SerializeField]
    RawImage rawImage;
    [SerializeField]
    List<Texture> images;


    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
    }

    public void OnModeButtonPressed()
    {
        grid.SwitchMode();
        if (grid.mode == Mode.Mine)
        {
            rawImage.texture = images[1];
        }
        else
        {
            rawImage.texture = images[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
