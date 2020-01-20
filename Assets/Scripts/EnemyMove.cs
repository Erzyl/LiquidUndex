using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyMove : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;

    public ThirdPersonCharacter character;

    private void Start() {
        agent.updateRotation = false;

        target = GameObject.Find("Player").transform;
    }

    void Update(){
        agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero,false,false);
    }
}
