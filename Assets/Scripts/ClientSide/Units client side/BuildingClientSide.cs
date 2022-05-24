using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

namespace ClientSideLogic
{

    public class BuildingClientSide : MonoBehaviour
    {
        public int IDInBuildingsPool = 0;
        public UnitClientSide unit;

        public Queue<int> UnitTrainingQueue = new Queue<int>();

        public sbyte BuildingTypeID = 0;

        private void Awake()
        {
            unit = GetComponent<UnitClientSide>();
        }

    }
}
