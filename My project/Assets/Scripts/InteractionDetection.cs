using UnityEngine;
using TMPro;
using System;

public class InteractionDetection : MonoBehaviour
{
    public GameObject tooltipPrefab;
    private GameObject currentTooltip; // Instance of the tooltip
    public Transform controllerTransform; // VR controller transform
    public float rayLength = 2f; // Length of the ray

    public MenuHandler menuHandler;

    void Update()
    {

        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // Check if hit object has TooltipTrigger script
            TooltipTrigger tooltipTrigger = hit.collider.GetComponent<TooltipTrigger>();

            if (tooltipTrigger != null && tooltipTrigger.enabled)
            {
                ShowTooltip(hit.point, tooltipTrigger.tooltipText, hit.collider.gameObject);

                menuHandler.goToNextObject(hit.collider.gameObject.name);
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

        currentTooltip.transform.position = position;
        currentTooltip.transform.position += new Vector3(0, 0.2f, 0);
        currentTooltip.transform.LookAt(Camera.main.transform);
        currentTooltip.transform.Rotate(0, 180, 0);
        TextMeshPro tmp = currentTooltip.GetComponentInChildren<TextMeshPro>();
        tmp.text = text;
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
