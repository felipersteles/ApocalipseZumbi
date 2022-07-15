
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace felipsteles
{
    public class GameStats : MonoBehaviour
    {
        public float health;
        public float healthMax;
        public int zombieCount;

        private PlayerManager playerStats;
        
        [SerializeField]
        public Text displayContagem;
        [SerializeField]
        public Image healthBar;

        
        [SerializeField]
        private GameObject gamingScreen;
        [SerializeField]
        private GameObject gameOverScreen;

        private void Start()
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }

        private void Update()
        {
            PlayerLife();
            DisplayScore();

            if(playerStats.isDead)
                GameOver();
        }

        private void PlayerLife()
        {
            health = playerStats.currentHealth;
            healthMax = playerStats.maxHealth;

            healthBar.fillAmount = health / healthMax;
        }

        private void DisplayScore()
        {
            zombieCount = playerStats.score;
            displayContagem.text = zombieCount.ToString();
        }

        private void GameOver()
        {
            gamingScreen.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }
}
