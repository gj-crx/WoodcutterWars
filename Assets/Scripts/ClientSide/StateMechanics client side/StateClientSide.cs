using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClientSideLogic
{
    public class StateClientSide : MonoBehaviour
    {
        public short StateID = 0;
        public sbyte RaceID = 0;
        public string StateName = "Unsyncronized state";
        public int ControllingPlayerID = 0;

        public float[] ResourcesAmount = new float[3];
        public List<BuildingClientSide> Buildings = new List<BuildingClientSide>();
        public BuildingClientSide Townhall;


        public BuildingClientSide GetTrainingBuilding()
        {
            int AttemptsCount = 0;
            while (AttemptsCount < 4)
            {
                int random = Random.Range(0, Buildings.Count);
                if (Buildings[random] != null)
                {
                    return Buildings[random];
                }
                AttemptsCount++;
            }
            return null;
        }
    }
}
