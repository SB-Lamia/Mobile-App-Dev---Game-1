using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public CityManager cityManager;
    public TradingSystemManager tradingSystemManager;

    public void DecideLocation()
    {
        if (true)
        {
            cityManager.Pause();
        }
        else if (true)
        {
            tradingSystemManager.Pause();
        }
    }

}
