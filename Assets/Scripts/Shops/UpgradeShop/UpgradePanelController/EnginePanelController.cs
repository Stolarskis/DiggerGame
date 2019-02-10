using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnginePanelController : MonoBehaviour
{
    public Button prefab;
    public playerMovement playerMovement;
    public PlayerInventory playerInventory;
    
    public Text engineNameText;
    public Text engineCostText;
    public Text enginePowerText;

    
    private float buttonSpacingY;
    private int currentSelectedEngine =0;

    private Button[] engineButtons = new Button[10];

    public delegate void NotEnoughMoney();
    public static event NotEnoughMoney NoMoney;

    public delegate void AlreadyOwned();
    public static event AlreadyOwned Owned;

    //public delegate void ButtonsGenerated();
    //public static event ButtonsGenerated EngineButtonsGenerated;

    public void Awake()
    {
        buttonSpacingY = -50f;
        generateButtons();
        //EngineButtonsGenerated();
    }

    public void generateButtons()
    {
        int i = 0;
        float y = prefab.transform.position.y;
        foreach (EngineObject engine in playerMovement.engines)
        {
            engineButtons[i] = Instantiate(prefab);
            engineButtons[i].transform.SetParent(gameObject.transform.GetChild(2));
            engineButtons[i].transform.position = new Vector3 (prefab.transform.position.x,y,prefab.transform.position.z);
            y += -50;
            //C# gets a bit weird with variable references. To fix it I had to use a tempInt inside the for loop.
            int tempInt = i;
            engineButtons[i].onClick.AddListener(delegate { displayEngineInfo(tempInt); });
            engineButtons[i].GetComponentInChildren<Text>().text = engine.name;
            i++;
        }
    }


    public void displayEngineInfo(int engine)
    {
        currentSelectedEngine = engine;
        EngineObject selectedEngine = playerMovement.engines[engine];
        engineNameText.text = selectedEngine.name; 
        engineCostText.text = "$" + selectedEngine.cost.ToString(); 
        enginePowerText.text = "Engine Power: " + selectedEngine.runSpeed.ToString(); 
    }

    public void buyEngine()
    {
        int engineCost = playerMovement.engines[currentSelectedEngine].cost;
        if (currentSelectedEngine == playerMovement.currentEngine)
        {
            Owned();
        }
        else if (playerInventory.getMoney() <=  engineCost){
            NoMoney();
        }
        else
        {
            playerInventory.addToMuny(-engineCost);
            playerMovement.currentEngine = currentSelectedEngine;
        }
    }
}
