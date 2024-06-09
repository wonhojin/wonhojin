using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // 속도
    public float power; // 파워 (총알)
    public float maxShotDelay; // 총알 발사 딜레이
    public float curShotDelay; // 총알 충전 딜레이
    public bool isTouchTop; // 천장에 닿으면 멈춤
    public bool isTouchBottom; // 바닥에 닿으면 멈춤
    public bool isTouchRight; // 오른쪽에 닿으면 멈춤
    public bool isTouchLeft; // 왼쪽에 닿으면 멈춤
    
    public GameObject bulletObjA; // 총알A생성
    public GameObject bulletObjB; //총알B생성

    Animator anim; // 애니메이션
    void Awake() // 애니메이션
    {
        anim = GetComponent<Animator>();
    }
    void Update() // 메인
    {
        Move(); // 플레이어 이동 
        Fire(); // 플레이어 총알 발사
        Reload(); // 플레이어 장전
    }

    void Move() //플레이어 이동
    {
        float h = Input.GetAxisRaw("Horizontal"); // 수평 값 생성, 체크
        if((isTouchRight && h == 1)||(isTouchLeft && h == -1))
            h = 0;
        float v = Input.GetAxisRaw("Vertical"); // 수직 값 생성, 체크
        if((isTouchTop && v == 1)||(isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position; // 플레이어 이동 로직
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; 
        transform.position = curPos + nextPos;

        if(Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")) { // 애니메이션
            anim.SetInteger("Input", (int)h);
        }

    }

    void Fire() //발사 로직
    {
        if(!Input.GetButton("Fire1"))
            return;
        if(curShotDelay < maxShotDelay)
            return;
        
        switch(power) {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation); //Instantiate함수(오브젝트, 위치, 방향)
                Rigidbody2D rigid= bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right*0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left*0.1f, transform.rotation);
                Rigidbody2D rigidR= bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL= bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right*0.35f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position , transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left*0.35f, transform.rotation);
                Rigidbody2D rigidRR= bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC= bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL= bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }

        curShotDelay = 0;
    }

    void Reload() // 장전 로직
    {
        curShotDelay += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision){ // 벽(boder) 닿음 체크
        if(collision.gameObject.tag =="Border"){
            switch(collision.gameObject.name){
                case "Top":
                isTouchTop = true;
                break;
                case "Bottom":
                isTouchBottom = true;
                break;
                case "Right":
                isTouchRight = true;
                break;
                case "Left":
                isTouchLeft = true;
                break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision){ // 벽(boder) 닿음 체크
        if(collision.gameObject.tag =="Border"){
            switch(collision.gameObject.name){
                case "Top":
                isTouchTop = false;
                break;
                case "Bottom":
                isTouchBottom = false;
                break;
                case "Right":
                isTouchRight = false;
                break;
                case "Left":
                isTouchLeft = false;
                break;

        }
    }
}
}
