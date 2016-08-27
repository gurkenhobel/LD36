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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckSwitchHit();
        }
    }

    private void CheckSwitchHit()
    {
        // TODO 
    }
}
