using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScannedEvent: UnityEvent<int, int>
{
}
public class MinedEvent : UnityEvent<int, int>
{
}

public class CubeInfo : MonoBehaviour
{
    public static ScannedEvent OnCubeClickedEvent = new ScannedEvent();
    

    // grid coordinates
    int x;
    int y;
    [SerializeField]
    public bool mined = false;
    [SerializeField]
    int ResourceValue = 1;
    public Renderer renderer;
    public GridManager grid;


    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("X: " + x + "  |  Y: " + y + "  |  Value: " + ResourceValue);
        }
        grid.CubePressed(x, y);
    }

    public void SetX (int _x)
    {
        x = _x;
    }
    public int GetX()
    {
        return x;
    }

    public void SetY(int _y)
    {
        y = _y;
    }
    public int GetY()
    {
        return y;
    }

    public void SetResourceValue(int _val)
    {
        ResourceValue = _val;
    }
    public int GetResourceValue()
    {
        return ResourceValue;
    }

    public void Reveal()
    {
        if (ResourceValue>=100)
        {
            renderer.material.color = new Color(254.0f / 255.0f, 255.0f / 255.0f, 42.0f / 255.0f);
        }
        else if (ResourceValue >= 50)
        {
            renderer.material.color = new Color(230.0f / 255.0f, 109.0f / 255.0f, 2.0f / 255.0f);
        }
        else if (ResourceValue >= 25)
        {
            renderer.material.color = new Color(229.0f / 255.0f, 44.0f / 255.0f, 0.0f / 255.0f);
        }
        else
        {
            renderer.material.color = new Color(0.0f / 255.0f, 128.0f / 255.0f, 0.0f / 255.0f);
        }
    }

    public void Mined()
    {
        mined = true;
        renderer.material.color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
        grid.AddPoints(ResourceValue);
    }

}
