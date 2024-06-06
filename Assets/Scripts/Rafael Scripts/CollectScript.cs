using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Material") return;
            print("Collision was player");

        //? call method to collect (get data) and destroy object
        
    }
}
