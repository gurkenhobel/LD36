﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class RailGridController : MonoBehaviour
{
    public int width = 20;
    public int height = 10;
    public int cellSize = 10;

    public GameObject straightNS;
    public GameObject straightOW;

    public GameObject curveNE;
    public GameObject curveNW;
    public GameObject curveSE;
    public GameObject curveSW;


    public TextAsset testLevel;

    public Rail[,] railGrid;


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

            for (int x = 0; x < gridWidth; x++)
            {
                Vector2 pos = new Vector2(x * cellSize, y * cellSize);
                char railChar = line[x];
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
                return new Rail(transform, straightNS, pos);
            case '|':
                return new Rail(transform, straightOW, pos);
            case 't':
                return new SwitchRail(transform, curveNE, pos);
            case 'r':
                return new SwitchRail(transform, curveNE, pos);
            case 'o':
                return null;
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
    protected Vector3 position;
    protected RailDirection direction;
    private readonly GameObject cellObject;

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

    public List<Waypoint> GetWaypoints()
    {
        var waypoints = cellObject.GetComponents<Waypoint>();
        return waypoints.ToList();
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

public class Waypoint : MonoBehaviour
{
    public Waypoint AdjecentWaypoint1;
    public Waypoint AdjecentWaypoint2;

}
