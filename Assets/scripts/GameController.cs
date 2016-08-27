using UnityEngine;

public class GameController : MonoBehaviour
{

    private TrainController _trainController;
    private RailGridController _railGrid;

    void Start()
    {
        #region init
        _trainController = new TrainController();
        _railGrid = GetComponent<RailGridController>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

    }
}
