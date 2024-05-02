using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private SpyroInputs spyroInput;
    public GameObject fireBall;

    public float speed = 5f;
    public float chargeSpeed = 10f;
    public bool onGround;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        spyroInput = new SpyroInputs();
        //enable input action
        spyroInput.Enable();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        playerRb.transform.Translate(new Vector3 (moveVector.x, 0, moveVector.y));
    }

    private void FixedUpdate()
    {
       Vector2 moveVector = spyroInput.InGame.Move.ReadValue<Vector2>();
        playerRb.transform.Translate(new Vector3 (moveVector.x, 0, moveVector.y) * speed * Time.deltaTime);

       
       


    }

    //Will make the player jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && onGround)
        {
            playerRb.AddForce(Vector3.up * 8f, ForceMode.Impulse);
            onGround = false;   
        }
    }

    //The player will slowly fall to the ground
    //Player can move as they fall
    //Can only be done when airborne
    public void Glide(InputAction.CallbackContext context)
    {
        if (context.performed && !onGround)
        {
            
        }
    }


    //Player will enter into a charge state upon pressing the input key.
    //Can be done from a standstill w/o walking
    public void Charge(InputAction.CallbackContext context)
    {
        if (context.performed && onGround)
        {
            playerRb.AddForce(Vector3.forward * chargeSpeed, ForceMode.Force);
            Debug.Log("Charging has started");
        }
        else
        {

            Debug.Log("Charging has ended");
        }
    }

    public void FireBall(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Instantiate(fireBall);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }


}
