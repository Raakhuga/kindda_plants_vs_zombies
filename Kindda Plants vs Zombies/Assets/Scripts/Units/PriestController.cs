﻿using System.Collections;
using UnityEngine;

public class PriestController : MonoBehaviour
{
    public AudioClip Death;
    public AudioSource Source;

    public float coolDown = 2;
    public float currentGold = 0;
    public float addGold = 5;
    public float maxGold = 25;
    public GameObject MoneyBag;

    private GameObject MB;
    private Stats sts;
    private Animator anim;
    private bool generating = false;
    private Renderer render;

    void Start()
    {
        Source.clip = Death;
        sts = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        anim.SetBool("FullGold", false);

        Vector3 pos = new Vector3(transform.position.x, 1.75f, transform.position.z);
        //Vector3 camPos = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        MB = Instantiate(MoneyBag, pos, transform.rotation);
        render = MB.transform.Find("money_bag").GetComponent<Renderer>();
        MB.transform.parent = transform;
        //MB.transform.LookAt(camPos);
        //MB.transform.Rotate(-90, 0, 0);
        MB.transform.localScale *=0.75f;
        render.enabled = false;
    }

    void Update()
    {
        if (currentGold < maxGold)
        {
            anim.SetBool("FullGold", false);
            if (!generating)
            {
                StartCoroutine(generateGold());
            }
        }
        else
        {
            anim.SetBool("FullGold", true);
            if(!render.enabled) render.enabled = true;

        }
    }

    IEnumerator generateGold()
    {
        generating = true;
        yield return new WaitForSeconds(coolDown);
        currentGold += addGold;
        currentGold = currentGold < maxGold ? currentGold : maxGold;
        generating = false;
    }

    public void takeGold()
    {
        GameManager.instance.resources.resources += currentGold;
        currentGold = 0;
        addGold += 1;
        maxGold = addGold * ratio;
        MB.SetActive(false);
    }

    public void deathSound() 
    {
        Source.Play();
    }
}

        addGold += 1;
        maxGold = addGold * ratio;
        MB.GetComponent<MoneyBagSpinner>().SetDeactive();