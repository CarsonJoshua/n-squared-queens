using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : ObjectInteractable
{
    [SerializeField] TileHandler tileHandler;
    public override void Interact() {
        tileHandler.Interact();
    }
}
