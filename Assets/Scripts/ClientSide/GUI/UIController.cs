using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClientSideLogic
{
    public class UIController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField]
        private Button button_ActionOpenBuildingPanel;
        [Header("Lists content")]
        [SerializeField]
        private GameObject Content_UnitsToBuildList;


        [Header("Panels")]
        [SerializeField]
        public GameObject panel_BuildingAndTraining;

        [Header("Prefabs")]
        [SerializeField]
        public GameObject prefab_UnitStats;

        // [Header("Other")]

        private bool BuildingPanelFormed = false;

        private void Awake()
        {
            //    UIPlayerActions.controller = this;

            //buttons
            button_ActionOpenBuildingPanel.onClick.AddListener(ButtonAction_OpenBuildingPanel);
        }
        public void ButtonAction_OpenBuildingPanel()
        {
            if (BuildingPanelFormed == false)
            {
                BuildingsList.FormUnitsToTrainList(Content_UnitsToBuildList, prefab_UnitStats);
                BuildingPanelFormed = true;
            }
            panel_BuildingAndTraining.SetActive(true);
        }
    }
}
