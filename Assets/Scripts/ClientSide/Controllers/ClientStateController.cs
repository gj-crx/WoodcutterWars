using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClientSideLogic {
    public static class ClientStateController
    {
        public static List<StateClientSide> States = new List<StateClientSide>();



        public static bool StateExistInClient(short StateReferenceID)
        {
             foreach (var state in States)
            {
                if (state.StateID == StateReferenceID)
                {
                    return true;
                }
            }
            return false;
        }
        public static void CreateClientStateRepresentation(State.StateSerializableData data)
        {
            StateClientSide NewState = GameObject.Instantiate(PrefabManager.Singleton.StatePrefab).GetComponent<StateClientSide>();
            data.Apply(NewState);
        }




    }
}
