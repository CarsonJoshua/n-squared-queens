using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 10F)) {
                if (hit.transform.TryGetComponent(out ObjectInteractable interactable)) {
                    interactable.Interact();
                }
            }
        }
    }
}
