using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardHandlerOld : MonoBehaviour
{
    [SerializeField] public int n;
    [SerializeField] public int d;
    [SerializeField] public GameObject tile;
    [SerializeField] public float tileSpace;

    TileHandlerOld[][][] board;



    //private void Awake() {
    //    this.gameObject.TryGetComponent(out Button button);
    //    button.PressButton += CreateAllTiles;
    //}

    //public void CreateAllTiles() {//TODO clear board if recalled, and track tiles + subscribe to tile events, maybe embed coord data into tile?
    //    if (d > 3) throw new System.Exception("Too many dimensions");
    //    tile.TryGetComponent(out Renderer r);
    //    Vector3[] transforms3D = {new Vector3(r.bounds.size.x+tileSpace,0,0), new Vector3(0,0,r.bounds.size.z+tileSpace), new Vector3(0,r.bounds.size.y+tileSpace,0)};
    //    CreateTileRow(n, gameObject.transform.position + new Vector3(3, 1, 3), transforms3D.Take(d).ToArray());
    //}

    //private void CreateTileRow(int n, Vector3 pos, Vector3[] transforms) {
    //    if (transforms.Length == 0) {
    //        Instantiate(tile, pos, Quaternion.identity);
    //        return;
    //    }
    //    Vector3 currentPos = pos;
    //    for(int i = 0; i < n; i++) {
    //        CreateTileRow(n, currentPos, transforms.Skip(1).Take(transforms.Length - 1).ToArray());
    //        currentPos += transforms[0];
    //    }
    //}
    public void CreateAllTiles() {//TODO clear board if recalled, and track tiles + subscribe to tile events, maybe embed coord data into tile?
        if (d > 3) throw new System.Exception("Too many dimensions");
        ClearBoard();
        if (d == 0) return;
        if (d == 1) board = new TileHandlerOld[n][][];//TODO fix
        tile.TryGetComponent(out Renderer r);
        Vector3[] transforms3D = { new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0) };
        CreateTileRow(n, 
            gameObject.transform.position + new Vector3(3, 1, 3), 
            new Vector3(), 
            new Vector3(r.bounds.size.x + tileSpace, r.bounds.size.y + tileSpace, r.bounds.size.z + tileSpace), 
            transforms3D.Take(d).ToArray());
    }

    private void CreateTileRow(int n, Vector3 startPos, Vector3 tilePos, Vector3 posMultiplier, Vector3[] transforms) {
        if (transforms.Length == 0) {
            GameObject g = Instantiate(tile, startPos+(multVect(tilePos, posMultiplier)), Quaternion.identity);

            return;
        }
        for (int i = 0; i < n; i++) {
            CreateTileRow(n, startPos, tilePos + (transforms[0]*i), posMultiplier, transforms.Skip(1).Take(transforms.Length - 1).ToArray());
        }
    }

    public void CreateTiles() {

    }

    public void ClearBoard() {
        if (board == null) return;
        everyTile(
            (TileHandlerOld t) => {
            Destroy(t.gameObject);
        });
    }

    private void everyTile(Action<TileHandlerOld> function) {
        everyTile((TileHandlerOld t) => { return true; }, function);
    }

    private void everyTile(Func<TileHandlerOld, Boolean> filter, Action<TileHandlerOld> function) {
        foreach (TileHandlerOld[][] taa in board) {
            foreach (TileHandlerOld[] ta in taa) {
                foreach (TileHandlerOld t in ta) {
                    if (filter(t)) {
                        function(t);
                    }
                }
            }
        }
    }


    private Vector3 multVect(Vector3 v1, Vector3 v2) {
        return new Vector3(v1.x*v2.x, v1.y*v2.y, v1.z*v2.z);
    }
}
