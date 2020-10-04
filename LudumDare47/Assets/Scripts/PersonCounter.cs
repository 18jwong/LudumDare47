﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCounter : MonoBehaviour
{
    [SerializeField] private Material originalMaterial = null;
    [SerializeField] private Material collectedMaterial = null;

    private GameController gameController;

    void Start()
    {
        gameController = GameController.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "collectable")
        {
            gameController.AddToScore(1);
            other.GetComponent<MeshRenderer>().material = collectedMaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "collectable")
        {
            gameController.AddToScore(-1);
            other.GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }
}
