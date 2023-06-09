using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject []arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool PlacementPoseIsValid = false;
    [SerializeField]private int spawnIndex;

    // Start is called before the first frame update
    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject == null && PlacementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    void UpdatePlacementIndicator()
    {
        if(spawnedObject == null && PlacementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetLocalPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        PlacementPoseIsValid = hits.Count > 0;
        if(PlacementPoseIsValid )
        {
            PlacementPose = hits[0].pose;
        }
    }
    void ARPlaceObject()
    {
        spawnIndex = ShapeSelector.shapeIndex;
        spawnedObject = Instantiate(arObjectToSpawn[spawnIndex], PlacementPose.position, PlacementPose.rotation);
    }
}
