using UnityEngine;
using UnityEngine.UI;

public class ResetHandler : MonoBehaviour
{
    [SerializeField] private Button button;

    private IPlanePointCalculator planeCalculator;

    private void Start()
    {
        planeCalculator = FindObjectOfType<LinksStorage>().PlanePointCalculator;
        if (planeCalculator == null)
        {
            Debug.LogError("IPlanePointCalculator field is null");
            Destroy(this);
        }

        if (button != null)
        {
            button.onClick.RemoveListener(HandleReset);
            button.onClick.AddListener(HandleReset);
        }
    }

    private void HandleReset()
    {
        planeCalculator.StartPlaneSearch();
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(HandleReset);
        }
    }
}