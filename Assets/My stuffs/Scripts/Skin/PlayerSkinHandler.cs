
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipstes
{
public class PlayerSkinHandler : MonoBehaviour
{
    public GameObject[] skins;
    public SkinCode currentSkinCode = 0;
    private SkinnedMeshRenderer currentSkin;
    private Skin skin;

    public bool isChanging;

    // Start is called before the first frame update
    void Start()
    {
        skins = GameObject.FindGameObjectsWithTag("Skin");
        skin = skins[(int)currentSkinCode].GetComponent<Skin>();
        currentSkin = skins[(int)currentSkinCode].GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isChanging)
            SelectSkin(currentSkinCode);
    }

    private void SelectSkin(SkinCode skinID)
    {
        currentSkin.enabled = false;
        skin.isEnabled = false;

        currentSkin = skins[(int)skinID].GetComponent<SkinnedMeshRenderer>();
        skin = skins[(int)currentSkinCode].GetComponent<Skin>();
        
        currentSkin.enabled = true;
        skin.isEnabled = true;
    }
}
}

