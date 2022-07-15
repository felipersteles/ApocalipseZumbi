using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace felipsteles
{
    public class HealthBarHandler : MonoBehaviour
    {
        public float health;
        public float healthMax;

        public Image healthBar;

        private PlayerManager playerStats;

        private void Start()
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }

        private void Update()
        {
            PlayerLife();
        }

        private void PlayerLife()
        {
            health = playerStats.currentHealth;
            healthMax = playerStats.maxHealth;

            healthBar.fillAmount = health / healthMax;
        }
    }
}
