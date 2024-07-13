using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    #region Private Members

    private Animator _animator;

    private CharacterController _characterController;

    private float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private Rigidbody rbPlayer;

    private AvatarSetUp avatar;

    private Data myData;

    #endregion

    #region Public Members

    public float Speed = 5.0f;

    public float JumpForce = 2.5f;

    public float RotationSpeed = 240.0f;

    public float JumpSpeed = 7.0f;

    public PhotonView view;

    #endregion

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        rbPlayer = GetComponent<Rigidbody>();
        avatar = GetComponent<AvatarSetUp>();

        if (view.IsMine)
        {
            avatar.SetAvatar(PlayerData.instance.data);
            SyncAvatar(PlayerData.instance);
        }
    }

    public void SyncAvatar(PlayerData data)
    {
        string syncString = data.AvatarToString();
        view.RPC("RPC_SyncAvatar", RpcTarget.OthersBuffered, syncString);
    }

    [PunRPC]
    void RPC_SyncAvatar(string data)
    {
        myData = JsonUtility.FromJson<Data>(data);
        avatar = GetComponent<AvatarSetUp>();
        avatar.SetAvatar(myData);
    }

    public bool mIsControlEnabled = true;

    public void EnableControl()
    {
        mIsControlEnabled = true;
    }

    public void DisableControl()
    {
        mIsControlEnabled = false;
    }

    private Vector3 mExternalMovement = Vector3.zero;

    public Vector3 ExternalMovement
    {
        set
        {
            mExternalMovement = value;
        }
    }

    void LateUpdate()
    {
        if (mExternalMovement != Vector3.zero)
        {
            _characterController.Move(mExternalMovement);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsControlEnabled && view.IsMine)
        {
            // Get Input for axis
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();


            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

            if (_characterController.isGrounded || mExternalMovement != Vector3.zero)
            {
                _moveDirection = transform.forward * move.magnitude;

                _moveDirection *= Speed;

                if (Input.GetButton("Jump"))
                {
                    _animator.SetBool("is_in_air", true);
                    _moveDirection.y = JumpSpeed;

                }
                else
                {
                    _animator.SetBool("is_in_air", false);
                    _animator.SetBool("run", move.magnitude > 0);
                }
            }
            else
            {
                Gravity = 20.0f;
            }


            _moveDirection.y -= Gravity * Time.deltaTime;

            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
}
