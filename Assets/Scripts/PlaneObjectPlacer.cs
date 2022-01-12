using UnityEngine;

public class PlaneObjectPlacer : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject objectPrefab;

    private IPlanePointCalculator pointCalculator;

    private GameObject settedObject;

    private void Start()
    {
        pointCalculator = FindObjectOfType<LinksStorage>().PlanePointCalculator;
        if (pointCalculator != null)
        {
            pointCalculator.OnPlacementChanged += SetObject;
        }
        settedObject = Instantiate(objectPrefab, gameObject.transform, true);
        settedObject.SetActive(false);
    }

    private void SetObject(Pose pose)
    {
        if (!settedObject.activeInHierarchy)
        {
            settedObject.SetActive(true);
        }

        if (pose == default)
        {
            return;
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
