using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    public float resources = 150;

    private void initLevelParams()
    {
        switch (GameManager.instance.currentLvl)
        {
            case 1:
                Debug.Log("Resources Lvl 1");
                resources = 50;
                break;
            case 2:
                Debug.Log("Resources Lvl 2");
                resources = 100;
                break;
            case 3:
                Debug.Log("Resources Lvl 3");
                resources = 150;
                break;
            case 4:
                Debug.Log("Resources Lvl 4");
                resources = 50;
                break;
            case 5:
                Debug.Log("Resources Lvl 5");
                resources = 50;
                break;
            default:
                Debug.Log("Resources default");
                resources = 150;
                break;
        }
    }

    public void initResources()
    {
        initLevelParams();
    }
}
