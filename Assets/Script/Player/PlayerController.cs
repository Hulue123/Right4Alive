using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 inputDirection;
    public PlayerInputControl inputControl;//����inputsystem����

    void Awake()
    {
        inputControl = new PlayerInputControl();//����inputsystemʵ��
        rb=GetComponent<Rigidbody2D>();//��ȡrigidbody2d 

    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }



    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }



    private void FixedUpdate()
    {
        Move();
    }

    public void Move()//�ƶ�����
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, inputDirection.y * speed * Time.deltaTime);
    }































}
