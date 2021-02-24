using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public GameObject handModelPrefab;
    public List<GameObject> controllerPrefabs;
    public InputDeviceCharacteristics controllerCharacteristics;

    private InputDevice targetDevice;
    private InputDevice headsetDevice;
    private GameObject spawnedHandModel;
    private GameObject spawnedController;
    private GameObject prefab;
    private GameObject prefabRiftS1;
    private GameObject prefabRiftS2;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryToInitialize();
    }

    void TryToInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        List<InputDevice> headsets = new List<InputDevice>();
        InputDeviceCharacteristics headsetCharacteristics = InputDeviceCharacteristics.HeadMounted;
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        InputDevices.GetDevicesWithCharacteristics(headsetCharacteristics, headsets);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        foreach (var item in headsets)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            headsetDevice = headsets[0];

            if (headsetDevice.name == "Oculus Rift S")
            {
                prefabRiftS1 = controllerPrefabs.Find(controller => controller.name == "Oculus Quest Controller - Right");
                prefabRiftS2 = controllerPrefabs.Find(controller => controller.name == "Oculus Quest Controller - Left");

                targetDevice = devices[0];

                if (targetDevice.name == "Oculus Touch Controller - Right")
                {
                    spawnedController = Instantiate(prefabRiftS1, transform);
                }
                else
                {
                    spawnedController = Instantiate(prefabRiftS2, transform);
                }
            }
            else
            {
                targetDevice = devices[0];
                prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

                if (prefab)
                {
                    spawnedController = Instantiate(prefab, transform);
                }
                else
                {
                    Debug.LogError("Did not find corresponding controller model.");
                    spawnedController = Instantiate(controllerPrefabs[0], transform);
                }
            }
        }

        spawnedHandModel = Instantiate(handModelPrefab, transform);
        handAnimator = spawnedHandModel.GetComponent<Animator>();
    }

    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryToInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }   
    }
}
