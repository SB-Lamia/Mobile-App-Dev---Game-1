using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    //used to decided if the location is a city or a trader.
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
