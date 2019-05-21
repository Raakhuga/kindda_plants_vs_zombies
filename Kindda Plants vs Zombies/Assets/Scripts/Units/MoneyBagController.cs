using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBagController : MonoBehaviour
{
    public int gold = 25;
    void Update()
    {
        if (transform.position.y > 0.35F) transform.Translate(new Vector3(0, -1, 0) * 4 * Time.deltaTime);
    }
}
