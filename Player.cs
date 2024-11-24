using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed, rotationSpeed;

    Rigidbody2D rb;
    Vector2 moveVelocity;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;

    [SerializeField] float timeBtwShoot = 2;
    float shootTimer;


    // запускається на старті
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        shootTimer = timeBtwShoot;
    }


    // запускається при кожному кадрі
    void Update()
    {
        PlayerRotation();

        shootTimer += Time.deltaTime; ;

        if (Input.GetMouseButtonDown(0) && shootTimer >= timeBtwShoot)
        {
            Shoot();
            shootTimer = 0;
        }
    }
    //фізвзаємод з фікс інт
    private void FixedUpdate()
    {
        Move();
    }
    #region Base Function Move Turn

    
    void Move()
    {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        moveVelocity = moveInput.normalized * speed;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void PlayerRotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,
            rotation, rotationSpeed * Time.deltaTime);
    }
#endregion 
    void Shoot()
    {
        Instantiate(bullet, shootPos.position, shootPos.rotation);
    }

    




}


