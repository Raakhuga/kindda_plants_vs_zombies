using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject[] board;

    public int nrows = 9;
    public int ncols = 5;

    public int maxrows = 10;
    public int maxcols = 5;

    public GameObject Tile1, Tile2;

    private Transform boardHolder;

    public void initBoard()
    {
        boardHolder = new GameObject("board").transform;

        nrows = nrows < maxrows ? nrows : maxrows;
        ncols = ncols < maxcols ? ncols : maxcols;
        board = new GameObject[nrows * ncols];

        int startingX = (maxcols - ncols) / 2;

        for (int i = 0; i < nrows; i++)
        {
            for (int j = 0; j < ncols; j++)
            {
                GameObject TyleType = (i + j) % 2 == 0 ? Tile1 : Tile2;
                GameObject tileInstance = Instantiate(TyleType, new Vector3(i, 0.0f, j + startingX), Quaternion.identity, boardHolder) as GameObject;
                tileInstance.name = "tile " + i + " " + j;
                TileParams tileParamsTmp = tileInstance.GetComponent<TileParams>();
                tileParamsTmp.pi = i;
                tileParamsTmp.pj = j;
                tileParamsTmp.x = i;
                tileParamsTmp.y = j + startingX;
                board[i * ncols + j] = tileInstance;
            }
        }
    }
}
