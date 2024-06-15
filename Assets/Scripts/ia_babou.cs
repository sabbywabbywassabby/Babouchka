using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;
using Mirror;
using UnityEngine.SceneManagement;


public class ia_babou : NetworkBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public GameObject spawn_pose;
    Vector3 dest;    
    int randNum;
    Transform currentDest;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public Transform player;
    public Transform player2;
    public GameObject screamer_img;
    public GameObject Game_over;
    public GameObject un;
    public GameObject deux;
    public GameObject trois;
    public AudioClip Shout;

    private AudioSource audioSource;
    private Transform current_player;
    private Controleur_Bryan control_joueur;
    private bool walking, chasing;   
    private PlayerSetup playerSetup;
    private PlayerSetup playerSetup_2;
    private Controleur_Bryan current_controleur;
    private PlayerSetup current_setup;
    private int indice;



    void Start()
    {
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        SetPlayer(GameObject.Find("Player_1"));
        indice = 4;
        audioSource = GetComponent<AudioSource>();
    }

    private void SetPlayer(GameObject p)
    {
        player = p.transform;
        control_joueur = player.GetComponent<Controleur_Bryan>();
        playerSetup = player.GetComponent<PlayerSetup>();
    }
    private void SetPlayer2(GameObject p)
    {
        player2 = p.transform;
        
        playerSetup_2 = player2.GetComponent<PlayerSetup>();
    }

    void Update()
    {
        if(player2 == null && GameObject.Find("Player_2") != null)
        {
            SetPlayer2(GameObject.Find("Player_2"));
        }
        
        if (chasing == true)
        {
            StopCoroutine(stayIdle());
            dest = current_player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            aiAnim.ResetTrigger("walk");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("run");
            float distance = Vector3.Distance(current_player.position, ai.transform.position);
            if (distance <= catchDistance)
            {
                StopAllCoroutines();
                StartCoroutine(deathRoutine());
            }
        }
        else if (walking == true)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
            {
                if (hit.collider.gameObject.name == "Player_1")
                {
                    current_player = player;
                    walking = false;
                    StopCoroutine("stayIdle");
                    StopCoroutine("chaseRoutine");
                    StartCoroutine("chaseRoutine");
                    chasing = true;
                }
            }
            Vector3 direction_2 = (player2.position - transform.position).normalized;
            RaycastHit hit_2;
            if (Physics.Raycast(transform.position + rayCastOffset, direction_2, out hit_2, sightDistance))
            {
                if (hit_2.collider.gameObject.name == "Player_2")
                {
                    current_player = player2;
                    
                    walking = false;
                    StopCoroutine("stayIdle");
                    StopCoroutine("chaseRoutine");
                    StartCoroutine("chaseRoutine");
                    chasing = true;
                }
            }
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
        if(current_player == player)
        {
            current_controleur = control_joueur;
            current_setup = playerSetup;
            current_controleur.stop_moving = true;
            // le joueur jette l'objet si il en a un.
            if (current_controleur.key_on_player != null)
            {
                current_controleur.Drop();
            }
        }
        else
        {
            
            current_setup = playerSetup_2;
        }
        
        chasing = false;

        aiAnim.ResetTrigger("run");
        aiAnim.ResetTrigger("walk");
        aiAnim.SetTrigger("idle");
        ai.speed = 0;
        StopCoroutine("stayIdle");
        walking = false;


        //afficher le screamer et le son
        if(current_player == player) 
        {
            //son
            audioSource.clip = Shout;
            audioSource.Play();

            screamer_img.SetActive(true);
            // Attendre la fin du screamer
            yield return new WaitForSeconds(2f);
            screamer_img.SetActive(false);
            indice--;
            if (indice == 0)
            {
                Game_over.SetActive(true);
                player2.GetComponent<Respawn>().lives = 0;
                player2.GetComponent<Respawn>().Respaw(spawn_pose.transform);
            }
            if(indice == 1)
            {
                un.SetActive(true);
            }
            if(indice == 2)
            {
                deux.SetActive(true);
            }
            if(indice == 3)
            {
                trois.SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            un.SetActive(false);
            deux.SetActive(false);
            trois.SetActive(false);

            player.transform.position = spawn_pose.gameObject.transform.position;
            current_controleur.stop_moving = false;
        }
        else
        {
            current_player.GetComponent<Respawn>().Respaw(spawn_pose.transform);
            if (player2.GetComponent<Respawn>().lives == 0)
            {
                Game_over.SetActive(true);
            }
        }
   
        aiAnim.ResetTrigger("punch");
        aiAnim.SetTrigger("idle");
        StartCoroutine("stayIdle"); 
    }
}