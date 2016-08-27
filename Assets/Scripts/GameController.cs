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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
