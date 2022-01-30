using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Main AI Script for enemies


[RequireComponent(typeof(NavMeshAgent))]
public class WoofersAI : MonoBehaviour
{
    [Tooltip("A list of Transforms from the scene. " +
        "The AI's default behavior is to make its way from point to point as a sort of patrol. " +
        "The points that the AI tries to get to are listed here.")]
    [SerializeField]
    private LoopingList<Transform> patrolPoints;


    [Tooltip("How close does the AI need to be to a patrol point " +
        "to move on to the next one?")]
    [SerializeField]
    private float goodEnough;

    [Tooltip("Movement speed while patroling")]
    [SerializeField]
    private float patrolSpeed;


    //Player detection vars
    private bool canSeePlayer = false;
    private bool playerDetected = false;
    private bool playerDetectedLastFrame = false;
    [Tooltip("What layer is the player on?")]
    [SerializeField]
    private LayerMask playerDetectionMask;
    [Tooltip("What layers can block the view of the AI?")]
    [SerializeField]
    private LayerMask visionObstructionMask;
    [Tooltip("Maximum distance the AI can detect the player from "+
        "if the player is in the AI's field of view")]
    [SerializeField]
    private float fovDetectionDistance;
    [Tooltip("The field of view angle of the AI's vision")]
    [SerializeField]
    [Range(0, 360)]
    private float fovDetectionAngle;
    [Tooltip("How close does the player need to be to automatically alert the AI, even if the AI isn't looking?")]
    [SerializeField]
    private float noFOVDetectionDistance;
    [Tooltip("Once the AI detects the player, how far away does the player need to be "+
        "to have the AI return to patroling?")]
    [SerializeField]
    private float dropDetectionDistance;
    [SerializeField]
    private GameObject attackHolder;
    private AttackType attack;

    private AudioSource audio;

    //Sound Effects
    [SerializeField]
    private AudioClip playerDetectedSound;
    [SerializeField]
    private AudioClip patrolSound;
    [SerializeField]
    private float timeBetweenPatrolSoundPlaying;
    private float timeSinceLastPatrolSound = 0;
    
    //The NavMesh Agent attached to the AI
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolPoints.resetCurrentIndex();
        attack = attackHolder.GetComponent<AttackType>();
        audio = GetComponentInParent<AudioSource>();

        StartCoroutine(FOVRoutine());
        StartCoroutine(PlayerDetectedRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetected)
        {
            if (playerDetectedLastFrame != playerDetected)
            {
                //if this is the first frame of the player being detected
                //play audio
                audio.clip = playerDetectedSound;
                audio.Play();
            }
            agent.ResetPath();
            attack.Attack(gameObject);
        }
        else
        {
            Patrol();

            //play audio every so often
            if (timeSinceLastPatrolSound >= timeBetweenPatrolSoundPlaying)
            {
                AudioHelper.PlayIfNotPlaying(audio, patrolSound);
                timeSinceLastPatrolSound = 0;

            }
        }

        playerDetectedLastFrame = playerDetected;
        timeSinceLastPatrolSound += Time.deltaTime;
    }

    //damage player if they touch the AI
    private void OnTriggerStay(Collider other)
    {
        //check if other is player. If so, do damage
        if (other.tag.Equals("Player"))
        {
            attack.DoDamage(other.gameObject);
        }
    }

    void Patrol()
    {
        //enforce patrol speed
        agent.speed = patrolSpeed;

        if (patrolPoints.list.Count <= 0)
        {
            Debug.LogWarning("No patrol points exist in patrolPoint list");
        }

        //distance to objective
        float distance = Vector3.Distance(patrolPoints.Current().position, transform.position);

        if (distance <= goodEnough)
        {
            patrolPoints.Next();
        }

        agent.SetDestination(patrolPoints.Current().position);
    }


    //Coroutines
    private IEnumerator PlayerDetectedRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            updatePlayerDetected();
        }
    }

    void updatePlayerDetected()
    {
        if (canSeePlayer)
        {
            playerDetected = true;
            return;
        }

        //if the player is too close, detect them
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);
        if (distance <= noFOVDetectionDistance)
        {
            playerDetected = true;

            return;
        }
        //If the player is far enough away, drop detection
        else if (distance >= dropDetectionDistance)
        {
            playerDetected = false;
            attack.ResetTimeSinceLastAttack();
            return;
        }
    }



    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            CheckFOV();
        }
    }

    //Check the FOV 
    private void CheckFOV()
    {
        //Draw debugger rays to show fov
        Debug.DrawRay(transform.position, Vector3.SlerpUnclamped(transform.forward, transform.right, fovDetectionAngle/90) * fovDetectionDistance);
        Debug.DrawRay(transform.position, Vector3.SlerpUnclamped(transform.forward, -1 * transform.right, fovDetectionAngle / 90) * fovDetectionDistance);

        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, fovDetectionDistance, playerDetectionMask);

        //Only player is on the layer denoted by playerDetectionMask, so only one object can be found.
        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;

            //get direction to target
            Vector3 direction = (target.position - transform.position).normalized;

            //If target is within detection angle
            if (Vector3.Angle(transform.forward, direction) <= fovDetectionAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                //if no obstructions
                if (!Physics.Raycast(transform.position, direction, distance, visionObstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                //player is outside of the AI's FOV
                canSeePlayer = false;
            }
        } else
        {
            canSeePlayer = false;
        }


    }

    //Get-sets

    public NavMeshAgent Agent
    {
        get { return agent; }
    }
}
