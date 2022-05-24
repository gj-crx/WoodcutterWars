using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
namespace ClientSideLogic
{

    public class ClientGameController : MonoBehaviour
    {

        [HideInInspector]
        public static ClientGameController Singleton;
        

        
        public GameObject prefab_State;

        public List<StateClientSide> States = new List<StateClientSide>();

        [Header("Other")]
        public Vector3 MapCenter = new Vector3(0, 0, 0);
        public int testradius = 10;

        private bool IsInitialized = false;

        private void Awake()
        {
            Singleton = this;
        }
        private void Start()
        {

        }
        private void Update()
        {
            DebugActions();
        }
        public void InitializeClient()
        {
            if (IsInitialized == false)
            {
                IsInitialized = true;
            }
            else
            {
                return;
            }
            Singleton = this;
            //client side types loading
            TypesData.LoadAllTypes(PrefabManager.Singleton.prefabs_Units, PrefabManager.Singleton.prefabs_Buildings, PrefabManager.Singleton.prefabs_Trees);
        }
        private void DebugActions(GameObject testobj = null, GameObject testobj2 = null)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                UIPlayerActions.LocalPlayer.OrderUnitToMoveServerRpc(ClientUnitController.AllUnits[0].ID, ClientUnitController.AllUnits[0].transform.position + new Vector3(10, 0, 0));
            }
        }

        
        
        
    }
}
