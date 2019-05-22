using System.Collections;
using UnityEngine;

public class GoldGen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MoneyBag;
    public float CoolDown = 3;

    public int numBags;
    public int maxBags;

    private int nrows;
    private int ncols;

    private bool generatingBag;

    private GameObject tile;

    public void initGold()
    {
        nrows = GameManager.instance.board.nrows;
        ncols = GameManager.instance.board.ncols;

        maxBags = nrows * ncols / 2;
        maxBags = maxBags < 1 ? 1 : maxBags;
    }

    public void Update()
    {
        if (!generatingBag)
        {
            generatingBag = true;
            StartCoroutine(genGold());
        }
    }

    private IEnumerator genGold()
    {
        if (numBags < maxBags)
        {
            int startingCol = GameManager.instance.board.startingCol;
            int x = (int)Random.Range(0, nrows);
            int z = (int)(Random.Range(0, ncols) + startingCol);

            tile = GameManager.instance.board.board[x * ncols + z];
            if (tile)
            {

                while (tile.GetComponent<TileParams>().tileGoldBag != null)
                {
                    x = (int)Random.Range(0, nrows);
                    z = (int)(Random.Range(0, ncols) + startingCol);
                    tile = GameManager.instance.board.board[x * ncols + z];
                }

                GameObject MB = Instantiate(MoneyBag, new Vector3(x, 10, z), transform.rotation);
                //MB.transform.Rotate(-90, 0, 0);
                MB.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
                MB.AddComponent<MoneyBagController>();
                tile.GetComponent<TileParams>().tileGoldBag = MB;

                numBags += 1;
            }
        }
        yield return new WaitForSecondsRealtime(CoolDown);
        generatingBag = false;
    }
}
