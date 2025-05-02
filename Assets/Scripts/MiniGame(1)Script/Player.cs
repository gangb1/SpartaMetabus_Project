using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;
    public float ForwardSpeed = 3f;
    public bool isDead = false;
    float DeathCooldown = 0f;

    bool isFlap = false;

    public bool GodMod = false;

    GameManager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameManager.Instance;

        animator = GetComponentInChildren<Animator>();              //getcomponetInChildren = 붙여져 있는 하위 오브젝트까지도 검색하는 명령어
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.Log("Not Founded Animator");

        if(_rigidbody == null)
            Debug.Log("Not Founded Rigidbody");
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            if(DeathCooldown <= 0)
            {
                //게임 재시작
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gamemanager.RestartGame();
                }

                }
            else
            {
                DeathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = ForwardSpeed;

        if(isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)                      //OnCollisionEnter = 충돌 시작 시 호출 / OnCollsionStay = 충돌이 유지되는 동안 호출 / OnCollsionExit = 충돌이 끝났을 때 호출            
    {
        if(GodMod) return;

        if (isDead) return;

        isDead = true;
        DeathCooldown = 1f;

        animator.SetInteger("IsDie", 1);
        gamemanager.GameOver();
    }
}
