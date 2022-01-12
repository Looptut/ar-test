using UnityEngine;

public class LinksStorage : MonoBehaviour
{
    [SerializeField] private ResetHandler resetHandler;
    [SerializeField] private PlaneObjectPlacer planeObjectPlacer;
    [SerializeField] private ARPlaneFrontPointCalculator pointCalculator;


    public ISearchCaller SearchCaller => pointCalculator;
    public IPlanePointCalculator PlanePointCalculator => pointCalculator;

}
