using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCanvas : MonoBehaviour
{
    public void selectUnit(int unitId)
    {
        GameManager.instance.gameInteraction.selectUnit(unitId);
    }
}
