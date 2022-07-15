using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipstes
{
public enum SkinCode
{
    RiscoBiologico = 0,
    Empresario = 1,
    Medico = 2,
    Fazendeiro = 3,
    Heroina = 4,
    Piromaniaca = 5,
    MulherSoldado = 6,
    MulherCasaco = 7,
    Perigo = 8,
    HomemCasaco = 9,
    Machucado = 10,
    HomemSobrevivente01 = 11,
    HomemSobrevivente02 = 12,
    HomemSobrevivente03 = 13,
    Cacador = 14,
    Sobrevivente01 = 15,
    SobreviventeExperiente = 16,
    Prisioneiro = 17,
    Construtor = 18,
    Explorador = 19,
    Xerife = 20
}

public class Skin : MonoBehaviour
{
    public SkinCode code;
    private SkinnedMeshRenderer skin;

    [SerializeField]
    public bool isEnabled;

    private void Awake()
    {
        skin = GetComponent<SkinnedMeshRenderer>();
        if(!isEnabled)
            skin.enabled = false;
    }
}
}