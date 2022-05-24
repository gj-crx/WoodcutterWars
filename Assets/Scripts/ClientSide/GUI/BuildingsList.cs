using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Types;

namespace ClientSideLogic
{
    public static class BuildingsList
    {
        public static void FormUnitsToTrainList(GameObject content, GameObject prefab)
        {
            sbyte ElementsCount = 0;
            foreach (var type in TypesData.UnitTypes)
            {
                GameObject n = GameObject.Instantiate(prefab);
                n.transform.SetParent(content.transform);
                //set Unit name, unit type and icon
                n.transform.Find("IconAndName").Find("UnitName").Find("Text").GetComponent<Text>().text = type.UnitTypeName;
                //set UIElement ID
                n.transform.Find("IconAndName").GetComponent<UIElement>().ID = ElementsCount;

                //set unit stats
                UnitStatsUI UnitStats = new UnitStatsUI(type);
                UnitStats.FormUnitStatsUI(n.transform.Find("UnitStats"));

                ElementsCount++;
            }
        }
    }
}
