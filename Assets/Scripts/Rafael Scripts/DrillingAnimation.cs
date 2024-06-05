using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillingAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem smoke;
    
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Mouse0))
      {
      animator.SetBool("Drilling", true);
      smoke.Play();
      }
      else if(Input.GetKeyUp(KeyCode.Mouse0))
      {
        smoke.Stop();
        animator.SetBool("Drilling", false);
      }
    }
}
