using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] private List<GameObject> units;
    private GameObject unit;
    private int pointer = -1; // Pointer to iterate through the units list

    [SerializeField] GameObject indicator; // used to indicate which grid is selected
    
    [SerializeField] float offset;  // offset for Y-axis the unit will spawn on

    List<Vector3> filledCells;
    #region Unity Methods
    private void Awake()
    {
        instance = this;
        grid = gridLayout.GetComponent<Grid>();
        filledCells = new List<Vector3>();
    }
    private void Update()
    {
        if (GameManager.gameState.Equals(GameManager.GameState.TeamSelection))
        {
            DisplayUnit();
            UnitSelection();
        }
        else
        {
            if (unit != null)
            {
                unit.GetComponent<UnitController>().DestroyUnit();
            }
            
        }
        
    }
    #endregion

   
    #region Grid Placement Methods
    private void DisplayUnit()
    {
        indicator.transform.position = WorldToGridPos(MouseWorldPosition());
        if (unit != null)
        {
            Vector3 pos = WorldToGridPos(MouseWorldPosition());
            unit.transform.position = new Vector3(pos.x, pos.y + offset, pos.z);
        }
    }
    private void UnitSelection()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ChooseUnit();
        }
        if (Input.GetMouseButtonDown(0) && unit != null)
        {
            PlaceUnit();
        }
    }
    #endregion


    #region Grid To World Position Methods
    public static Vector3 MouseWorldPosition() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
        
    }
    public Vector3 WorldToGridPos(Vector3 pos)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(pos);
        pos = grid.GetCellCenterWorld(cellPos);
        return pos;
    }
    #endregion

    #region Unit Placement Methods
    public void PlaceUnit()
    {
        Vector3 pos = WorldToGridPos(MouseWorldPosition());
        foreach(Vector3 cell in filledCells)
        {
            if (cell.Equals(pos))
            {
                Debug.Log("Cell is filled");
                return;
            }
        }
        Instantiate(unit, new Vector3(pos.x, pos.y + offset, pos.z), Quaternion.identity);
        filledCells.Add(pos);
    }
    private void ChooseUnit()
    {
        if (unit != null)
        {
            unit.GetComponent<UnitController>().DestroyUnit();
        }
        if (pointer >= units.Count-1)
        {
            pointer = -1;
        }
        pointer++;
        unit = Instantiate(units[pointer], transform.position, Quaternion.identity);
    }
    #endregion
}
