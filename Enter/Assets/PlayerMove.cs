using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    [SerializeField]
    GameObject cam;
    float h, v, angle;
    Rigidbody2D rigid;
    bool isdash, isfire;

    

    Camera characterCamera;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    ParticleSystem Bullet_Wall;

    

    

    [SerializeField]
    GameObject arm;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }
    void Start()
    {
        
    }

    
    void Update()
    {


        Vector3 MousePoint = Camera.main.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            -Camera.main.transform.position.z));
        
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        
            cam.transform.position =
            new Vector3(transform.position.x + (MousePoint.x * 0.3f),
            transform.position.y + (MousePoint.y * 0.3f),
            -10);
        
        Click();
    }
    private void FixedUpdate()
    {
        if (!isdash)
            rigid.velocity = new Vector2(h,v) * MoveSpeed;
    }
    private void Click()
    {
        if (Input.GetMouseButtonDown(1) && !isdash)
        {
            Debug.Log("Dash");
            StartCoroutine(Dash());
        }
        if (Input.GetMouseButton(0) && !isfire)
        {
            Debug.Log("Fire");
            StartCoroutine(Fire());
        }
    }
    IEnumerator Dash()
    {
        isdash = true;
        rigid.velocity = rigid.velocity * 1.7f;
            yield return new WaitForSeconds(0.5f);
        isdash = false;
    }
    IEnumerator Fire()
    {
        isfire = true;
        var bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            - arm.transform.position;

        direction.z = 0;

        bullet.transform.position = arm.transform.position;
        bullet.Shoot(direction);
        yield return new WaitForSeconds(0.3f);
        isfire = false;
    }
    public void BulletWallParticle(Vector2 BulletPosition)
    {
        Bullet_Wall.transform.position = BulletPosition;
        Bullet_Wall.Play();
    }
    public void LookMouseCursor()
    {
        
    }
}
