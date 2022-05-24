using Unity.Netcode;
using UnityEngine;
using Types;

namespace ClientSideLogic
{
    public class UnitClientSide : NetworkBehaviour
    { //represent a basic game object (building, character, tree)
        [Header("Actual versions of UnitType variable that by default same, but can be changed in game")]
        public float a_MaxHP = 100;
        public float a_Damage = 10;
        public float a_Regeneration = 1f;
        public float a_MoveSpeed = 3;
        public float a_AttackDelay = 0.5f;
        public float a_AttackRange = 2.5f;

        public float a_ResourcesCarriedMaximum = 20;
        public float[] a_ResourcesGivenOnKilled = new float[3];
        [Header("Synced variables")]
        public float currentHP = 100;
        public float[] ResourcesCarried = new float[3];


        public int ID = -1;

        public BasicUnitType type;
        public State state;

        private Vector3 LatestPosition;

        void Awake()
        {
            
        }
        private void Start()
        {
            ClientUnitController.AddUnitToList(this, type.UnitClassID);
            LatestPosition = transform.position;
        }

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, LatestPosition, Time.deltaTime);
        }
        public void Death(UnitClientSide Killer = null)
        {

            Destroy(gameObject);
        }
        private int GetUnitType()
        {
            if (gameObject.tag == "Tree")
            {
                return 0;
            }
            try
            {
                if (GetComponent<BuildingClientSide>() != null)
                {
                    return 2;
                }
            }
            catch { }
            return 1;
        }
        /// <summary>
        /// Creation of new client sided unit
        /// </summary>
        public void ApplyRecievedData(Unit.UnitSerializableData data)
        {
            ID = data.UnitObjectID;
            currentHP = data.CurrentHP;
            type = TypesData.UnitTypes[data.UnitTypeID];
            LatestPosition = data.position; //temporal solution
        }

    }
}
