using UnityEngine;

public class Death : MonoBehaviour
{
    private Stats sts;
    private Resources r;
    // Start is called before the first frame update
    void Start()
    {
        sts = GetComponent<Stats>();
        r = GameObject.Find("GameController").GetComponent<Resources>();
    }

    public void UnitDeath()
    {
        if (tag == "enemy")
        {
            r.resources += sts.gold;
        }
        else if (tag == "ally")
        {
            GetComponent<UnitTile>().tile.GetComponent<TileParams>().activeUnit = false;
        }
        Destroy(transform.Find("HitBox").gameObject);
        GetComponent<UnitController>().GetComponent<Animator>().SetBool("Death", true);
        GetComponent<UnitController>().setCanMove(false);
        Destroy(gameObject, 3.5f);
    }

}