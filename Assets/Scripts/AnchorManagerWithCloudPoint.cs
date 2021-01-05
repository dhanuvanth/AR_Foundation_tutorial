using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARPointCloudManager))]
public class AnchorManagerWithCloudPoint : MonoBehaviour
{
    private ARRaycastManager aRRaycastManager;

    private ARAnchorManager anchorManager;

    private ARPointCloudManager pointCloudManager;

    private List<ARAnchor> anchorPoints = new List<ARAnchor>();

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        anchorManager = GetComponent<ARAnchorManager>();
        pointCloudManager = GetComponent<ARPointCloudManager>();
    }


    public void TogglePlane()
    {
        pointCloudManager.enabled = !pointCloudManager.enabled;

        foreach (var plane in pointCloudManager.trackables)
        {
            plane.gameObject.SetActive(pointCloudManager.enabled);
        }


    }

    [System.Obsolete]
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        if (touch.phase != TouchPhase.Began)
            return;

        if (aRRaycastManager.Raycast(touch.position,hits,UnityEngine.XR.ARSubsystems.TrackableType.FeaturePoint))
        {
            Pose hitPose = hits[0].pose;
            ARAnchor anchor = anchorManager.AddAnchor(hitPose);

            if (anchor == null)
            {
                Debug.Log("Error");
            }
            else
            {
                anchorPoints.Add(anchor);
            }
        }

    }
}
