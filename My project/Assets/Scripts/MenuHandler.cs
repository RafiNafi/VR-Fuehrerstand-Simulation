using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public Toggle freeMode;
    public Toggle processMode;
    public Toggle exameMode;
    public TMP_Dropdown selectProcess;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMode()
    {
        if (freeMode.isOn)
        {
            toggleAllTooltips(true);
        }
        else if (processMode.isOn)
        {
            toggleAllTooltips(false);
        }
        else if (exameMode.isOn)
        {
            toggleAllTooltips(false);
        }
    }

    public void toggleAllTooltips(bool activate)
    {
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if (script.GetType().Name is "TooltipTrigger")
            {
                script.enabled = activate;
            }
        }
    }
}
