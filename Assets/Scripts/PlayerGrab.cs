using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private PlayerController player;

    public bool isConnected = false;
    public float holdSpeed = 10f;
    public GameObject objectToHold;
    public Transform holdPos;


    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && player.mIsControlEnabled)
        {
            if(isConnected == true)
            {
                isConnected = false;
                objectToHold = null;
            }
            if(objectToHold != null && isConnected == false)
            {
                isConnected = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if(objectToHold == null || isConnected == false)
            return;

        objectToHold.GetComponent<Rigidbody>().velocity = holdSpeed * (holdPos.position - objectToHold.transform.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Objects"))
        {
            objectToHold = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        objectToHold = null;
        isConnected = false;
    }
}
