using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private float yInput;
    private float xInput;

    private const float baseSpeed = 5f;
    [SerializeField][Range(0f, 10f)]
    public float speed = baseSpeed;

    private const float baseStrikeRadius = 1f;
    public float strikeRadius = baseStrikeRadius;
    public bool notStriking = true;
    public bool barCanRaise = true;
    private bool hasAlarmed = false;
    public bool canmove = true;

    public Animator anim;
    private Rigidbody2D rb;
    public BoxCollider2D bc;
    public StrikeObject strike;
    private SpriteRenderer sr;
    private Manager gameManager;
    public Text hptext;

    private int health = 100;
    public int curMeter = 0;
    public int maxMeter = 150;

    public int instabilityRate = 2;

    private float invulTimer = 1f;
    private bool isInvul = false;
    private float isInvulUntil = 0f;

    public AudioClip strikeSound;
    public AudioClip alarmSound;
    public AudioClip deathSound;


    private void Awake()
    {
        gameManager = FindObjectOfType<Manager>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(ChangeMeter());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) Application.Quit();
        if(canmove) DetectInput();
        if(notStriking) UpdateStrikePosition();
        if (isInvul) CheckInvul();
    }

    private void DetectInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            if (notStriking) Strike();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ToggleEnrage();
        }
    }

    private void ExecuteMovement(float x, float y)
    {
        bool isMoving = x > 0.1f || x < -0.1f || y > 0.1f || y < -0.1f;
        anim.SetBool("IsMoving", isMoving);

        Vector3 test = new Vector3(x * speed, y * speed, 0);
        rb.MovePosition(transform.position + test * Time.deltaTime);
        //Vector3 newPos = new Vector3(speed * x, speed * y, 0) * Time.deltaTime;
        //transform.position += newPos;
    }
    
    private void Strike()
    {
        notStriking = false;
        strike.gameObject.SetActive(true);
        strike.ChooseStrike(StatsTracker.clawtier);
        AudioSource.PlayClipAtPoint(strikeSound, transform.position);
    }

    private void UpdateStrikePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0f;

        Vector3 newPos = mousePos - transform.position;
        newPos = Vector3.ClampMagnitude(newPos, strikeRadius);

        float angle = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;

        strike.transform.SetPositionAndRotation(transform.position + newPos, Quaternion.Euler(new Vector3(0f, 0f, angle-180f)));
    }

    private IEnumerator ChangeMeter()
    {
        while (true)
        {
            if (gameManager.curState == Manager.GameState.PLAYERNORMAL)
            {
                curMeter += instabilityRate;
            }
            else if (gameManager.curState == Manager.GameState.PLAYERENRAGED)
            {
                curMeter -= instabilityRate;
            }


            if (curMeter >= maxMeter)
            {
                gameManager.GameOver();
                AudioSource.PlayClipAtPoint(deathSound, transform.position);

            }
            else if (curMeter <= 0)
            {
                ToggleEnrage();
                if (health + 20 >= 100) health = 100;
                else health += 20;
                hptext.text = health.ToString("000");
            }
            
            if(curMeter >= 100 && hasAlarmed == false)
            {
                AudioSource.PlayClipAtPoint(alarmSound, transform.position);
                hasAlarmed = true;
            }
            yield return new WaitForSeconds(0.5f);

            if(barCanRaise == false)
            {
                yield return new WaitUntil(() => barCanRaise == true);
            }
            if (!canmove) instabilityRate = 0;
        }
    }

    private void FixedUpdate()
    {
        if(canmove) ExecuteMovement(xInput, yInput);
    }

    public void DamageMe(int damage)
    {
        if (isInvul) return;

        health -= damage;
        hptext.text = health.ToString("000");
        sr.color = new Color(255, 255, 255, .5f);
        isInvulUntil = Time.time + invulTimer;
        isInvul = true;

        speed += 2f;

        if(health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            gameManager.GameOver();
        }
    }

    private void CheckInvul()
    {
        if (isInvulUntil < Time.time)
        {
            isInvul = false;
            sr.color = new Color(255, 255, 255, 255);
            speed -= 2f;
        }
    }

    private void ToggleEnrage()
    {
        if(gameManager.curState == Manager.GameState.PLAYERNORMAL)
        {
            if (curMeter < 50) return;

            anim.SetBool("isEnraged", true);
            gameManager.curState = Manager.GameState.PLAYERENRAGED;

            speed += 3f;
            StatsTracker.clawdamage += 6;
            strikeRadius += 0.25f;
        }
        else if(gameManager.curState == Manager.GameState.PLAYERENRAGED)
        {
            anim.SetBool("isEnraged", false);
            gameManager.curState = Manager.GameState.PLAYERNORMAL;
            if (curMeter < 100) hasAlarmed = false;
            speed -= 3f;
            StatsTracker.clawdamage -= 6;
            strikeRadius -= 0.25f;
        }
    }
}
