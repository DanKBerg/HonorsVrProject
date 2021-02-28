using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DevelopmentQuit : MonoBehaviour
{
    public XRController leftHand;
    public InputHelpers.Button quitButton;
    public float activationThreshold;

    // Update is called once per frame
    void Update()
    {
        if (CheckIfActivated(leftHand))
        {
            Application.Quit();
            Debug.Log("Ended Session!");
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, quitButton, out bool isActivated);
        return isActivated;
    }
}
