using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;

public class WeaponSystem : MonoBehaviour
{

    public LayerMask hitWhat;
    public Transform hitParticle;//������Ч
    public Transform firepoint;//ǹ��λ��
    public Transform shootTrail;//��ȡ�ӵ�
    public PlayerInputControl fireController;
    public Transform muzzleFlash;//��ȡǹ�ڻ���
    public Camera cam;
    public Animator anim;
    float gunAnimTimer = 0.5f;
    float gunShootingTimer = 0f;


    private void Awake()
    {
        fireController = new PlayerInputControl();
        anim = GetComponent<Animator>();


    }



    private void Update()
    {
        GunRotate();//ǹת��
        gunShootingTimer -= Time.deltaTime;

        if (Mouse.current.leftButton.isPressed)
        {
            if (gunShootingTimer<=0)
                Shoot();//���
        }

        
        GunAnimatior();






    }

    void MuzzleFlash()//ǹ�ڻ��� 
    {
        Transform flash = Instantiate(muzzleFlash, firepoint.position, firepoint.rotation);
        flash.SetParent(firepoint);
        float randomSize = UnityEngine.Random.Range((float)0.6, (float)0.9);
        flash.localScale = new Vector3(randomSize, randomSize, randomSize);
        Destroy(flash.gameObject, 0.05f);
    }


    void Shoot()//ǹ���
    {

        Vector2 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 firePointPosition = firepoint.position;

        Vector2 shootDirection = mousePosition - firePointPosition;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;


        Debug.Log("shoot");
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, shootDirection, 5, hitWhat);
        if (hit.collider != null)
            ShootEffect(hit);
        else
            ShootEffect(angle);

    }
    
    void ShootEffect(float angle)//δ��������
    {
        Transform trail = Instantiate(shootTrail, firepoint.position, Quaternion.Euler(transform.eulerAngles));
        Destroy(trail.gameObject, 0.05f);
        MuzzleFlash();
        gunShootingTimer = 0.5f;
    }
    void ShootEffect(RaycastHit2D hit)//��������
    {
        Vector3 firepointPosition = firepoint.position;
        Transform trail = Instantiate(shootTrail, firepointPosition, Quaternion.identity);
        LineRenderer lineRenderer = trail.GetComponent<LineRenderer>();//��ȡ��β��Ч
        Vector3 endPosition = new Vector3(hit.point.x, hit.point.y, firepoint.position.z);
       //��ȾЧ��
        lineRenderer.useWorldSpace=true;
        lineRenderer.SetPosition(0, firepointPosition);
        lineRenderer.SetPosition(1, endPosition);
        //����Ч��
        Transform sparks = Instantiate(hitParticle,hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(sparks.gameObject, 0.2f);
        
        Destroy(trail.gameObject, 0.05f);
        MuzzleFlash();
        gunShootingTimer = 0.5f;


    }
    void GunRotate()//ǹ��ת��
    {
        anim.SetBool("isShooting", false);
        Vector2 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 firePointPosition = firepoint.position;

        Vector2 shootDirection = mousePosition - firePointPosition;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);//ǹת��

    }

    void GunAnimatior()//ǹ����������
    {
        gunAnimTimer -= Time.deltaTime;
        
        if (Mouse.current.leftButton.isPressed)
        {
            gunAnimTimer = 0.2f;
        }
        if (gunAnimTimer>=0)
            anim.SetBool("isShooting", true);
        else
            anim.SetBool("isShooting", false);



    }
}






















