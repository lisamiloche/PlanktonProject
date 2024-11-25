using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListImageMerge
{
    public enum Types
    {
        Panneau,
        BoutDeBois,
        Vaisseau,
        Boulon,
        AffairesPerdues,
        Compo01,
        Compo02,
        Ferraille,
        Autres
    }
    public Types type;
    public Sprite Sprite;
}
