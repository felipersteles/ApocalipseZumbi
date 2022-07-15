using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace felipsteles
{
    public class Score : MonoBehaviour
    {
        public int zombieCount;

        public Text displayContagem;

        private Gun player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        }

        // Update is called once per frame
        void Update()
        {
            zombieCount = player.numOfVictims;
            displayContagem.text = zombieCount.ToString();
        }
    }   
}
