using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class FeatureCheck : MonoBehaviour
{

    public Text checks;

    public ARTrackedImageManager aRTrackedImageManager;
    public ARFaceManager aRFaceManager;
    public AREnvironmentProbeManager aREnvironmentProbeManager;

    // Start is called before the first frame update
    void Start()
    {
        bool supportsMovingImages = aRTrackedImageManager.subsystem.SubsystemDescriptor.supportsMovingImages;
        bool supportsMutableLibrary = aRTrackedImageManager.subsystem.SubsystemDescriptor.supportsMutableLibrary;

        bool supportsEyeTracking = aRFaceManager.subsystem.SubsystemDescriptor.supportsEyeTracking;
        bool supportsFaceMeshNormals = aRFaceManager.subsystem.SubsystemDescriptor.supportsFaceMeshNormals;
        bool supportsFacePose = aRFaceManager.subsystem.SubsystemDescriptor.supportsFacePose;
        bool supportsFaceMeshUVs = aRFaceManager.subsystem.SubsystemDescriptor.supportsFaceMeshUVs;

        bool supportsAutomaticPlacement = aREnvironmentProbeManager.subsystem.SubsystemDescriptor.supportsAutomaticPlacement;
        bool supportsEnvironmentTexture = aREnvironmentProbeManager.subsystem.SubsystemDescriptor.supportsEnvironmentTexture;
        bool supportsEnvironmentTextureHDR = aREnvironmentProbeManager.subsystem.SubsystemDescriptor.supportsEnvironmentTextureHDR;
        bool supportsRemovalOfAutomatic = aREnvironmentProbeManager.subsystem.SubsystemDescriptor.supportsRemovalOfAutomatic;

        checks.text = $"supportsMovingImages : {supportsMovingImages}\n" +
            $"supportsMutableLibrary : {supportsMutableLibrary}\n" +
            $"supportsEyeTracking : {supportsEyeTracking}\n" +
            $"supportsFaceMeshNormals : {supportsFaceMeshNormals}\n" +
            $"supportsFacePose : {supportsFacePose}\n" +
            $"supportsFaceMeshUVs : {supportsFaceMeshUVs}\n" +
            $"supportsAutomaticPlacement : {supportsAutomaticPlacement}\n" +
            $"supportsEnvironmentTexture : {supportsEnvironmentTexture}\n" +
            $"supportsEnvironmentTextureHDR : {supportsEnvironmentTextureHDR}\n" +
            $"supportsRemovalOfAutomatic : {supportsRemovalOfAutomatic}\n";
    }
}