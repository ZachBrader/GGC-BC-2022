using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Main AI Script for enemies


[RequireComponent(typeof(NavMeshAgent))]
public class WoofersAI : MonoBehaviour
{
    [Tooltip("A list of Transfroms from the scene. " +
        "The AI's default behavior is to make its way from point to pointas a sort of patrol. " +
        "The points that the AI tries to get to are listed here.")]
    [SerializeField]
    private LoopingList<Transform> patrolPoints;

    [Tooltip("How close does the AI need to be to a patrol point " +
        "to move on to the next one?")]
    [SerializeField]
    private float goodEnough; 



    //The NavMesh Agent attached to the AI
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolPoints.resetCurrentIndex();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
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
}
