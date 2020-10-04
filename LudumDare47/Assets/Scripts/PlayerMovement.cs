using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float verticalSpeed = 1f;
    [SerializeField] private float verticalHeight = 1f;
    [SerializeField] private AudioSource moveUpSound = null;
    [SerializeField] private AudioSource moveDownSound = null;

    private GameController gameController;

    private Rigidbody rb;
    private bool up = false;
    private Vector3 originalPosition;  
    private float originalSpeed;  

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;

        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        verticalHeight = verticalHeight + transform.position.y;
        originalSpeed = movementSpeed;

        StartCoroutine("HeightAdjust");
    }

    void Update()
    {
        if(gameController.GetCurrentSeconds() <= 0) return; // game over

        if(Input.GetKeyDown("e"))
        {   
            up = !up;
            if(!up) moveDownSound.Play();
            if(up) moveUpSound.Play();
        }
    }

    void FixedUpdate()
    {
        if(gameController.GetCurrentSeconds() <= 0) return; // game over

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement*movementSpeed);
    }

    public void ChangeSpeed(float s)
    {
        movementSpeed = originalSpeed * s;
    }

    private IEnumerator HeightAdjust()
    {
        while(true)
        {
            if(up && transform.position.y <= verticalHeight-0.1)
            {
                rb.velocity = new Vector3(rb.velocity.x, verticalSpeed, rb.velocity.z);
            }
            else if(!up && transform.position.y >= originalPosition.y+0.1)
            {
                rb.velocity = new Vector3(rb.velocity.x, -verticalSpeed, rb.velocity.z);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            yield return new WaitForSeconds(.025f);
        }
    }
}
