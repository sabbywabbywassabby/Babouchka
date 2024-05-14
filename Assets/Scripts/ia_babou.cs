using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class ia_babou : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    private bool walking, chasing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset; 
    public string deathScene;
    private Controleur_Bryan control_joueur;

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        player = GameObject.Find("Player_1").transform;
    }
    void Update()
    {
        if(player == null){
            player = GameObject.Find("Player_1").transform;
            control_joueur = player.GetComponent<Controleur_Bryan>();
        }
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                chasing = true;
            }
        }
        if (chasing == true)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            aiAnim.ResetTrigger("walk");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("run");
            float distance = Vector3.Distance(player.position, ai.transform.position);
            if (distance <= catchDistance)
            {
                StartCoroutine(deathRoutine());
                
            }
        }
        if (walking == true)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            aiAnim.ResetTrigger("run");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("walk");
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.ResetTrigger("run");
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("idle");
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }
    IEnumerator deathRoutine()
    {
        control_joueur.stop_moving = true;
        control_joueur.Look(transform.position + Vector3.up);
        
        
        aiAnim.ResetTrigger("run");
        aiAnim.ResetTrigger("walk");
        aiAnim.SetTrigger("idle");
        ai.speed = 0;
        StopCoroutine("stayIdle");
        walking = false;
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        aiAnim.SetTrigger("punch");
        yield return new WaitForSeconds(jumpscareTime);
        control_joueur.anim_control.SetBool("punch",true);
        yield return new WaitForSeconds(2f);
        aiAnim.ResetTrigger("punch");
        aiAnim.SetTrigger("idle");
        StartCoroutine("stayIdle");
        player.gameObject.SetActive(false);
        chasing = false;
    }
}