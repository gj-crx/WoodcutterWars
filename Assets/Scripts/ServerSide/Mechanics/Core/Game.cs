using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Game : MonoBehaviour
{
    public sbyte GameID = 0;
    public Map map;
    public UnitsController unitsController;
    public StatesController statesController;
    public Pathfinding pf;



    [Header("Testing variables")]
    public GameObject testobj1;
    public GameObject testobj2;

    public List<Vector2Int> TestWay = new List<Vector2Int>();

    private void Awake()
    {
        
    }
    public void InitializeGame()
    {
        Debug.Log("New game started");
        map = new Map();
        pf = new Pathfinding(map);
        unitsController = new UnitsController();
        statesController = new StatesController();


        map.SetupMap();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            unitsController.AllUnits[0].position += new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            pf.GetWayPath(testobj1.transform.position, testobj2.transform.position);
            TestWay = pf.Way;
        }
        ControlGame();
    }
    private void ControlGame()
    {
        unitsController.ControlUnits();
        unitsController.ControlBuildings();
        statesController.ControlStatesInteractions();
    }
}
