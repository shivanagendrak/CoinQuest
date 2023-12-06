using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Transform firstPoint;
    public Transform secondPoint;
    // private Transform currentTarget;
    Vector3 destinationPoint;
    public NavMeshAgent agent;
    public Vector3 startPosition;
    public bool isFollowPlayer;
    public bool isRndom;
    public bool isBetweenTwoPoint;
    public float followTargetDist;
    public float randomDist;
    //public Animator enemyAnimator;

    private void Start()
    {
        target = GameManager.Instance.playertarget.transform;
        startPosition = transform.position;
        if (isBetweenTwoPoint)
        {
            destinationPoint = secondPoint.position;
        }
        else if (isRndom)
        {
            destinationPoint = RandomNavSphere(transform.position, randomDist, -1);
        }
        agent.SetDestination(destinationPoint);
    }

    private void Update()
    {

        if (followTargetDist != -1 && isRndom && target)
        {
            float dist = Vector3.Distance(target.position, transform.position);
            if (dist < followTargetDist)
            {
                isFollowPlayer = true;
                isRndom = false;
            }
            else if (isFollowPlayer)
            {
                isFollowPlayer = false;
                isRndom = true;
            }
        }

        if (!isFollowPlayer)
        {
            float dist = Vector3.Distance(destinationPoint, transform.position);


            if (dist < 0.5f)
            {
                if (isBetweenTwoPoint)
                {
                    if (destinationPoint == firstPoint.position)
                    {
                        destinationPoint = secondPoint.position;
                    }
                    else
                    {
                        destinationPoint = firstPoint.position;
                    }
                }
                else if (isRndom)
                {
                    destinationPoint = RandomNavSphere(transform.position, randomDist, -1);
                }
            }
        }
        else
        {
            if (!target)
                return;
            destinationPoint = target.position;
        }
        agent.SetDestination(destinationPoint);
        // enemyAnimator.SetFloat("Speed",1);
    }

    public void StopEnemy()
    {
        //enemyAnimator.SetFloat("Speed", 0);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
