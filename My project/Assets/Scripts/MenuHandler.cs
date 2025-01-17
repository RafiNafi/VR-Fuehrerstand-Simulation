using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Playables;
using System.Xml;
using UnityEngine.InputSystem;

public class MenuHandler : MonoBehaviour
{
    public Toggle freeMode;
    public Toggle processMode;
    public Toggle exameMode;
    public TMP_Dropdown selectProcess;

    private string persistentFilePath;
    private string sourcePath;

    ScenarioCollection sc;

    // Start is called before the first frame update
    void Start()
    {

        persistentFilePath = Application.persistentDataPath + "/scenarios.json";
        sourcePath = Application.streamingAssetsPath + "/scenarios.json";

        if (!File.Exists(persistentFilePath))
        {
            copyFileToPersistentPath();
        }

        sc = loadScenario();

        addDropdownOptions();

        selectProcess.gameObject.SetActive(false);
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
            startScenario();
        }
        else if (exameMode.isOn)
        {
            toggleAllTooltips(false);
            startScenario();
        }
    }

    public void startScenario()
    {
        string name = selectProcess.options[selectProcess.value].text;

        foreach (Scenario scenario in sc.scenarios)
        {
            if (scenario.name == name)
            {
                //TODO
            }
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

    public void checkTooltip()
    {
        if (freeMode.isOn)
        {
            selectProcess.gameObject.SetActive(false);
        }
        else if (processMode.isOn)
        {
            selectProcess.gameObject.SetActive(true);
        }
        else if (exameMode.isOn)
        {
            selectProcess.gameObject.SetActive(true);
        }
    }

    [System.Serializable]
    public class Scenario
    {
        public string name;
        public List<string> objects;
    }

    [System.Serializable]
    public class ScenarioCollection
    {
        public List<Scenario> scenarios;
    }



    public void saveScenario(ScenarioCollection data)
    {
        string jsonData = JsonUtility.ToJson(data, true);

        File.WriteAllText(persistentFilePath, jsonData);
    }

    public ScenarioCollection loadScenario()
    {
        if (File.Exists(persistentFilePath))
        {
            string jsonData = File.ReadAllText(persistentFilePath);
            ScenarioCollection data = JsonUtility.FromJson<ScenarioCollection>(jsonData);

            return data;
        }
        else
        {
            Debug.LogWarning("File not found.");
            return null;
        }
    }

    public void copyFileToPersistentPath()
    {
        string sourcePath = Path.Combine(Application.streamingAssetsPath, "scenarios.json");

        if (Application.platform == RuntimePlatform.Android)
        {
            var www = new WWW(sourcePath);
            while (!www.isDone) { }
            File.WriteAllBytes(persistentFilePath, www.bytes);
        }
        else
        {
            File.Copy(sourcePath, persistentFilePath);
        }

    }

    void addDropdownOptions()
    {
        if (sc != null)
        {
            List<string> options = new List<string>();

            foreach (var scenario in sc.scenarios)
            {
                options.Add(scenario.name);
            }

            selectProcess.ClearOptions();

            selectProcess.AddOptions(options);
        }
    }
}
