using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class CatMovement : MonoBehaviour
{

    private Rigidbody rb;
    public InputDevice Device;
    public int PlayerNumber;
    public float MoveSpeed;


	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
        MoveSpeed = 5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Device != null)
	    {
	        UpdateMovement();
        }

	    if (Device == null)
	    {
	        Device = InputManager.ActiveDevice;
        }
    }

    private void UpdateMovement()
    {
        float hori = Device.Direction.X;
        float vert = Device.Direction.Y;
        // print(hori + " + " + vert);

        Vector3 currPosition = transform.position;
        Vector3 temp = new Vector3(Mathf.Round(currPosition[0] / 0.24f), Mathf.Round(currPosition[1] / 0.24f), currPosition[2]);
        Vector3 goalPosition = new Vector3(temp[0] * 0.24f, temp[1] * 0.24f, temp[2]);
        Vector3 toPosition = new Vector3((goalPosition[0] - currPosition[0]) * MoveSpeed, (goalPosition[1] - currPosition[1]) * MoveSpeed, 0);

        Vector3 newSpeed = new Vector3(hori * MoveSpeed, vert * MoveSpeed, 0);
        rb.velocity = newSpeed;
        
        
    }
}
