using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Types
{
    public class UnitTypePrefabVariant : MonoBehaviour
    {
        public sbyte UnitTypeID = 0;
        public sbyte UnitClassID = 0;
        public string UnitTypeName = "Unknown unit";
        public float TrainingTimeNeeded = 10;
        public float[] ResourcesCostToTrain = new float[3];

        public float MaxHP = 100;
        public float Damage = 10;
        public float Regeneration = 1f;
        public float MoveSpeed = 3;
        public float AttackDelay = 0.5f;
        public float AttackRange = 2.5f;

        public sbyte ObstacleRadius = 1;

        public float ResourcesCarriedMaximum = 20;
        public float[] ResourcesGivenOnKilled = new float[3];
        public BasicUnitType ToBasicType()
        {
            BasicUnitType b = new BasicUnitType(this);

            return b;
        }
        private void LateUpdate()
        {
            Destroy(this);
        }
    }
}
