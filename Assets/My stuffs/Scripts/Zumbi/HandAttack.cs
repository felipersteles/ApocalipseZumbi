using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles
{
    public class HandAttack : MonoBehaviour
    {
        private EnemyStats zombie;

        public float attackTime;

        // Start is called before the first frame update
        void Start()
        {
            zombie = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>();
        }

        // Update is called once per frame
        void Update()
        {   
            attackTime -= Time.deltaTime;
            if(zombie.isAttacking && attackTime < 0)
                Hit();
        }

        private void Hit()
        {
            Ray ray = new Ray(gameObject.transform.position, new Vector3(0.5f,0,0)); //ray partindo do centro da screen
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, 100))
            {
                var alvo = hitInfo.collider.gameObject;
                if(alvo.name != gameObject.name)
                    Debug.Log("Zumbi atingiu o " + alvo.name);

                if(alvo.tag == "Player")
                {
                    var playerHealth = hitInfo.collider.GetComponent<PlayerManager>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(zombie.damage);
                        attackTime = 1f;
                    }
                }
            }
        }
    }
}
