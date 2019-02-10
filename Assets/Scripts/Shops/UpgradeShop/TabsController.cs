using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabsController : MonoBehaviour
{
    public Button DrillTabButton;
    public Button EngineTabButton;
    public Button FuelTankTabButton;
    public Button HullTabButton;

    //OnClickEvents
    public delegate void TabClicked();
    public static event TabClicked DrillTabClicked;
    public static event TabClicked EngineTabClicked;
    public static event TabClicked FuelTankTabClicked;
    public static event TabClicked HullTabClicked;

    private void Awake()
    {
        //Upon Tab beinb clicked, disable all current tabs         
        //TODO: might need specific methods to disable current tab
        //In theory, we don't need these as the panel functions in the upgrade canvas controller does this for us.
        /**
         DrillTabButton.onClick.AddListener(() => CloseAllTabs());
         EngineTabButton.onClick.AddListener(() => CloseAllTabs());
         FuelTankTabButton.onClick.AddListener(() => CloseAllTabs());
         HullTabButton.onClick.AddListener(() => CloseAllTabs());
     */
        //Upon tab being clicked, send notification to subscribers to open tab
        DrillTabButton.onClick.AddListener(() => DrillTabClicked());
        EngineTabButton.onClick.AddListener(() => EngineTabClicked());
        FuelTankTabButton.onClick.AddListener(() => FuelTankTabClicked());
        HullTabButton.onClick.AddListener(() => HullTabClicked());
    }


}