﻿using System.Collections;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public AudioClip Hit;
    public AudioClip Death;
    public AudioSource Source;

    private Stats sts;
    private Animator anim;

    private bool canMove;
    private bool attacking;

    private float attackCooldown;
    private string target; // Used in MeleeHit.cs
    private GameObject attackTarget;


    void Start()
    {
        sts = GetComponent<Stats>();
        anim = GetComponent<Animator>();

        canMove = true;
        attackCooldown = sts.AttackSpeed;

        if (transform.Find("Root/Spine1/Spine2/Chest/Neck1/Neck2/Neck3/Neck4/Head/UpperHead1/Flames") != null)
        {
            GameObject Flames = transform.Find("Root/Spine1/Spine2/Chest/Neck1/Neck2/Neck3/Neck4/Head/UpperHead1/Flames").gameObject;
            Renderer render = Flames.GetComponent<Renderer>();
            render.enabled = false;
        }

        target = "ally";

        Source.clip = Hit;
    }

    void Update()
    {
        if (transform.position.x < 0)
        {
            StartCoroutine(startAttack());
            if (!GameManager.instance.lostGame && GameManager.instance.enemyGenerator.numEnemiesWave != 0) {
                GameManager.instance.GameLost();
            }
            return;
        }
        if (attackTarget == null)
        {
            canMove = true;
        }
        else if (!attacking)
        {
            StartCoroutine(startAttack());
        }

        if (canMove)
        {
            anim.SetBool("CanMove", true);
            transform.Translate(Vector3.forward * sts.vel * Time.deltaTime);
        }

    }

    IEnumerator startAttack()
    {
        canMove = false;
        anim.SetBool("CanMove", false);
        attacking = true;
        yield return new WaitForSeconds(attackCooldown);
        anim.SetBool("Attack", true);
    }

    void ResetAttack()
    {
        Source.Play();
        anim.SetBool("Attack", false);
        if (attackTarget != null)
        {
            attackTarget.GetComponent<Stats>().health -= sts.dmg;
            //HealthBar = attackTarget.GetComponent<HealthBar>()
            attackTarget.GetComponent<HealthBar>().actHBar();
            // Kill unit if health <= 0
            if (attackTarget.GetComponent<Stats>().health <= 0)
            {
                attackTarget.GetComponent<HealthBar>().destroyBar();
                attackTarget.GetComponent<Death>().UnitDeath();
                attackTarget = null;
            }
        }
        attacking = false;
    }

    public void meleeAttack(Collider col)
    {
        if (canMove)
        {
            attackTarget = col.gameObject.transform.parent.gameObject;
            StartCoroutine(startAttack());
        }
    }

    public void deathSound()
    {
        Source.clip = Death;
        Source.Play();

        if (transform.Find("Root/Spine1/Spine2/Chest/Neck1/Neck2/Neck3/Neck4/Head/UpperHead1/Flames") != null)
        {
            GameObject Flames = transform.Find("Root/Spine1/Spine2/Chest/Neck1/Neck2/Neck3/Neck4/Head/UpperHead1/Flames").gameObject;
            Renderer render = Flames.GetComponent<Renderer>();
            render.enabled = false;
        }
    }

    void playHit()
    {
        Source.Play();
    }

    void toggleFire()
    {
        if (transform.Find("Root/Spine1/Spine2/Chest/Neck1/Neck2/Neck3/Neck4/Head/UpperHead1/Flames") != null)
        {
            GameObject Flames = transform.Find("Root/Spine1/Spine2/Chest/Neck1/Neck2/Neck3/Neck4/Head/UpperHead1/Flames").gameObject;
            Renderer render = Flames.GetComponent<Renderer>();
            render.enabled = !render.enabled;
            //if (render.enabled) ResetAttack();
        }
    }
}
