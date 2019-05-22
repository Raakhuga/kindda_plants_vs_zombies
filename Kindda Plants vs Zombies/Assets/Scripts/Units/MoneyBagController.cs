using UnityEngine;

public class MoneyBagController : MonoBehaviour
{
    public int gold = 25;

    private float yMin = 0;
    void Update()
    {
        if (transform.position.y > yMin)
        {
            transform.Translate(new Vector3(0, -1, 0) * 4 * Time.deltaTime);
            if (transform.position.y < yMin)
            {
                transform.SetPositionAndRotation(
                    new Vector3(transform.position.x, yMin, transform.position.z),
                    transform.rotation);
            }
        }
    }
}
