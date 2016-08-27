using System;
using System.ComponentModel;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class RailGridController : MonoBehaviour
{
    public int width = 20;
    public int height = 10;
    public int cellSize = 1;

    public GameObject straightNS;
    public GameObject straightOW;

    public GameObject curveNE;
    public GameObject curveNW;
    public GameObject curveSE;
    public GameObject curveSW;


    public TextAsset testLevel;

    public Rail[,] railGrid;

    private List<Waypoint> _waypoints;


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
                _waypoints.AddRange(rail.GetWaypoints());
            }
        }

        
    }

    private void WeldRailParts()
    {
        for (int y = 0; y < railGrid.GetLength(0); y++)
        {
            for (int x = 0; x < railGrid.GetLength(1); x++)
            {
                var wpts = railGrid[y, x].GetWaypoints();
                var looseEnds = wpts.Where(wp => wp.AdjecentWaypoint1 == null || wp.AdjecentWaypoint2 == null);

                foreach (var le in looseEnds)
                {
                    if (!le.IsSwitchRail)
                    {
                        // finds the waypoint with the lowest distance to le
                        le.AdjecentWaypoint1 = le.AdjecentWaypoint1 ??  _waypoints.OrderBy(w => Vector3.Distance(w.transform.position, le.transform.position)).ToArray()[0];
                        le.AdjecentWaypoint2 = le.AdjecentWaypoint2 ?? _waypoints.OrderBy(w => Vector3.Distance(w.transform.position, le.transform.position)).ToArray()[0];
                    }
                    else
                    {

                    }
                }

            }
        }
    }

    private static string[] SplitOnNewline(string str)
    {
        return str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }

    private Rail GetRail(char rail, Vector2 pos)
    {
        /*
         * t = top
         * b = bottom
         * l = left
         * r = right
         * - = vertical
         * | = horizontal
         */

        switch (rail)
        {
            case '-':
                return new Rail(transform, straightNS, pos);
            case '|':
                return new Rail(transform, straightOW, pos);
            case 't':
                return new SwitchRail(transform, curveNE, pos);
            case 'b':
                return new SwitchRail(transform, curveNW, pos);
            case 'l':
                return new SwitchRail(transform, curveSE, pos);
            case 'r':
                return new SwitchRail(transform, curveNE, pos);
            case 'o':
                return null;
            default:
                throw new InvalidEnumArgumentException();
        }
    }
}

public class Waypoint : MonoBehaviour
{
    public bool IsSwitchRail;

    public Waypoint AdjecentWaypoint1;

    public Waypoint AdjecentWaypoint2;
    
}
