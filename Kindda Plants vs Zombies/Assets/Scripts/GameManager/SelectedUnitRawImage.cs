using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUnitRawImage : MonoBehaviour
{
    RawImage m_RawImage;
    
    public Texture[] tex = new Texture[5];

    private void Awake()
    {
        m_RawImage = GetComponent<RawImage>();
        tex[0] = Resources.Load("Priest", typeof(Texture)) as Texture;
        tex[1] = Resources.Load("Archer", typeof(Texture)) as Texture;
        tex[2] = Resources.Load("Paladin", typeof(Texture)) as Texture;
        tex[3] = Resources.Load("Barricade", typeof(Texture)) as Texture;
        tex[4] = Resources.Load("Barrel", typeof(Texture)) as Texture;
    }

    void Start()
    {
        m_RawImage.texture = tex[GameManager.instance.gameInteraction.selectedUnitIdx];
    }

    void Update()
    {
        m_RawImage.texture = tex[GameManager.instance.gameInteraction.selectedUnitIdx];
    }
}
