using System.Collections;
using UnityEngine;

public class GoldGen : MonoBehaviour
{
    public GameObject MoneyBag;
    public float CoolDown = 10;

    public int numBags;
    private int maxBags;

    private int nrows;
    private int ncols;

    private bool generatingBag;

    private GameObject tile;

    public void initGold()
    {
        StopAllCoroutines();
        nrows = GameManager.instance.board.nrows;
        ncols = GameManager.instance.board.ncols;

        maxBags = nrows * ncols / 2;
        maxBags = maxBags < 1 ? 1 : maxBags;

        numBags = 0;
        generatingBag = false;
    }

    public void stopCoroutines()
    {
        StopAllCoroutines();
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
            int x = Random.Range(0, nrows);
            int z = Random.Range(0, ncols);

            tile = GameManager.instance.board.board[x * ncols + z];
            if (tile)
            {
                while (tile.GetComponent<TileParams>().tileGoldBag != null)
                {
                    x = Random.Range(0, nrows);
                    z = Random.Range(0, ncols);
                    tile = GameManager.instance.board.board[x * ncols + z];
                }

                GameObject MB = Instantiate(MoneyBag, new Vector3(x, 10, z + startingCol), transform.rotation);
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
