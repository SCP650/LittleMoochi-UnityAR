using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class TapToPlaceObj : MonoBehaviour
{
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRaycast;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private List<GameObject> objectsList;

    public GameObject placementIndicator;
    public GameObject objectToPlace;
    

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRaycast = FindObjectOfType<ARRaycastManager>();
        objectsList = new List<GameObject>();
        placementIndicator.SetActive(false);
    }
 
    public void StartPlayingAR()
    {
        StartCoroutine(StartScanning());

    }
    public void StopPlayingAR()
    {
        placementIndicator.SetActive(false);
        StopAllCoroutines();
    }
    private IEnumerator StartScanning()
    {
        while(true){
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            //if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //{
            //    PlaceObject();
            //} 
            //use button
            yield return null;
        }
    }


    public void PlaceObject()
    {
        if (placementPoseIsValid)
        {
            GameObject temp = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
            objectsList.Add(temp);
        }
       
    }

    public void DestroyAllObjects()
    {
        foreach(GameObject go in objectsList)
        {
            Destroy(go);
        }
    }
    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycast.Raycast(screenCenter, hits,UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
       }
    }
    void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
}
