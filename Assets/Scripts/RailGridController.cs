using System;
using System.ComponentModel;
using UnityEngine;

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
                return new SwitchRail(transform, curveSW, pos);
            case 'o':
                return null;
            default:
                throw new InvalidEnumArgumentException();
        }
    }
}