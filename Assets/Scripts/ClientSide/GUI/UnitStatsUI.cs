using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
using UnityEngine.UI;

namespace ClientSideLogic
{

    public class UnitStatsUI
    {
        private BasicUnitType UnitType;

        public void FormUnitStatsUI(Transform panel)
        {
            //damage
            Text t = panel.Find("UnitDamage").Find("Text").GetComponent<Text>();
            t.text = UITextFormatter.CutOffNumericalPart(t.text) + UnitType.Damage;
            //HP
            t = panel.Find("UnitHP").Find("Text").GetComponent<Text>();
            t.text = UITextFormatter.CutOffNumericalPart(t.text) + UnitType.MaxHP;
            //Resource costs
            for (int i = 0; i < UnitType.ResourcesCostToBuild.Length; i++)
            {
                t = panel.Find("ResourcesCost" + i).Find("Text").GetComponent<Text>();
                t.text = UITextFormatter.CutOffNumericalPart(t.text) + UnitType.ResourcesCostToBuild[i];
            }

        }
        public UnitStatsUI(BasicUnitType unitType)
        {
            UnitType = unitType;
        }
        public UnitStatsUI(UnitClientSide unit)
        {
            UnitType = unit.type;
        }
    }
}
