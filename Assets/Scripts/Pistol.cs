﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    public Text munitionText;
    public Animator animator;
    public Camera camera;
    public float rifleDamage = 30f;
    public float snipeDamage = 50f;
    public float range = 100f;
    private float timeStamp = 0f;
    private bool isReloading = false;
    private int coolDown = 10;
    private const int MAX_POSSIBLE_MUNITION = 999;
    public int maximumMunition = 10;
    public int currentMunition;
    public int loadedMax=5;

    private void Start()
    {
        currentMunition = loadedMax;
        maximumMunition = maximumMunition - loadedMax;
    }

    private void Update()
    {
        munitionText.text = currentMunition + "|" + maximumMunition;
        if (Input.GetKeyDown(KeyCode.G))
        {
            
            PickUp();
        }
        if (maximumMunition<= 0 && currentMunition<=0) return;
        if (isReloading) return;
        if (currentMunition == 0 )
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && maximumMunition>=5)
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            
            Shoot(rifleDamage);
        }


        if (Input.GetButtonDown("Fire2") && Time.time >= timeStamp)
        {
            timeStamp = Time.time + coolDown;
            Shoot(snipeDamage);
        }
    }

    private void Shoot(float damage)
    {
        
        StartCoroutine(ShootingCoroutine());
        currentMunition--;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range) &&
            hit.transform.tag.Equals("Enemy"))
        {
            Enemy hitEnemy = hit.transform.GetComponent<Enemy>();
            if (hitEnemy != null)
            {
                
                hitEnemy.TakeDamage(damage);
            }
        }
    }

    private void PickUp()
    {
        RaycastHit hit;
        
        
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 5f) && hit.transform.tag.Equals("Ammo"))
        {
            
            maximumMunition += 10;
            Destroy(hit.transform.gameObject);
        }
    }

    IEnumerator ShootingCoroutine()
    {
        animator.SetBool("isShooting", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isShooting", false);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2);
        
            currentMunition+=loadedMax;
            maximumMunition -= loadedMax;
        
        isReloading = false;
    }
}