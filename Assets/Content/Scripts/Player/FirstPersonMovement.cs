using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviour
{
    Rigidbody rb;
    PlayerInput input;
    [SerializeField] GameObject terrainCheck;
    [SerializeField] GameObject playerCamera;

    [SerializeField] float jumpVelocity = 8;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float strafeSpeed = 0.5f;

    [SerializeField] bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       input = GetComponent<PlayerInput>();
       if(rb == null) { Destroy(gameObject);Debug.LogError(gameObject.name + "removed, no rigidbody");} //Better safe than sorry!
    }

    // Update is called once per frame
    void Update()
    {
        //Handle camera movement
        if (playerCamera != null) 
        {
            playerCamera.transform.localEulerAngles = new Vector3(input.mouseY,0,0);
        } 
        //Handle player rotation
        gameObject.transform.Rotate(new Vector3(0,input.mouseX,0));
        //Handle player movement
        rb.velocity = transform.forward*moveSpeed*input.moveY + transform.right*strafeSpeed*input.moveX + transform.up * rb.velocity.y + transform.up * ((input.jump&&isGrounded) ? jumpVelocity : 0);
        //Check if grounded
        isGrounded = false;
        Collider[] groundCollisions = Physics.OverlapSphere(terrainCheck.transform.position,0.2f); //Get everything below player
        foreach(Collider i in groundCollisions)
        {
            GameObject gObj = i.gameObject;
            if (gObj.GetComponent<Tag_Ground>()) //Check if any object below the player is terrain. (jumpable)
            {
                isGrounded = true;
            }
        }
    }
}
