using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileHandlerOld : ObjectInteractable
{
    public TileHandlerOld[] tilesThreatenable;
    private List<TileHandlerOld> tilesThreatening;
    private Boolean isThreatened;
    private Boolean hasPiece;

    private void Awake() {
        tilesThreatening = new List<TileHandlerOld>();
        isThreatened = false;
        hasPiece = false;
    }
    public override void Interact() {
        if(hasPiece) {
            foreach (var tile in tilesThreatenable) tile.Unthreaten(this);
            hasPiece = false;
            RemovePiece();
        }
        else {
            foreach (var tile in tilesThreatenable) tile.Threaten(this);
            hasPiece = true;
            AddPiece();
        }
    }

    public void Threaten(TileHandlerOld threatener) {
        if (!isThreatened) BecomeThreatened();
        tilesThreatening.Add(threatener);
    }
    public void Unthreaten(TileHandlerOld threatener) {
        tilesThreatening.Remove(threatener);
        if(tilesThreatening.Count==0)BecomeUnthreatened();
    }

    private void BecomeThreatened() {
        isThreatened = true;//TODO change color
    }
    private void BecomeUnthreatened() {
        isThreatened = false;
    }
    private void AddPiece() {
        //TODO
    }
    private void RemovePiece() {

    }
}
