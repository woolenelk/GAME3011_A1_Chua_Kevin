using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public enum Mode
{
    Scan,
    Mine
}

struct XY_Coord
{
    public int x, y;
}

public class GridManager : MonoBehaviour
{
    [SerializeField]
    public Mode mode;
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    int SizeX;
    [SerializeField]
    int SizeY;
    [SerializeField]
    float Spacing;
    [SerializeField]
    int numOfMaxResources;
    [SerializeField]
    int points = 0;
    [SerializeField]
    TextMeshProUGUI pointsText;

    [SerializeField]
    int scans = 6;
    [SerializeField]
    TextMeshProUGUI scansText;

    [SerializeField]
    int mines = 3;
    [SerializeField]
    TextMeshProUGUI minesText;


    //List of a list
    List<List<GameObject>> GridObjects = new List<List<GameObject>>();
    List<XY_Coord> ResourceDesposits = new List<XY_Coord>();

    // Start is called before the first frame update
    void Start()
    {
        pointsText.text = "" + points;
        scansText.text = "" + scans;
        minesText.text = "" + mines;
        Vector3 defaultPos = transform.position;
        defaultPos.x -= (SizeX-1) * Spacing / 2.0f;
        defaultPos.y -= (SizeY-1) * Spacing / 2.0f;
        for (int y = 0; y < SizeY; y++)
        {
            List<GameObject> item = new List<GameObject>();
            GridObjects.Add(item);
            for (int x = 0; x < SizeX; x++)
            {
                Vector3 newPos = defaultPos;
                newPos += new Vector3(x * Spacing, y * Spacing, 0);
                GameObject cube = Instantiate(prefab, newPos, Quaternion.identity);
                cube.GetComponent<CubeInfo>().grid = this;
                cube.GetComponent<CubeInfo>().SetX(x);
                cube.GetComponent<CubeInfo>().SetY(y);
                cube.GetComponent<CubeInfo>().SetResourceValue(1);
                GridObjects[y].Add(cube);
            }
        }

        // Generating where the Max Resource Cubes are
        for (int i = 0; i < numOfMaxResources; i++)
        {
            XY_Coord coord = new XY_Coord();
            coord.x = UnityEngine.Random.Range(2, SizeX - 2);
            coord.y = UnityEngine.Random.Range(2, SizeY - 2);
            ResourceDesposits.Add(coord);
            Debug.Log("Max Resource #" + i + "   x: " + coord.x + "   y: " + coord.y);

            for(int row = -2; row < 3; row++)
            {
                for (int col = -2; col < 3; col++)
                {
                    if (GridObjects[coord.y-row][coord.x-col].GetComponent<CubeInfo>().GetResourceValue() < 25)
                    {
                        

                        GridObjects[coord.y - row][coord.x - col].GetComponent<CubeInfo>().SetResourceValue(25);
                        //GridObjects[coord.y - row][coord.x - col].GetComponent<CubeInfo>().renderer.material.color = new Color(229.0f / 255.0f, 44.0f / 255.0f, 0.0f / 255.0f);
                    }
                }
            }
            for (int row = -1; row < 2; row++)
            {
                for (int col = -1; col < 2; col++)
                {
                    if (GridObjects[coord.y - row][coord.x - col].GetComponent<CubeInfo>().GetResourceValue() < 50)
                    {
                        GridObjects[coord.y - row][coord.x - col].GetComponent<CubeInfo>().SetResourceValue(50);
                        //GridObjects[coord.y - row][coord.x - col].GetComponent<CubeInfo>().renderer.material.color = new Color(230.0f / 255.0f, 109.0f / 255.0f, 2.0f / 255.0f);
                    }
                }
            }

            GridObjects[coord.y][coord.x].GetComponent<CubeInfo>().SetResourceValue(100);
            //GridObjects[coord.y][coord.x].GetComponent<CubeInfo>().renderer.material.color = new Color(254.0f / 255.0f, 255.0f / 255.0f, 42.0f / 255.0f);
        }
    }

    public void CubePressed(int x, int y)
    {
        switch (mode)
        {
            case Mode.Scan:
                if (scans <= 0)
                    return;
                for(int i = -1; i <2; i ++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (!(x + j < 0) && !(x + j >= SizeX) && !(y + i < 0) && !(y + i >= SizeY))
                        { GridObjects[y + i][x + j].GetComponent<CubeInfo>().Reveal(); }
                    }
                }
                scans--;
                scansText.text = "" + scans;
                break;
            case Mode.Mine:
                if (mines <= 0)
                    return;
                if (GridObjects[y][x].GetComponent<CubeInfo>().mined)
                    return;
                GridObjects[y][x].GetComponent<CubeInfo>().Mined();
                mines--;
                minesText.text = "" + mines;
                break;
        }
    }


    public void SwitchMode()
    {
        switch (mode)
        {
            case Mode.Scan:
                mode = Mode.Mine;
                break;
            case Mode.Mine:
                mode = Mode.Scan;
                break;
        }
    }

    public int AddPoints(int _points = 0)
    {
        points += _points;
        pointsText.text = "" + points ;
        return points;
    }
}
