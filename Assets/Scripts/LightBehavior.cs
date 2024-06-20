using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    Animator animator;
    float repeatTime = 2f;
    int minNumber = 1;
    [SerializeField] int maxNumber = 20;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("RandomLight", 0, repeatTime);
    }
    void RandomLight()
    {
        int number = Random.Range(minNumber, maxNumber+1);

        if (number == minNumber)
        {
            animator.SetTrigger("LightFlik");
        }
        if (number == maxNumber)
        {
            animator.SetTrigger("LightLongOff");
        }
    }
}