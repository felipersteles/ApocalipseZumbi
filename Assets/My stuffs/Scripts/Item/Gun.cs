
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles
{
    public class Gun : MonoBehaviour
    {
        private GameObject alvo;
        public int numOfVictims = 0;

        [SerializeField]
        [Range(0.5f, 1.5f)]
        private float fireRate = 1;

        [SerializeField]
        [Range(1,10)]
        private int damage = 1;

        [SerializeField]
        private ParticleSystem muzzleParticle;

        [SerializeField]
        private AudioSource gunFireSource;

        private float timer;

        private void Start()
        {
            muzzleParticle = GetComponentInChildren<ParticleSystem>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= fireRate)
            {
                if(Input.GetButton("Fire1"))
                {
                    timer = 0f;
                    FireGun();
                }
            }
        }

        private void FireGun()
        {

            muzzleParticle.Play();
            gunFireSource.Play();
            
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f); //ray partindo do centro da screen
            RaycastHit hitInfo;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

            if(Physics.Raycast(ray, out hitInfo, 100))
            {
                alvo = hitInfo.collider.gameObject;
                Debug.Log("Atingiu o " + alvo.name);

                if(alvo.tag == "Enemy")
                {
                    var enemyHealth = hitInfo.collider.GetComponent<EnemyStats>();
                    if (enemyHealth != null)
                        enemyHealth.TakeDamage(damage);
                }
            }
        }
    }
}
