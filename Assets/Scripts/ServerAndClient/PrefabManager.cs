using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] prefabs_Units = new GameObject[15];
    public GameObject[] prefabs_Buildings = new GameObject[15];
    public GameObject[] prefabs_Trees = new GameObject[15];

    [HideInInspector]
    public GameObject[] prefabs_AllUnits = new GameObject[1000];

    [Header("Client side unit prefabs")]
    public List<GameObject> prefabs_TownhallRaces;

    [Header("Logical prefabs")]
    public GameObject StatePrefab;


    public static PrefabManager Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;
    }
}
