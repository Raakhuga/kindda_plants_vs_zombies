using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MoneyBag;
    public float CoolDown;

    private float nrows;
    private float ncols;

    public void initGold() 
    {
    	StartCoroutine(genGold());
    }

    private IEnumerator genGold() 
    {
    	nrows = GameManager.instance.board.nrows;
        ncols = GameManager.instance.board.ncols;
    	float startingCol = GameManager.instance.board.startingCol;
    	float x = Random.Range(0, nrows);
    	float z = Random.Range(0, ncols) + startingCol;
    	GameObject MB = Instantiate(MoneyBag, new Vector3(x, 10, z), transform.rotation);
    	//MB.transform.Rotate(-90, 0, 0);
    	MB.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
    	MB.AddComponent<MoneyBagController>();
    	yield return new WaitForSecondsRealtime(CoolDown);
    }
}
