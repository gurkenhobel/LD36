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

    public void SpawnTrain(Transform spawnpoint)
    {
        var train = MonoBehaviour.Instantiate<GameObject>(TrainPrefab);
        train.transform.SetParent(TrainParent.transform);
        train.transform.position = spawnpoint.position;
        train.transform.rotation = spawnpoint.rotation;
        var trainBehaviour = train.GetComponent<Train>();
        _trains.Add(trainBehaviour);
    }
}
