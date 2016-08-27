using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainController
{
    #region prefabs
    public GameObject TrainPrefab;
    #endregion

    public GameObject TrainParent;

    private List<Train> _trains;

    public TrainController()
    {
        _trains = new List<Train>();
    }

    public void SpawnTrain(int x, int y)
    {
        var train = MonoBehaviour.Instantiate<GameObject>(TrainPrefab);
        train.transform.SetParent(TrainParent.transform);
        _trains.Add(train.GetComponent<Train>());
    }
}
