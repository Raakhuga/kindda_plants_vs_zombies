using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject[] board;

    public int nrows = 9;
    public int ncols = 5;

    public int maxrows = 10;
    public int maxcols = 5;

    public int startingCol = 0;

    public GameObject Tile1, Tile2;

    private Transform boardHolder;

    public void initBoard()
    {
        boardHolder = new GameObject("board").transform;

        nrows = nrows < maxrows ? nrows : maxrows;
        nrows = nrows < 1 ? 1 : nrows;
        ncols = ncols < maxcols ? ncols : maxcols;
        ncols = ncols < 1 ? 1 : ncols;
        board = new GameObject[nrows * ncols];

        startingCol = (maxcols - ncols) / 2;

        for (int i = 0; i < nrows; i++)
        {
            for (int j = 0; j < ncols; j++)
            {
                GameObject TyleType = (i + j) % 2 == 0 ? Tile1 : Tile2;
                GameObject tileInstance = Instantiate(TyleType, new Vector3(i, 0.0f, j + startingCol), Quaternion.identity, boardHolder) as GameObject;
                tileInstance.name = "tile " + i + " " + j;
                TileParams tileParamsTmp = tileInstance.GetComponent<TileParams>();
                tileParamsTmp.pi = i;
                tileParamsTmp.pj = j;
                tileParamsTmp.x = i;
                tileParamsTmp.y = j + startingCol;
                board[i * ncols + j] = tileInstance;
            }
        }
    }
}
