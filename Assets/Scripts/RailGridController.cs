using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class RailGridController : MonoBehaviour
{
    public int cellSize = 1;

    public GameObject straightNS;
    public GameObject straightOW;

    public GameObject curveNE;
    public GameObject curveNW;
    public GameObject curveSE;
    public GameObject curveSW;

    public GameObject spawnTrack;

    public TextAsset testLevel;
    public Rail[,] railGrid;
    public Waypoint SpawnPoint;

    private List<Waypoint> _waypoints;
    private Waypoint spawnWaypoint;


    private void Awake()
    {
        _waypoints = new List<Waypoint>();
        GetRailGridFromFile(testLevel);
        print(_waypoints.Count);
        SpawnPoint = _waypoints[0];
        TrainSpawner trainSpawner = spawnTrack.GetComponent<TrainSpawner>();
        trainSpawner.Spawnpoints[0].AdjecentWaypoints[0] = spawnWaypoint;
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
                Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
                char railChar = line[x];
                Rail rail = GetRail(railChar, pos);
                railGrid[y, x] = rail;
                if (rail != null)
                    _waypoints.AddRange(rail.GetWaypoints());
            }
        }
        WeldRailParts();
    }

    private void WeldRailParts()
    {
        List<Waypoint> allLooseEnds = _waypoints.Where(wp => wp.AdjecentWaypoints.Count < 2).ToList();
        for (int y = 0; y < railGrid.GetLength(0); y++)
        {
            for (int x = 0; x < railGrid.GetLength(1); x++)
            {
                if (railGrid[y, x] != null)
                {
                    var wpts = railGrid[y, x].GetWaypoints();
                    List<Waypoint> looseEnds = wpts.Where(wp => wp.AdjecentWaypoints.Count < 2).ToList();


                    foreach (var le in looseEnds)
                    {
                        // finds the waypoint with the lowest distance to le
                        var wPTemp = allLooseEnds;
                        var rail = le.GetComponentInParent<Rail>();
                        var wpInRail = rail.GetWaypoints();
                        wPTemp.RemoveAll(e => wpInRail.Contains(e));

                        var newWp = wPTemp.OrderBy(w => Vector3.Distance(w.transform.position, le.transform.position)).ToArray()[0];
                        le.AdjecentWaypoints.Add(newWp);
                        newWp.AdjecentWaypoints.Add(le);
                    }
                }
            }
        }
    }

    private static string[] SplitOnNewline(string str)
    {
        return str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }

    private Rail GetRail(char rail, Vector3 pos)
    {
        /*
         * t = top
         * b = bottom
         * l = left
         * r = right
         * - = vertical
         * | = horizontal
         * x = free cell
         * s = spawnPoint
         */

        switch (rail)
        {
            case '-':
                return ((GameObject)Instantiate(straightNS, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case '|':
                return ((GameObject)Instantiate(straightOW, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 't':
                return ((GameObject)Instantiate(curveSW, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 'b':
                return ((GameObject)Instantiate(curveNW, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 'l':
                return ((GameObject)Instantiate(curveSE, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 'r':
                return ((GameObject)Instantiate(curveNE, pos, Quaternion.identity, transform)).GetComponent<Rail>();
            case 's':
                // TODO lagerhallen model
                Rail spawnRail = (Rail)Instantiate(straightNS, pos, Quaternion.identity, transform);
                spawnWaypoint = spawnRail.GetWaypoints()[0];
                return spawnRail;
            case 'x':
                return null;
            default:
                throw new InvalidEnumArgumentException();
        }
    }
}


