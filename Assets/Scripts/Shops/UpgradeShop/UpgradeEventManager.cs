using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEventManager : MonoBehaviour
{
    public delegate void NotEnoughMoney();
    public static event NotEnoughMoney NoMoney;

}
