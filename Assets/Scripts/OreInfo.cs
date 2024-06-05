using UnityEngine;

public class OreInfo : MonoBehaviour
{
    public string materialName;
    public int materialAmount;
    public Color materialColor;

    public void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = materialColor;
    }
    public void AssignValues(string mName,int mAmount, Color mColor)
    {
        materialName = mName;
        materialAmount = mAmount;
        materialColor = mColor;
    }
}