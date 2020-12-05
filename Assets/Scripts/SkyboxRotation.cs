using UnityEngine;


public class SkyboxRotation : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier;

    void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _speedMultiplier);
    }
}
