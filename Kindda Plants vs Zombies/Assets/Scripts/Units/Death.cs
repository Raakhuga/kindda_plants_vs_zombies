using UnityEngine;

public class Death : MonoBehaviour
{
    public void UnitDeath()
    {
        Stats sts = GetComponent<Stats>();
        sts.vel = 0;
        if (tag == "enemy")
        {
            //GameManager.instance.resources.resources += sts.gold;
            GameManager.instance.enemyGenerator.numEnemiesWave--;
        }
        else if (tag == "ally")
        {
            TileParams tileParams = GetComponent<UnitTile>().tile.GetComponent<TileParams>();
            tileParams.tileUnit = null;
        }

        if (transform != null && transform.Find("HitBox") != null)
        {
            Destroy(transform.Find("HitBox").gameObject);
            if (GetComponent<Animator>() != null)
            {
                Animator anim = GetComponent<Animator>();
                if (paramInAnimator(anim, "canMove"))
                {
                    anim.SetBool("canMove", false);
                }
                anim.SetBool("Death", true);
            }
            
            Destroy(gameObject, 3.5f);
        }
    }

    bool paramInAnimator(Animator anim, string pname)
    {
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == pname)
            {
                return true;
            }
        }
        return false;
    }
}