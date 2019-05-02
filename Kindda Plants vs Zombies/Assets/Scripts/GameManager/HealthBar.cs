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
	private Stats sts;
    // Start is called before the first frame update
    void Start()
    {
        sts = GetComponent<Stats>();
        InitialHealth = sts.health; 
               
    }

    // Update is called once per frame
    void Update()
    {
        if (sts.health != InitialHealth){
        	if(newHBar == null) {
        		Vector3 pos = new Vector3(transform.position.x, 2.5f, transform.position.z);
        		newHBar = Instantiate(HBar, pos, transform.rotation);
        		newHBar.transform.parent = transform;
        		HBfill = newHBar.transform.Find("HealthBar_full").gameObject.GetComponent<Image>();
        	}
        	float pct = sts.health / InitialHealth;
        	StartCoroutine(changeValue(pct));
        }
    }

    private IEnumerator changeValue(float pct) 
    {
    	float initPct = HBfill.fillAmount;
    	float elapsed = 0f;
    	float speed = 0.5f;

    	while (elapsed < speed) {
    		elapsed += Time.deltaTime;
    		HBfill.fillAmount = Mathf.Lerp(initPct, pct, elapsed / speed);
    		yield return null;
    	}
    	HBfill.fillAmount = pct;
    }
}
