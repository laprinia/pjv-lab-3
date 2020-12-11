using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public Healthbar Healthbar;
    public float health = 100f;
    public float viewRadius = 5;
    public float attackRadius = 2;
    public NavMeshAgent agent;
    public float rotationSpeed = 3f;
    private float attackTimeStamp = 0f;
    private float walkTimeStamp = 0f;
    private int attackCoolDown = 3;
    private int walkCoolDown = 5;
    
    private Transform target;
    private Player playerScript;
    public float currentHealth;
    

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.magenta;
        Gizmos.DrawWireSphere(transform.position,viewRadius);
    }

    private void Start()
    {
        
        currentHealth = health;
        Healthbar.SetMaximumHealth(100);
        target = PlayerManager.instance.player.transform;
        playerScript = PlayerManager.instance.player.GetComponent<Player>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= viewRadius)
        {
            
            FaceTarget();
            agent.SetDestination(target.position);
            GetComponent<Animator>().SetTrigger("Walk_Cycle_1");
            if(distance <= attackRadius && Time.time>=attackTimeStamp) 
            {
                attackTimeStamp = Time.time + attackCoolDown;
                GetComponent<Animator>().SetTrigger("Attack_3");
                playerScript.TakeDamage(10);

            }
        }
        else if (Time.time>=walkTimeStamp)
        {
                walkTimeStamp = Time.time + walkCoolDown;            
                GetComponent<Animator>().SetTrigger("Walk_Cycle_1");
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length)
                {
                    currentWaypoint = 0;
                }
                agent.SetDestination(waypoints[currentWaypoint].position);
        }
        
            
            
    }
   
    void RotateN() {
        Quaternion currentRotation = transform.rotation;
        Quaternion wantedRotation = currentRotation * Quaternion.AngleAxis(-90, Vector3.up);
        transform.rotation = Quaternion.Slerp(currentRotation, wantedRotation, Time.deltaTime * rotationSpeed);
    }
    
    
    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

   
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Healthbar.SetHealth((int)currentHealth);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private void OnMouseOver()
    {
        transform.GetChild(3).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        transform.GetChild(3).gameObject.SetActive(false);;
    }

    private IEnumerator Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
