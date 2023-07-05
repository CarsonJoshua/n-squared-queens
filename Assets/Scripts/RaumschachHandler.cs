using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaumschachHandler : MonoBehaviour
{
    [SerializeField] private GameObject board;

    private static readonly float BOARD_MARGIN = GlobalVariables.BOARD_MARGIN;
    private static readonly int N = GlobalVariables.N;
    private static readonly float SCALE = GlobalVariables.SCALE;
    TileHandler[][,] raumschach;
    BoardHandler[] boards;
    void Awake() {
        raumschach = new TileHandler[N][,];
        boards = new BoardHandler[N];
        for(int i = 0; i < N;i++) {
            GameObject boardLayer = Instantiate(board, gameObject.transform, true);
            boardLayer.transform.position += new Vector3(0, BOARD_MARGIN * i, 0);
            boardLayer.TryGetComponent(out BoardHandler boardHandler);
            raumschach[i]=boardHandler.getBoard();
            boards[i] = boardHandler;
            boardHandler.SetParentRaumschach(this);
        }
        for(int i = 0; i < N; i++) {
            for(int j = 0; j < N; j++) {
                for(int k = 0; k < N; k++) { 
                    for(int l = 0; l < N; l++) {
                        if (i != l) raumschach[i][j, k].AddThreat(raumschach[l][j, k]);
                    }
                }
            }
        }



        gameObject.transform.localScale=new Vector3(SCALE, SCALE, SCALE);
    }
    public void SetAllActive(bool isActive) {
        foreach(BoardHandler boardHandler in boards) {
            boardHandler.SetActive(isActive);
        }
    }
}
