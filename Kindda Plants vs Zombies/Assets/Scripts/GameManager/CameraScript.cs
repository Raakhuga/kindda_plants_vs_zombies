using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject lostGameMenu;

    void Update()
    {
        if (GameManager.instance.lostGame)
        {
            lostGameMenu.SetActive(true);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(4.5f, 20f, -7f), 0.1f * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(60f, 0f, 0f), 0.1f * Time.deltaTime);
        }
        else
        {
            lostGameMenu.SetActive(false);
        }
    }
}
