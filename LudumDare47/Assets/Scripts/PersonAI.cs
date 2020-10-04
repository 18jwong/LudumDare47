using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAI : MonoBehaviour
{
    [SerializeField] private float randomMultiplier = 1f;
    [SerializeField] private float randomMovementRange = 5f;
    [SerializeField] private float randomMovementTime = 0.1f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("MoveRandomly", 0, randomMovementTime);
    }

    // Update is called once per frame
    private void MoveRandomly()
    {
        rb.AddForce(Random.Range(-randomMovementRange, randomMovementRange)*randomMultiplier, Random.Range(-randomMovementRange/5f, randomMovementRange/5f)*randomMultiplier, Random.Range(-randomMovementRange, randomMovementRange)*randomMultiplier);
    }
}
