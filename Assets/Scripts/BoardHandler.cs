using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour
{
    //public static float BOARD_WIDTH = 5.0F;
    //public static float BOARD_HEIGHT = 0.2F;
    //public static float BOARD_DEPTH = 5.0F;
    //public static float BORDER_MARGIN = .2F;
    private static readonly int N = GlobalVariables.N;
    private static readonly float TILE_SIZE = GlobalVariables.TILE_SIZE;
    private static readonly float SLIDE_AMOUNT = GlobalVariables.SLIDE_AMOUNT;
    private TileHandler[,] board;
    [SerializeField] private GameObject tile;
    [SerializeField] RaumschachHandler parentRaumschach;
    private bool isActive;
    void Awake()
    {
        //gameObject.transform.localScale = new Vector3(BOARD_WIDTH + 2 * BORDER_MARGIN, BOARD_HEIGHT, BOARD_DEPTH + 2 * BORDER_MARGIN);
        //Vector3 relativeCorner = new Vector3(
        //    -BOARD_WIDTH/2, 
        //    BOARD_HEIGHT/2,
        //     -BOARD_DEPTH/2);

        //Vector3 tileOffset = new Vector3(
        //    TileHandler.TILE_WIDTH / 2,
        //    TileHandler.TILE_HEIGHT / 2,
        //    TileHandler.TILE_DEPTH / 2);

        board = new TileHandler[N,N];
        for(int i = 0; i < N; i++) {
            for(int j = 0; j < N; j++) {
                Vector3 relativeTilePosition = new Vector3(TILE_SIZE*i, 0, TILE_SIZE*j);
                GameObject newTile = Instantiate(tile, gameObject.transform, true);
                newTile.transform.position = gameObject.transform.position + relativeTilePosition;
                newTile.TryGetComponent(out TileHandler tileHandler);
                board[i, j] = tileHandler;
                tileHandler.SetColor((i + j) % 2 == 0);
                tileHandler.SetParent(this);
            }
        }
        for(int i = 0; i<N; i++) {
            for( int j = 0; j < N;j++) {
                board[i, j].AddThreat(board[i,j]);
                for(int k = 0; k < N;k++) {

                    //if(i!=k)board[i, j].AddThreat(board[k, j]);
                    //if(i+j-k<n && i+j-k>=0) board[i, j].AddThreat(board[k, i+j-k]);
                    //if(i-j+k<n && i-j+k>=0) board[i, j].AddThreat(board[i-j+k, k]);

                    //more operation efficient to do here than in tile handler

                    if (j != k) board[i, j].AddThreat(board[i, k]);
                    if (i != k) board[i, j].AddThreat(board[k, j]);
                    if (i + j - k < N && i + j - k >= 0 && (i != k || j != i + j - k)) board[i, j].AddThreat(board[k, i + j - k]);
                    if (i - j + k < N && i - j + k >= 0 && (i != i - j + k || j != k)) board[i, j].AddThreat(board[i - j + k, k]);
                }
            }
        }

        isActive = false;
    }
    public TileHandler[,] getBoard() {
        return board;
    }

    public void SetParentRaumschach(RaumschachHandler r) {
        parentRaumschach = r;
    }

    public void SetActive(bool isActive) {
        if(isActive == this.isActive) { return; }
        if(isActive) {
            parentRaumschach.SetAllActive(false);
        }
        foreach(TileHandler t in board) {
            t.SetActive(isActive);
        }
        gameObject.transform.position += isActive ? new Vector3(SLIDE_AMOUNT, 0, 0) : new Vector3(-SLIDE_AMOUNT, 0, 0);
        this.isActive = isActive;
    }

}
