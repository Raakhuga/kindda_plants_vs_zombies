using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBagSpinner : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Sound;
    private GameObject tile;
    void Start()
    {
        Source.clip = Sound;
    }

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 35 * Time.deltaTime);
    }

    public void DestroyBag()
    {
        Source.Play();
        tile.GetComponent<TileParams>().tileGoldBag = null;
        transform.Find("money_bag").GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 2f);
    }

    public void SetDeactive()
    {
        Source.Play();
        transform.Find("money_bag").GetComponent<Renderer>().enabled = false;
    }

    public void SetTile(GameObject tile)
    {
        this.tile = tile;
    }
}
