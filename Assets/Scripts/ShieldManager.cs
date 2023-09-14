using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public GameObject shield;
    public int shieldHealth = 50;
    public bool shieldEnabled;

    void Update() 
    {
        ControlShield();        
    }

    void ControlShield()
    {
        if (shieldEnabled)
            shield.gameObject.SetActive(true);
        else
            shield.gameObject.SetActive(false);
    }
}
