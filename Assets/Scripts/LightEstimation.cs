using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    [Header("Scene Refs")]

    [Tooltip("Default AR Camera Manager in the scene")]
    [SerializeField] private ARCameraManager cameraManager;

    [Tooltip("Main Directional Light in the scene")]
    [SerializeField] private Light directionalLight;

    [Header("Controls")]
    [SerializeField] private float intesityMultiplier = 2.0f;


    private void OnEnable()
    {
        cameraManager.frameReceived += OnFrameReceived;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnFrameReceived;
    }

    private void OnFrameReceived(ARCameraFrameEventArgs frameData)
    {
        if (frameData.lightEstimation.averageBrightness.HasValue)
        {
            directionalLight.intensity = frameData.lightEstimation.averageBrightness.Value * intesityMultiplier;
        }

        if (frameData.lightEstimation.averageIntensityInLumens.HasValue)
        {
            directionalLight.intensity = frameData.lightEstimation.averageIntensityInLumens.Value;
        }

        if (frameData.lightEstimation.colorCorrection.HasValue)
        {
            directionalLight.color = frameData.lightEstimation.colorCorrection.Value;
        }

        if (frameData.lightEstimation.mainLightColor.HasValue)
        {
            directionalLight.color = frameData.lightEstimation.mainLightColor.Value;
        }

        if (frameData.lightEstimation.mainLightDirection.HasValue)
        {
            directionalLight.transform.rotation = Quaternion.LookRotation(frameData.lightEstimation.mainLightDirection.Value);
        }

        if (frameData.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
            RenderSettings.ambientProbe = frameData.lightEstimation.ambientSphericalHarmonics.Value;
        }
    }

}
