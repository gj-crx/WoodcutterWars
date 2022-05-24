using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Types
{
    /// <summary>
    /// refers only to normal units (characters, nut buildings / trees)
    /// </summary>
    public class BasicUnitType
    {
        /// <summary>
        /// 0 - regular unit, 1 - building, 2 - tree
        /// </summary>
        public sbyte UnitClassID = 0;
        public sbyte UnitTypeID = 0;
        public string UnitTypeName = "Unknown unit";
        public float BuildTimeNeeded = 10;
        public float[] ResourcesCostToBuild = new float[3];

        public float MaxHP = 100;
        public float Damage = 10;
        public float Regeneration = 1f;
        public float MoveSpeed = 3;
        public float AttackDelay = 0.5f;
        public float AttackRange = 2.5f;

        public sbyte ObstacleRadius = 1;

        public float ResourcesCarriedMaximum = 20;
        public float[] ResourcesGivenOnKilled = new float[3];

        public BasicUnitType(UnitTypePrefabVariant v)
        {
            UnitClassID = v.UnitClassID;
            UnitTypeID = v.UnitTypeID;
            UnitTypeName = v.UnitTypeName;
            BuildTimeNeeded = v.TrainingTimeNeeded;
            ResourcesCostToBuild = v.ResourcesCostToTrain;

            MaxHP = v.MaxHP;
            Damage = v.Damage;
            Regeneration = v.Regeneration;
            MoveSpeed = v.MoveSpeed;
            AttackDelay = v.AttackDelay;
            AttackRange = v.AttackRange;

            ObstacleRadius = v.ObstacleRadius;

            ResourcesCarriedMaximum = v.ResourcesCarriedMaximum;
            ResourcesGivenOnKilled = v.ResourcesGivenOnKilled;
        }
        public void ApplyType(Unit UnitToApplyStats)
        {

        }
    }
    /// <summary>
    /// used only to define unit types via unity editor
    /// </summary>
    
}
