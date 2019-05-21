using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBagSpinner : MonoBehaviour
{
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 35 * Time.deltaTime);
    }
}
