using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask interactLayerMask;
    //[SerializeField] private LayerMask terminalLayerMask;
    [SerializeField] private float interactDistance = 4f;
    //[SerializeField] private Transform objectGrabPointTransform;

    //private ObjectGrabbable objectGrabbed;
    //private GameObject objectLooked;

    public UnityEvent PressKeyE;
    //public UnityEvent<GameObject> DropDisc;//used by disc acceptors
    ////public UnityEvent<GameObject> PickupDisc;//unused? move ui onto disc pointed at?
    ////public UnityEvent<ObjectGrabbable> LookAtGrabbable;
    //public UnityEvent<DiscData> LookAtDisc;
    //public UnityEvent LookAtNothing;


    ////BIG TODO raycast update system works badly, need to revert the pickup/interact system and maybe redo it.

    // Update is called once per frame
    void Update()//TODO should I raycast on update to see what I'm pointed at, to highlight/give disc info? maybe only raycast if no object held
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            PressKeyE?.Invoke();
        }
    }

    public void TryFindObject() {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance, interactLayerMask)) {
            if(raycastHit.transform.TryGetComponent(out ObjectInteractable interactable)) {
                interactable.Interact();
            }
        }

        //if (objectGrabbed != null) {
        //    objectGrabbed.Drop();
        //    //if (objectGrabbable.TryGetComponent(out DiscData disc) && Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHitTerminal, 20f, terminalLayerMask)) {
        //    //    if (raycastHitTerminal.transform.TryGetComponent(out DiscTerminal discTerminal)) {
        //    //        Debug.Log("Facing Disc Terminal");
        //    //        discTerminal.TakeDisc(objectGrabbable.gameObject);
        //    //    }
        //    //}
        //    if (objectGrabbed.TryGetComponent(out DiscData disc)) {
        //        DropDisc.Invoke(objectGrabbed.gameObject);
        //    }
        //    objectGrabbed = null;
        //}
        //else if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, interactLayerMask)) {
        //    if (raycastHit.transform.TryGetComponent(out objectGrabbed)) {
        //        //Debug.Log(objectGrabbable);
        //        objectGrabbed.Grab(objectGrabPointTransform);
        //        if (objectGrabbed.TryGetComponent(out DiscData disc)) {
        //            PickupDisc.Invoke(objectGrabbed.gameObject);
        //        }
        //    }
        //    else if (raycastHit.transform.TryGetComponent(out ObjectInteractable objectInteractable)) {
        //        objectInteractable.Interact();
        //    }
        //}
    }

    //public void TryPickupDropObject() {
    //    if(objectGrabbed == null) {
    //        if (objectLooked == null) return;
    //        objectLooked.TryGetComponent<ObjectGrabbable>(out objectGrabbed);
    //        //objectGrabbed = objectLooked;
    //        objectGrabbed.Grab(objectGrabPointTransform);
    //    }
    //    else {
    //        objectGrabbed.Drop();
    //        if (objectGrabbed.TryGetComponent(out DiscData disc)) {
    //            DropDisc.Invoke(objectGrabbed.gameObject);
    //        }
    //        objectGrabbed = null;
    //    }
    //}

    //public void TryInteract() {
    //    if (objectLooked == null) return;
    //    if(objectLooked.TryGetComponent(out ObjectInteractable interactable)) {
    //        interactable.Interact();
    //    }
    //}

    ////public void TargetObject(ObjectGrabbable obj) {
    ////    objectLooked = obj;
    ////}
    //public void TargetNothing() {
    //    objectLooked = null;
    //}
}
