using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles
{
    public class SkinHandler : MonoBehaviour
    {
        public Skin currentSkin;
        private Skin newSkin;

        private List<Skin> skins = new List<Skin>();
        
        [SerializeField]
        public SkinCode codigoDaSkin;

        private void Start()
        {
            currentSkin = GameObject.FindGameObjectWithTag("ActivedSkin").GetComponent<Skin>();
            codigoDaSkin = currentSkin.code;
        }

        private void Update()
        {
            SelectSkinByIndex(codigoDaSkin);
        }

        public void SelectSkinByIndex(SkinCode index)
        {
            currentSkin.code = index;
        }

        /*public bool ChangeSkin(GameObject newSkin)
        {
            if(newSkin != null)
            {
                currentSkin.SetActive(false);
                newSkin.SetActive(true);

                currentSkin = newSkin;

                return true;
            }

            return false;
        }*/
    }

    /*
        GameObject SegurancaAero = GameObject.Find("SegurancaAeroporto");
        GameObject ControleAero = GameObject.Find("ControleAeroporto");
        GameObject CarregadorDeMala = GameObject.Find("CarregadorDeMalas");
        GameObject Executivo = GameObject.Find("Executivo");
        GameObject LiderDeTorcida = GameObject.Find("LiderDeTorcida");
        GameObject Palhaco = GameObject.Find("Palhaco");
        GameObject Fazendeiro = GameObject.Find("Fazendeiro");
        GameObject MulherFazendeira = GameObject.Find("MulherFazendeira");
        GameObject Bombeiro = GameObject.Find("Bombeiro");
        GameObject JogadorDeFutebol = GameObject.Find("JogadorDeFutebol");
        GameObject Avo = GameObject.Find("Avo");
        GameObject MoradorDeAreaLivre = GameObject.Find("MoradorDeAreaLivre");
        GameObject Industrial = GameObject.Find("Industrial");
        GameObject Mecanico = GameObject.Find("Mecanico");
        GameObject PoliciaMontada = GameObject.Find("PoliciaMontada");
        GameObject Piloto = GameObject.Find("Piloto");
        GameObject Gangster = GameObject.Find("Gangster");
        GameObject Prisioneiro = GameObject.Find("Prisioneiro");
        GameObject TrabalhadorEstrada = GameObject.Find("TrabalhadorEstrada");
        GameObject Ladrao = GameObject.Find("Ladrao");
        GameObject Corredor = GameObject.Find("Corredor");
        GameObject PapaiNoel = GameObject.Find("PapaiNoel");
        GameObject AtendenteLoja = GameObject.Find("AtendenteLoja");
        GameObject Soldado = GameObject.Find("Soldado");
        GameObject JogadorDeBasket = GameObject.Find("JogadorDeBasket");
        GameObject Turista = GameObject.Find("Turista");
        GameObject Caminhoneiro = GameObject.Find("Caminhoneiro");
    */
}