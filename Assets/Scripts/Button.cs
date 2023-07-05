using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : ObjectInteractable
{
    public UnityEvent PressButton;
    public override void Interact() {
        PressButton?.Invoke();
    }
}
