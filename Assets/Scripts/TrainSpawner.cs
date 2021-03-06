﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    #region prefabs
    public GameObject Train;
    public GameObject Trailer;
    #endregion

    public RailGridController Grid;
    public List<Waypoint> Spawnpoints;

    private const int MAX_TRAIN_LENGTH = 5;


    public Train SpawnTrain(int length)
    {
        if (length - 1 > MAX_TRAIN_LENGTH)
            throw new ArgumentOutOfRangeException();

        var train = ((GameObject)Instantiate(Train, Spawnpoints[0].transform.position, Spawnpoints[0].transform.rotation, Grid.transform)).GetComponent<Train>();

        for (int i = 0; i < length - 2; i++)
        {
            train.AddTrailer(Spawnpoints[i], Spawnpoints[i + 1]);
        }

        return train;
    }
}
