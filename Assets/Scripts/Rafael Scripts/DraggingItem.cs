using Unity.VisualScripting;
using UnityEngine;

class DraggingItem : MonoBehaviour
{
    static public Item item;
    public static DraggingItem Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

}
