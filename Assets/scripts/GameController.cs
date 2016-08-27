using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    public event EventHandler OnLoadComplete;

    private TrainController _trainController;
	
	void Start ()
    {
        #region init
        _trainController = new TrainController();
        #endregion


        if (OnLoadComplete != null)
            OnLoadComplete(this, null);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
