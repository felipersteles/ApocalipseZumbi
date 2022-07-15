using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles
{
    public class ZombieAnimatorHandler : MonoBehaviour
    {
        private Animator anim;
        private EnemyStats zombie;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            zombie = GetComponentInParent<EnemyStats>();
        }

        private void FixedUpdate()
        {
            anim.SetBool("isAttacking", zombie.isAttacking);
            anim.SetBool("isWalking", zombie.isWalking);
            anim.SetBool("isDying", zombie.isDead);
            anim.SetBool("wonTheGame", zombie.eating);
        }
    }
}