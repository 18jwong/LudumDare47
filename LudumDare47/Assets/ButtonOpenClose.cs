using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenClose : MonoBehaviour
{
    [SerializeField] private GameObject objectToTurnOn = null;

    private bool off = true;

    public void Toggle()
    {
        if(off) 
        {
            objectToTurnOn.SetActive(true);
            off = false;
        }
        else
        {
            objectToTurnOn.SetActive(false);
            off = true;
        }
    }
}
