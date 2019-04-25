using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    public GameObject gameManager;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
