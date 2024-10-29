﻿using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    protected CharacterInfo characterInfo;
    protected CharacterController characterController;
    protected Rigidbody rigid;

    protected Vector2 moveDir;

    private void Awake()
    {
        characterInfo = GetComponent<CharacterInfo>();
        characterController = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        characterController.OnMoveEvent += GetMoveDir;
    }

    protected virtual void FixedUpdate()
    {
        if(characterInfo.isMovable)
            Move();

        if (transform.position.y < 0)
        {
            characterInfo.GetDamage(25);
            transform.position = new Vector3(UnityEngine.Random.Range(-35.0f, 35.0f), 8.5f, UnityEngine.Random.Range(-35.0f, 35.0f));
        }
    }

    protected virtual void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;
        float dirSpeed = (0.7f + (moveDir.y * 0.3f));
        dir *= characterInfo.moveSpeed * dirSpeed;
        dir.y = rigid.velocity.y;

        rigid.velocity = dir;
    }

    protected virtual void GetMoveDir(Vector2 vector)
    {
        if (characterInfo.isMovable)
            moveDir = vector;

        else
            moveDir = Vector3.zero;
    }
}
