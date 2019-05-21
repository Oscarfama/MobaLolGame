using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : NetworkBehaviour
{
    PlayerMotor motor;
    public LayerMask movementMask;
    Camera cam;
    public Animator animator;

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,movementMask))
            {
                motor.MoveToPoint(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
            }
        }

        if (motor.agent.remainingDistance == 0)
        {
            animator.SetInteger("toDo", 1);
        }
        else
        {
            animator.SetInteger("toDo", 9);
        }
    }
    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CompleteProject.CameraController>().setTarget(gameObject.transform);
    }
}
