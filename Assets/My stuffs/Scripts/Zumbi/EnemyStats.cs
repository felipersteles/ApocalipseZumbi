using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField]
        private int startingHealth = 5;
        [SerializeField]        
        private int currentHealth;
        [SerializeField]
        public int damage = 4;

        public bool isAttacking;
        public bool isWalking;
        public bool eating;
        public bool isDead;
        
        private Gun player;

        private void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();

            currentHealth = startingHealth;
            isDead = false;
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;

            if(currentHealth <= 0 && isDead == false)
                Die();
        }

        private void Die()
        {
            player.numOfVictims++;
            isDead = true;
        }
    }
}

