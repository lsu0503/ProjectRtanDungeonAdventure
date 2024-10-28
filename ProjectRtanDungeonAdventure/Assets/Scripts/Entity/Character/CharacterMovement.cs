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
        Move();
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
        moveDir = vector;
    }
}