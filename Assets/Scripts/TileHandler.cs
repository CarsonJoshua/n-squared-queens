using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : ObjectInteractable
{
    private List<TileHandler> tilesThreatenable;
    private List<TileHandler> tilesThreatening;

    private BoardHandler parentBoard;
    private bool isActive;

    private Boolean isThreatened;
    private Boolean hasPiece;

    public readonly static float TILE_WIDTH = GlobalVariables.TILE_SIZE;
    public readonly static float TILE_HEIGHT = GlobalVariables.TILE_SIZE/5;
    public readonly static float TILE_DEPTH = GlobalVariables.TILE_SIZE;

    [SerializeField] private Material tileWhite;
    [SerializeField] private Material tileBlack;
    [SerializeField] private Material tileWhiteThreatened;
    [SerializeField] private Material tileBlackThreatened;
    [SerializeField] private Material tileWhiteInvalid;
    [SerializeField] private Material tileBlackInvalid;

    private new Renderer renderer;
    private bool isWhite;
    private bool isInvalid;


    [SerializeField] private GameObject piece;

    private void Awake() {
        gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x * TILE_WIDTH,
            gameObject.transform.localScale.y * TILE_HEIGHT,
            gameObject.transform.localScale.z * TILE_DEPTH);


        tilesThreatening = new List<TileHandler>();
        tilesThreatenable = new List<TileHandler>();
        isThreatened = false;
        hasPiece = false;
        isActive = false;

        renderer = gameObject.GetComponent<Renderer>();
        piece.SetActive(false);
    }
    public override void Interact() {
        if(!isActive) {
            parentBoard.SetActive(!isActive);
            return;
        }
        if (hasPiece) {
            RemovePiece();
            foreach (var tile in tilesThreatenable) tile.Unthreaten(this);
        }
        else {
            AddPiece();
            foreach (var tile in tilesThreatenable) tile.Threaten(this);
            //foreach(TileHandler tileHandler in tilesThreatening)
            //Debug.Log(tileHandler.gameObject);
        }
    }

    public void Threaten(TileHandler threatener) {
        if (!isThreatened) BecomeThreatened();
        tilesThreatening.Add(threatener);
        if(tilesThreatening.Count > 1 && hasPiece) {
            BecomeInvalid();
        }

    }
    public void Unthreaten(TileHandler threatener) {
        tilesThreatening.Remove(threatener);
        if (tilesThreatening.Count == 0) BecomeUnthreatened();
        if(isInvalid&&!(tilesThreatening.Count > 1 && hasPiece)) {
            BecomeValid();
        }
    }

    private void BecomeThreatened() {
        isThreatened = true;//TODO change color
        renderer.material = isWhite?tileWhiteThreatened:tileBlackThreatened;
    }
    private void BecomeUnthreatened() {
        isThreatened = false;
        renderer.material = isWhite ? tileWhite : tileBlack;
    }
    private void BecomeInvalid() {
        isInvalid = true;
        renderer.material = isWhite?tileWhiteInvalid:tileBlackInvalid;
    }
    private void BecomeValid() {
        isInvalid = false;
        BecomeThreatened();
    }
    private void AddPiece() {
        hasPiece = true;
        piece.SetActive(true);
    }
    private void RemovePiece() {
        hasPiece = false;
        piece.SetActive(false);
    }
    public void AddThreat(TileHandler threat) {
        //if(!tilesThreatenable.Contains(threat))
        tilesThreatenable.Add(threat);
    }
    public void SetColor(bool white) {
        this.isWhite = white;
        if (white) {
            renderer.material = tileWhite;
        }
        else {
            renderer.material = tileBlack;
        }
    }

    public void SetParent(BoardHandler board) {
        parentBoard = board;
    }
    public void SetActive(bool isActive) {
        this.isActive = isActive;
    }
}
