using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLight : MonoBehaviour
{
    GameObject player;
    Light2D globalLight;
    CinemachineVirtualCamera virtualCamera;
    public float time = 12f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        virtualCamera = player.GetComponentInChildren<CinemachineVirtualCamera>();
        globalLight = GetComponentInChildren<Light2D>();
        InvokeRepeating("ChangeLight", 60, 60);
    }
    /// <summary>
    /// For chaning the light based on the time
    /// </summary>
    void ChangeLight()
    {
        
    }
    void Update()
    {
        //if player is above 0
        if (player.transform.position.y >= 0)
        {
            //sets the light intensity to 1
            globalLight.intensity = 1f;
            //sets the virtual camera size to 1.5
            virtualCamera.m_Lens.OrthographicSize = 2f;
        }
        else
        {
            //sets float t based on -2 0 of the player y position
            float t = Mathf.InverseLerp(-2f, 0f, player.transform.position.y);
            //interpolate virtual camera size from 1 to 1.5 based on t
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(1.5f, 2f, t);
            globalLight.intensity = Mathf.Lerp(0.2f, 1f, t);
        }
    }
}