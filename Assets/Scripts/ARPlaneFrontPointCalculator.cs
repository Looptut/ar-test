using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Determines point on the plane in front of the AR Camera
/// </summary>
public class ARPlaneFrontPointCalculator : MonoBehaviour, ISearchCaller, IPlanePointCalculator
{
    public event Action<Pose> OnPlacementChanged = delegate { };
    public event Action OnStartSearching = delegate { };
    public event Action OnEndSearching = delegate { };

    [SerializeField] private Camera arCamera;

    private ARRaycastManager arRaycast;
    private Pose placement = default;

    private Coroutine searchPlaneCoroutine = null;
    private WaitForEndOfFrame await;

    private void Start()
    {
        arRaycast = FindObjectOfType<ARRaycastManager>();
        if (arRaycast == null)
        {
            Debug.LogError("There's no raycast manager");
            Destroy(this);
        }
        StartPlaneSearch();
    }

    public void StartPlaneSearch()
    {
        if (searchPlaneCoroutine == null)
        {
            searchPlaneCoroutine = StartCoroutine(SearchForPlane());
        }
    }

    private IEnumerator SearchForPlane()
    {
        OnStartSearching.Invoke();
        while (!TryFindPlaneCenter(out placement))
        {
            yield return await;
        }
        OnPlacementChanged.Invoke(placement);
        OnEndSearching.Invoke();
        searchPlaneCoroutine = null;
    }

    private bool TryFindPlaneCenter(out Pose placement)
    {
        var screenCenter = arCamera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        var resultHits = new List<ARRaycastHit>();
        placement = default;

        if (arRaycast.Raycast(screenCenter, resultHits, TrackableType.Planes))
        {
            placement = resultHits[0].pose;
            return true;
        }

        return false;
    }
}
