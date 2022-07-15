using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace felipsteles{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Gun gun;
        //Animator anim;
        PlayerLocomotion playerLocomotion;

        public bool isInteracting;
        public bool isSprinting;
        public bool isGrounded;
        public bool isInAir;
        public bool isDead;


        //Player stats
        [SerializeField]
        public int maxHealth = 30;
        [SerializeField]
        public int currentHealth;

        //Player score
        [SerializeField]
        public int score;

        //Multiplayer stuffs
        PhotonView view;

        void Start()
        {
            currentHealth = maxHealth;
            isDead = false;

            inputHandler = GetComponent<InputHandler>();
            gun = GetComponent<Gun>();
            //anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();

            view = GetComponent<PhotonView>();
        }

        // Update is called once per frame
        void Update()
        {
            if(view.IsMine)
            {
                float delta = Time.deltaTime;
                score = gun.numOfVictims;

                inputHandler.TickInput(delta);
                if(!isDead)
                    playerLocomotion.HandleMovement(delta);

                if(currentHealth <= 0 && isDead == false)
                    Die();
            }
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
        }

        private void Die()
        {
            isDead = true;
            Debug.Log("Game Over");
        }
    }
}