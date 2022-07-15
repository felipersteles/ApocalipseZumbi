using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace felipsteles
{
    public class EnemyAI : MonoBehaviour
    {
        private PlayerManager player;
        private NavMeshAgent navMeshAgent;
        private Transform target;
        public float distanceToPlayer; 
        public Vector3 directionToPlayer;
        private ZombieAnimatorHandler animatorHandler;

        public float startWaitTime = 4f;
        public float timeToRotate = 2f;
        public float speedWalk = 6f;
        public float speedRun = 9f;

        public float viewRadius = 15;
        public float viewAngle = 90;
        public LayerMask playerMask;
        public LayerMask obstacleMask;
        public float meshResolution = 1f;
        public int edgeIterations = 4;
        public float edgeDistance = 0.5f;

        public GameObject[] waypoints;
        public int m_CurrentWayPointIndex;

        Vector3 playerLastPosition = Vector3.zero;
        Vector3 playerPosition;

        public float m_WaitTime;
        float m_TimeToRotate;
        bool m_PlayerInRange;
        public bool m_PlayerNear;
        public bool m_IsPatrol;
        public bool m_CaughtPlayer;
        
        private EnemyStats stats;

        // Start is called before the first frame update
        void Start()
        {
            stats = GetComponent<EnemyStats>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

            navMeshAgent = GetComponent<NavMeshAgent>();

            animatorHandler = GetComponentInChildren<ZombieAnimatorHandler>();
            animatorHandler.Initialize();

            playerPosition = Vector3.zero;
            m_IsPatrol = true;
            m_CaughtPlayer = false;
            m_PlayerInRange = false;
            m_WaitTime = startWaitTime;
            m_TimeToRotate = timeToRotate;
            
            waypoints = GameObject.FindGameObjectsWithTag("WayPoint");
            m_CurrentWayPointIndex = NearestPoint();

            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speedWalk;
            navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            //distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            //directionToPlayer = (player.transform.position - transform.position).normalized; //direcao do player
            //Funcao teste
            //FollowAndAttackTheTarget();
            distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if(!stats.isDead)
            {
                EnviromentView();

                if(!m_IsPatrol)
                    Chasing();
                else   
                    Patroling(); 
            }
            else
                navMeshAgent.isStopped = true;
        }

        //Funcoes do zumbi
        private void Chasing() //perseguindo
        {
            m_PlayerNear = false; //enemy ja viu o player
            playerLastPosition = Vector3.zero;

            if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3f)
            {
                CaughtPlayer();
                StopAndHit();
            }
            else
            {
                Move(speedRun);
                navMeshAgent.SetDestination(playerPosition);
            }

            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) //Control if the enemy arrive to the player Location
            {
                if(m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
                {
                    m_IsPatrol = true;
                    m_PlayerNear = false;
                    Move(speedWalk);
                    m_TimeToRotate = timeToRotate;
                    m_WaitTime = startWaitTime;
                    navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].transform.position);
                }
                else
                {
                    if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                    {
                        Stop();
                        m_WaitTime -= Time.deltaTime;
                    }
                }
            }
        }
        private void Patroling()
        {
            if(m_PlayerNear)
            {
                if(m_TimeToRotate <= 0)
                {
                    Move(speedWalk);
                    LookingPlayer(playerLastPosition);
                }
                else
                {
                    Stop();
                    m_TimeToRotate -= Time.deltaTime;
                }
            }
            else
            {
                m_PlayerNear = false;
                playerLastPosition = Vector3.zero;
                navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].transform.position);
                if(navMeshAgent.remainingDistance <= (navMeshAgent.stoppingDistance + 0.2f))
                {
                    if(m_WaitTime <= 0)
                    {
                        NextPoint(m_CurrentWayPointIndex);
                        Move(speedWalk);
                        m_WaitTime = startWaitTime;
                    }
                    else
                    {
                        Stop();
                        m_WaitTime -= Time.deltaTime;
                    }
                }
            }
        }
        private void CaughtPlayer()
        {
            m_CaughtPlayer = true;
        }  
        private void LookingPlayer(Vector3 player)
        {
            navMeshAgent.SetDestination(player);
            distanceToPlayer = Vector3.Distance(transform.position, player);
            if(distanceToPlayer <= 3f)
            {
                if(m_WaitTime <= 0)
                {
                    m_PlayerNear = false;
                    Move(speedWalk);
                    navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].transform.position);
                    m_WaitTime = startWaitTime;
                    m_TimeToRotate = timeToRotate;
                }
                else
                {
                    StopAndHit();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
        private void Move(float speed)
        {
            stats.isWalking = true;
            stats.isAttacking = false;
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
        }
        private void Stop()
        {
            stats.isWalking = false;
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
        }
        private void StopAndHit()
        {
            if(!player.isDead)
                stats.isAttacking = true;
            else
            {
                stats.eating = true;
                stats.isAttacking = false;
            }

            stats.isWalking = false;
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
        }
        public void NextPoint(int index) //The nearst waay point
        {
            float minDistance = 999f;

            for(int i = 0; i < waypoints.Length; i++)
            {
                if( i != index )
                {   
                    var distance = Vector3.Distance(gameObject.transform.position, waypoints[i].transform.position);
                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        m_CurrentWayPointIndex = i;
                    }
                }
            }
            navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].transform.position);
        }
        private int NearestPoint()
        {
            float minDistance = 999f;
            int aux = 0;

            for(int i = 0; i < waypoints.Length; i++)
            {
                var distance = Vector3.Distance(gameObject.transform.position, waypoints[i].transform.position);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    aux = i;
                }
            }

            return aux;
        }

        //Fins de visualização
        private void EnviromentView()
        {
            //Maked an overlap sphere around the enemy to detect the playermask in the view radius
            Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

            for(int i = 0; i < playerInRange.Length; i++)
            {
                Transform player = playerInRange[i].transform;
                directionToPlayer = (player.position - transform.position).normalized;
                if(Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
                {
                    distanceToPlayer = Vector3.Distance(player.position, transform.position);
                    if(!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                    {
                        m_PlayerInRange = true;
                        m_IsPatrol = false;
                    }
                    else
                    {
                        m_PlayerInRange = false;
                    }
                }
                if(Vector3.Distance(transform.position, player.position) > viewRadius)
                {
                    m_PlayerInRange = false;
                }
                if(m_PlayerInRange)
                {
                   playerPosition = player.transform.position;
                }
            }
        }
    }
}