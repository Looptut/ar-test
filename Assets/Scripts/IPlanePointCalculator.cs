using System;
using UnityEngine;

public interface IPlanePointCalculator
{
    event Action<Pose> OnPlacementChanged;
    void StartPlaneSearch();
}
