using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles
{
    public class WaveSpawner : MonoBehaviour
    {
        public enum SpawnState{ SPAWNING, WAITING, COUNTING};

        [System.Serializable]
        public class Wave
        {
            public string Name;
            public GameObject Enemy;
            public int count;
            public float rate;
        }

        private int currentSpawn = 0;
        public int countDead = 0;

        public Wave[] waves;
        private int nextWave = 0;

        public GameObject[] spawnPoints;

        public float timeBetweenWaves = 5f;
        private float waveCountDown; //de preferencia private

        private float searchCountDown = 1f;

        private SpawnState state = SpawnState.COUNTING;

        void Start()
        {
            waveCountDown = timeBetweenWaves;
            spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        }

        void Update()
        {
            
            if(currentSpawn > spawnPoints.Length)
                currentSpawn = 0;
                
            if(state == SpawnState.WAITING)
            {
                // Check if enemies are still alive
                if(!EnemyIsAlive(waves[nextWave], countDead))
                {
                    //Begin a new round
                    Debug.Log("Count of the wave: "+ waves[nextWave].count);
                    NewRound();
                }
                else
                {
                    return;
                }
            }

            if(waveCountDown<=0)
            {
                if(state != SpawnState.SPAWNING)
                {
                    //Começa a spawwnar a wave
                    if(nextWave>=0)
                        StartCoroutine( SpawnWave(waves[nextWave]) );
                }
            }
            else
            {
                waveCountDown -= Time.deltaTime;
            }
        }

        void NewRound()
        {
            Debug.Log("Wave completed");

            state = SpawnState.COUNTING;
            waveCountDown = timeBetweenWaves;

            if(nextWave + 1 > waves.Length - 1)
            {
                nextWave = -1;
                Debug.Log("Completed all waves"); //all waves complete
            }
            else
                nextWave++;
        }

        private bool EnemyIsAlive(Wave wave, int numOfVictims)
        {

            searchCountDown -= Time.deltaTime;
            if(searchCountDown <= 0f)
            {
                searchCountDown = 1f;

                GameObject[] gos;
                gos = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject go in gos)
                {
                    var enemyStats = go.GetComponent<EnemyStats>();
                    if(enemyStats != null)
                        if(enemyStats.isDead)
                            countDead++;
                }

                if( countDead == (numOfVictims + wave.count) ) //Numero de vitimas total mais o numero de inimigos no round
                {
                    ClearEnemys();
                    return false;
                }
            }
            countDead = numOfVictims;
            return true;
        }

        private void ClearEnemys()
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
        }

        IEnumerator SpawnWave (Wave _wave)
        {
            Debug.Log("Spawning Wave: " + _wave.Name);
            state = SpawnState.SPAWNING;

            //Spawn
            for(int i = 0; i < _wave.count; i++)
            {
                    SpawnEnemy(_wave.Enemy);

                    yield return new WaitForSeconds( 1f/_wave.rate );
            }

            state = SpawnState.WAITING;

            yield break;
        }

        void SpawnEnemy(GameObject _enemy)
        {
            // Spawn enemy
            Debug.Log("Spawning Enemy: " + _enemy.name); //lembrar de adicionar o tipo
            
            if(spawnPoints.Length == 0)
                Debug.Log("No spwawn points refered");

            Transform _sp = spawnPoints[currentSpawn].transform;

            //RandomSkin(_enemy);
            Instantiate(_enemy.transform, _sp.position, _sp.rotation);

            currentSpawn++;
        }
        
        //Skin stuffs
        private void RandomSkin(GameObject zumbi){}
    }
}
