using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    public sbyte ID = 0;
    void Start()
    {
        
    }
    public void BuildOrderButtonClicked()
    {
        UIPlayerActions.LocalPlayer.TrainUnitOrderServerRpc(UIPlayerActions.LocalPlayer.ControlledState.GetTrainingBuilding().IDInBuildingsPool, ID);
    }
}
