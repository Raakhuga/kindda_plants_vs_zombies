using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public GameObject HBar;
	private Image HBfill;
	private GameObject newHBar;
	private float InitialHealth;
	private float currentHealth;
	private Stats sts;
	private Camera mainCamera;
	private bool damaging;
    // Start is called before the first frame update
    void Start()
    {
        sts = GetComponent<Stats>();
        InitialHealth = sts.health;
        currentHealth = InitialHealth;
        mainCamera = Camera.main;
        Vector3 pos = new Vector3(transform.position.x, 2.5f, transform.position.z);
		newHBar = Instantiate(HBar, pos, transform.rotation);
		newHBar.transform.parent = transform;
		Vector3 camPos = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
		newHBar.transform.LookAt(camPos);
		newHBar.SetActive(false);
		HBfill = newHBar.transform.Find("HealthBar_full").gameObject.GetComponent<Image>();
		damaging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sts.health != InitialHealth){
        	if (!newHBar.activeSelf) {
        		newHBar.SetActive(true);
        	}
        	float pct = sts.health / InitialHealth;
        	if(!damaging && currentHealth != sts.health) StartCoroutine(changeValue(pct));
        }
    }

    private IEnumerator changeValue(float pct) 
    {
    	damaging = true;
    	float initPct = HBfill.fillAmount;
    	float speed = 0.2f;
    	for(float elapsed = 0; elapsed < speed; elapsed += Time.deltaTime) {
    		//elapsed += Time.deltaTime;
    		HBfill.fillAmount = Mathf.Lerp(initPct, pct, elapsed / speed);

    		yield return null;
    	}
    	currentHealth = sts.health;
    	damaging = false;
    	//HBfill.fillAmount = pct;
    }
}
