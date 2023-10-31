using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public CityManager cityManager;
    public TradingSystemManager tradingSystemManager;

    public void DecideLocation()
    {
        if (GameManager.instance.CurrentLocation.name == "City(Clone)")
        {
            cityManager.Pause();
        }
        else if (GameManager.instance.CurrentLocation.name == "Trader(Clone)")
        {
            tradingSystemManager.Pause();
        }
    }

}
