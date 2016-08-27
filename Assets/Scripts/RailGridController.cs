using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Object = UnityEngine.Object;

public class RailGridController : MonoBehaviour
{
    public int width = 20;
    public int height = 10;
    public int cellSize = 10;

    public GameObject straightRail;
    public GameObject curveLeft;
    public GameObject curveRight;

    public TextAsset testLevel;

    private Rail[,] railGrid;


    private void Start()
    {
        GetRailGridFromFile(testLevel);
    }

    private void GetRailGridFromFile(TextAsset level)
    {
        string[] lines = SplitOnNewline(level.text);
        int gridHeight = lines.Length;
        int gridWidth = lines[0].Length;
        railGrid = new Rail[gridHeight, gridWidth];

        for (int y = 0; y < gridHeight; y++)
        {
            string line = lines[y];
            Debug.Log(line);

            for (int x = 0; x < gridWidth; x++)
            {
                Vector2 pos = new Vector2(x * cellSize, y * cellSize);
                char railChar = line[x];
                Debug.Log(railChar);
                Rail rail = GetRail(railChar, pos);
                railGrid[y, x] = rail;
            }
        }
    }

    private static string[] SplitOnNewline(string str)
    {
        return str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }

    private Rail GetRail(char rail, Vector2 pos)
    {
        switch (rail)
        {
            case '-':
                return new Rail(transform, straightRail, pos);
            case '|':
                return new Rail(transform, straightRail, pos);
            case 'l':
                return new SwitchRail(transform, curveLeft, pos);
            case 'r':
                return new SwitchRail(transform, curveRight, pos);
            default:
                throw new InvalidEnumArgumentException();
        }
    }
}

public enum RailDirection
{
    Left,
    Right,
    Top,
    Bottow
}

public class Rail
{
    protected Vector2 position;
    protected RailDirection direction;
    protected GameObject cellObject;

    public Rail(Transform parent, GameObject prefab, Vector2 pos)
    {
        position = new Vector3(pos.x, 0, pos.y);
        cellObject = Object.Instantiate(prefab, position, Quaternion.identity) as GameObject;
        cellObject.transform.SetParent(parent);
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public List<Vector3> GetWaypoints()
    {
        var waypoints = new List<Vector3>();
        // TODO get waypoints
        return waypoints;
    }
}

public class SwitchRail : Rail
{
    public SwitchRail(Transform parent, GameObject prefab, Vector2 pos) : base(parent, prefab, pos)
    {

    }

    public void RotateSwitch()
    {
        direction += 1 % 4;
    }
}
