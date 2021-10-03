using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeObject : MonoBehaviour
{
    public PlayerScript player;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    private void EndStrike()
    {
        player.notStriking = true;
        gameObject.SetActive(false);
        anim.SetTrigger("reset");
    }

    public void ChooseStrike(int striketier)
    {
        switch (striketier)
        {
            case 0:
                anim.SetTrigger("strike1");
                break;
            case 1:
                anim.SetTrigger("strike2");
                break;
            case 2:
                anim.SetTrigger("strike3");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBasic"))
        {
            collision.GetComponent<BasicImp>().DamageMe();
        }
        else if (collision.CompareTag("EnemyFire"))
        {
            collision.GetComponent<FireballImp>().DamageMe();
        }
        else if (collision.CompareTag("EnemyCharge"))
        {
            collision.GetComponent<ImpBull>().DamageMe();
        }
    }

}
