using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
namespace felipsteles
{
    public enum SkinCode
    {
        SegurancaAero = 0,
        ControleAero = 1,
        CarregadorDeMala = 2,
        Executivo = 3,
        LiderDeTorcida = 4,
        Palhaco = 5,
        Fazendeiro = 6,
        MulherFazendeira = 7,
        Bombeiro = 8,
        JogadorDeFutebol = 9,
        Avo = 10,
        MoradorDeAreaLivre = 11,
        Industrial = 12,
        Mecanico = 13,
        PoliciaMontada = 14,
        Piloto = 15,
        Gangster = 16,
        Prisioneiro = 17,
        TrabalhadorEstrada = 18,
        Ladrao = 19,
        Corredor = 20,
        PapaiNoel = 21,
        AtendenteLoja = 22,
        Soldado = 23,
        JogadorDeBasket = 24,
        Turista = 25,
        Caminhoneiro = 26
    }

    public class Skin : MonoBehaviour
    {
        public SkinCode code;
        public GameObject skin;
        public bool isActive;

        public void Awake()
        {
            isActive = gameObject.activeSelf;
        }

        public void Update()
        {
            gameObject.SetActive(isActive);
        }
    }
}

