using UnityEngine;
using TMPro;

public class InteractionDetection : MonoBehaviour
{
    public GameObject tooltipPrefab; // Assign your tooltip prefab
    private GameObject currentTooltip; // Instance of the tooltip
    public Transform controllerTransform; // VR controller transform
    public float rayLength = 2f; // Length of the ray

    void Update()
    {
        // Shoot a ray from the controller
        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // Check if the hit object has a TooltipTrigger script
            TooltipTrigger tooltipTrigger = hit.collider.GetComponent<TooltipTrigger>();

            if (tooltipTrigger != null)
            {
                ShowTooltip(hit.point, tooltipTrigger.tooltipText, hit.collider.gameObject);
   
            }
            else
            {
                HideTooltip();
            }
        }
        else
        {
            HideTooltip();
        }
    }

    void ShowTooltip(Vector3 position, string text, GameObject g)
    {
        if (currentTooltip == null)
        {
            currentTooltip = Instantiate(tooltipPrefab, position, Quaternion.identity);
        }

        // Update the tooltip's position and text
        currentTooltip.transform.position = position;
        currentTooltip.transform.position += new Vector3(0, 0.3f, 0);
        currentTooltip.transform.LookAt(Camera.main.transform);
        currentTooltip.transform.Rotate(0, 180, 0);
        currentTooltip.GetComponentInChildren<TextMeshPro>().text = text;
    }

    void HideTooltip()
    {
        if (currentTooltip != null)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }
}
