using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    private TrainController _trainController;
    private RailGrid _railGrid;
	
	void Start ()
    {
        #region init
        _trainController = new TrainController();
        #endregion
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
