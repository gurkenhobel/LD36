using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainController
{
    #region prefabs
    public Train TrainPrefab;
    #endregion

    private List<Train> _trains;

    public TrainController()
    {

    }

    public void SpawnTrain(int x, int y)
    {
        var train = MonoBehaviour.Instantiate<Train>(TrainPrefab);
        _trains.Add(train);
    }
}
