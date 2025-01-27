using UnityEngine;
using TMPro;
using System;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class InteractionDetection : MonoBehaviour
{
    public GameObject tooltipPrefab;
    private GameObject currentTooltip; // Instance of the tooltip
    public float rayLength = 2f; // Length of the ray

    public MenuHandler menuHandler;

    public ActionBasedController controllerRight;

    public float cooldownTime = 1.0f;
    private float lastClickTime = 0f;

    public InputActionProperty grabAction;

    private void Start()
    {
        grabAction.action.Enable();
    }

    void Update()
    {

        Ray ray = new Ray(controllerRight.transform.position, controllerRight.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // Check if hit object has TooltipTrigger script
            TooltipTrigger tooltipTrigger = hit.collider.GetComponent<TooltipTrigger>();


            if (tooltipTrigger != null && tooltipTrigger.enabled)
            {
                ShowTooltip(hit.point, tooltipTrigger.tooltipText, hit.collider.gameObject);

            }
            else
            {
                HideTooltip();
            }

            if (hit.collider.GetComponent<Outline>() != null)
            {
                if (IsTriggerPressed(controllerRight) && IsClickAllowed())
                {
                    lastClickTime = Time.time;
                    menuHandler.goToNextObject(hit.collider.gameObject.name);
                    Debug.Log("Interacted with " + hit.collider.gameObject.name);

                }
            }

        }
        else
        {
            HideTooltip();
        }
    }

    bool IsClickAllowed()
    {
        return Time.time >= lastClickTime + cooldownTime;
    }

    bool IsTriggerPressed(ActionBasedController controller)
    {
        if (controller.activateAction.action != null)
        {
            return grabAction.action.ReadValue<float>() > 0.5f;
        }
        return false;
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
