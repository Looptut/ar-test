using System;
using UnityEngine;

public class PlaneObjectPlacer : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject objectPrefab;

    private IPlanePointCalculator pointCalculator;
    private ISearchCaller searchCaller;
    private GameObject settedObject;

    private void Start()
    {
        var storage = FindObjectOfType<LinksStorage>();
        pointCalculator = storage.PlanePointCalculator;

        if (pointCalculator != null)
        {
            pointCalculator.OnPlacementChanged += SetObject;
        }

        searchCaller = storage.SearchCaller;
        if (searchCaller != null)
        {
            searchCaller.OnStartSearching += HandleSearchStart;
        }
        settedObject = Instantiate(objectPrefab, gameObject.transform, true);

        HandleSearchStart();
    }

    private void HandleSearchStart()
    {
        settedObject.SetActive(false);
    }

    private void SetObject(Pose pose)
    {
        if (pose == default)
        {
            return;
        }

        if (!settedObject.activeInHierarchy)
        {
            settedObject.SetActive(true);
        }

        settedObject.transform.position = pose.position;

        var cameraForward = arCamera.transform.forward;
        settedObject.transform.rotation = Quaternion.LookRotation(new Vector3(cameraForward.x, 0, cameraForward.z)).normalized;
    }

    private void OnDestroy()
    {
        if (pointCalculator != null)
            pointCalculator.OnPlacementChanged += SetObject;
    }
}
