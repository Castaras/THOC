using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class CatMovement : MonoBehaviour
{

    private Rigidbody rb;
    public int PlayerNumber;
    public float MoveSpeed;
    private String keyUp;
    private String keyDown;
    private String keyLeft;
    private String keyRight;


    // Use this for initialization
    void Start ()
	{
	    rb = GetComponent<Rigidbody>();
        MoveSpeed = 5f;
        if (PlayerNumber == 0)
        {
            keyUp = "up";
            keyDown = "down";
            keyLeft = "left";
            keyRight = "right";
        } 
        if (PlayerNumber == 1)
        {
            keyUp = "w";
            keyDown = "s";
            keyLeft = "a";
            keyRight = "d";
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
        float hori = 0;
        float vert = 0;

        if (Input.GetKey(keyUp))
        {
            vert += 1;
        }
        if (Input.GetKey(keyDown))
        {
            vert += -1;
        }
        if (Input.GetKey(keyLeft))
        {
            hori += -1;
        }
        if (Input.GetKey(keyRight))
        {
            hori += 1;
        }

        if (hori != 0 || vert != 0)
        {
            UpdateMovement(hori, vert);
        }
    }

    public void UpdateMovement(float hori, float vert)
    {
        // print(hori + " + " + vert);

        Vector3 currPosition = transform.position;
        Vector3 temp = new Vector3(Mathf.Round(currPosition[0] / 0.24f), Mathf.Round(currPosition[1] / 0.24f), currPosition[2]);
        Vector3 goalPosition = new Vector3(temp[0] * 0.24f, temp[2], temp[1] * 0.24f);
        Vector3 toPosition = new Vector3((goalPosition[0] - currPosition[0]) * MoveSpeed, 0, (goalPosition[1] - currPosition[1]) * MoveSpeed);

        Vector3 newSpeed = new Vector3(hori * MoveSpeed, 0, vert * MoveSpeed);
        rb.velocity = newSpeed;
        
        
    }
}
