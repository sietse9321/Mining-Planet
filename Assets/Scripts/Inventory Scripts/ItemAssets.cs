using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
public static ItemAssets Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }


    public Transform itemWorldPrefab;

    public Sprite stoneSprite;
    public Sprite titaniumSprite;
    public Sprite malachiteSprite;
    public Sprite copperSprite;
    public Sprite ironSprite;
}
