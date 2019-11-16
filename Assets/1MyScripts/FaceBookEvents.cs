using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public static class FaceBookEvents
{
    public static void LogLevelStartedEvent (string weaponChoice) {
        if(!GameObject.Find("GameJustStarted")) 
        {
            return;
        }

        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["WeaponChoice"] = weaponChoice;
        FB.LogAppEvent(
            "Level Started", null, 
            parameters
        );
    }

    public static void LogLevelReachedEvent (int number) {
        if(!GameObject.Find("GameJustStarted")) 
        {
            return;
        }

        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["Number"] = number;
        FB.LogAppEvent(
            "Level Reached", null,
            parameters
        );
    }

    public static void LogEnemyKilledEvent () {
        if(!GameObject.Find("GameJustStarted")) 
        {
            return;
        }

        FB.LogAppEvent(
            "Enemy Killed"
        );
    }

    public static void LogItemPickedUpOrConsumedEvent (string name) {
        if(!GameObject.Find("GameJustStarted")) 
        {
            return;
        }

        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["Name"] = name;
        FB.LogAppEvent(
            "Item Picked Up Or Consumed", null,
            parameters
        );
    }

    public static void LogUpgradePurchasedEvent (string name, int modifierLevel) {
        if(!GameObject.Find("GameJustStarted")) 
        {
            return;
        }

        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["Name"] = name;
        parameters["ModifierLevel"] = modifierLevel;
        FB.LogAppEvent(
            "Upgrade Purchased", null,
            parameters
        );
    }
}
