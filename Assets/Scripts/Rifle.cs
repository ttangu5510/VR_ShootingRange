using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Rifle : Gun
{
    public InputActionProperty triggerAction;

    void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        if (triggerValue > 0.5f)
        {
            Fire();
        }
    }
}
