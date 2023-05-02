using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObjects : MonoBehaviour
{
    public GameObject []gameObjectToInstantiate;

    private GameObject spawnObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    private int spawnIndex;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount>0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out touchPosition))
            return;
        if(_arRaycastManager.Raycast(touchPosition, hits,TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(spawnObject == null)
            {
                spawnIndex = ShapeSelector.shapeIndex;
                spawnObject = Instantiate(gameObjectToInstantiate[spawnIndex], hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnObject.transform.position = hitPose.position;
            }
        }
    }
}
