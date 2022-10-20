using WotN.Player;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace WotN.Interactables
{
    public class NPCMovement : MonoBehaviour
    {
        private static readonly int moveParam = Animator.StringToHash("move");

        [SerializeField]
        private List<Vector3> stops = new List<Vector3>();

        [SerializeField]
        private float waitTime = 3;

        [SerializeField]
        private float rotateTime = 6f;

        [SerializeField]
        private bool isTalking;

        private PlayerNPCInteractable playerTarget;

        private float currentWaitTime;

        private NavMeshAgent navMeshAgent;

        private Animator anim;

        private int currentIndex = 0;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            currentWaitTime = waitTime;
        }
        void Update()
        {
            if (!isTalking)
            {
                if (navMeshAgent.velocity.magnitude == 0)
                {

                    currentWaitTime -= Time.deltaTime;

                    if (currentWaitTime <= 0)
                    {
                        ++currentIndex;

                        if (currentIndex >= stops.Count)
                        {
                            currentIndex = 0;
                        }

                        navMeshAgent.destination = stops[currentIndex];

                        currentWaitTime = waitTime;
                    }

                }
            }
            else
            {
                currentWaitTime = waitTime;

                if (playerTarget)
                {
                    var temp = new Vector3(playerTarget.transform.position.x, transform.position.y, playerTarget.transform.position.z);

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(temp - transform.position, Vector3.up), rotateTime * Time.deltaTime);
                }

            }

            anim.SetFloat(moveParam, navMeshAgent.velocity.magnitude);
        }

        public void SetWalkStops(Transform localSpawnPointPosition,Vector3[] walkStops)
        {
            for (int i = 0; i < walkStops.Length; i++)
            {
                stops.Add(localSpawnPointPosition.localPosition + walkStops[i]);
            }
        }

        public void StopTalking()
        {
            playerTarget = null;
            isTalking = false;
        }

        public void TalkToPlayer(PlayerNPCInteractable target)
        {
            navMeshAgent.SetDestination(transform.position);
            playerTarget = target;
            this.isTalking = true;
        }
    }
}

