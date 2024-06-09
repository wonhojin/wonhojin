using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; // 적 속도
    public int health; // 적 체력
    public Sprite[] sprites; // SpriteRenderer 에서 Sprites 변경을 위해

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake() //초기화 로직
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }

    void Onhit(int dmg)
    {
        health -= dmg;
        spriteRenderer.Sprite = sprites[1];
        Invoke("ReturnSprite",0.1f);
        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    void ReturnSprite() 
    {
        spriteRenderer = sprites[0];
    }

    void OnTriggerEnter2D(Collide2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);
        else if(collision.gameObject.tag == "PlayerBullet")
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            Onhit(bullet dmg);
    }
}
