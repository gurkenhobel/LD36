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

    public Waypoint SpawnPoint;

    private List<Waypoint> _waypoints;


    private void Start()
    {
        _waypoints = new List<Waypoint>();
        GetRailGridFromFile(testLevel);
        SpawnPoint = _waypoints[UnityEngine.Random.Range(0, _waypoints.Count)];
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
                        // finds the waypoint with the lowest distance to le
                        le.AdjecentWaypoint1 = le.AdjecentWaypoint1 ??  _waypoints.OrderBy(w => Vector3.Distance(w.transform.position, le.transform.position)).ToArray()[0];
                        le.AdjecentWaypoint2 = le.AdjecentWaypoint2 ?? _waypoints.OrderBy(w => Vector3.Distance(w.transform.position, le.transform.position)).ToArray()[0];
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
                return ((GameObject) Instantiate( straightNS,pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case '|':
                return ((GameObject)Instantiate(straightOW, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 't':
                return ((GameObject)Instantiate(curveNE, pos, Quaternion.identity, transform)).GetComponent<Rail>(); 
            case 'b':
                return ((GameObject)Instantiate(curveNW, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 'l':
                return ((GameObject)Instantiate(curveSE, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 'r':
                return ((GameObject)Instantiate(curveNE, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 'o':
                return null;
            default:
                throw new InvalidEnumArgumentException();
        }
    }
}


