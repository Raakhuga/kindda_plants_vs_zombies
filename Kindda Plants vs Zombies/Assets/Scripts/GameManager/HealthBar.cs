using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject HBar;
    private Image HBfill;
    private GameObject newHBar;
    [SerializeField] private float InitialHealth;
    private Stats sts;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        sts = GetComponent<Stats>();
        InitialHealth = sts.health;
        mainCamera = Camera.main;
        Vector3 pos = new Vector3(transform.position.x, 1.6f, transform.position.z);
        newHBar = Instantiate(HBar, pos, transform.rotation);
        newHBar.transform.parent = transform;
        Vector3 camPos = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        newHBar.transform.LookAt(camPos);
        HBfill = newHBar.transform.Find("HealthBar_full").gameObject.GetComponent<Image>();
        newHBar.SetActive(false);
    }

    public void actHBar()
    {
        StartCoroutine(changeValue());
    }

    public IEnumerator changeValue()
    {
        float pct = sts.health / InitialHealth;
        float initPct = HBfill.fillAmount;
        float speed = 0.2f;
        if (!newHBar.activeSelf)
        {
            newHBar.SetActive(true);
        }
        for (float elapsed = 0; elapsed < speed; elapsed += Time.deltaTime)
        {
            HBfill.fillAmount = Mathf.Lerp(initPct, pct, elapsed / speed);
            //Debug.Log(elapsed);
            yield return null;
        }
        //if (tag == "enemy") Debug.Log("hooo");
        HBfill.fillAmount = pct;
    }

    public void destroyBar()
    {
        if (newHBar != null)
            Destroy(newHBar);
    }
}
