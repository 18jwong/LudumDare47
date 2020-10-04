using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        originalScale = transform.localScale;
    }

    public void UpdateSize(float s)
    {
        Vector3 v = new Vector3(originalScale.x*s, originalScale.y, originalScale.z*s);
        transform.localScale = v;
    }

    public void UpdateSpeed(float s)
    {
        playerMovement.ChangeSpeed(s);
    }
}
