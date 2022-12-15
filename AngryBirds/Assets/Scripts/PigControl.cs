using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigControl : MonoBehaviour
{
    public float MaxSpeed = 10f;
    public float MinSpeed = 5f;
    private SpriteRenderer render;
    public Sprite hurt;
    public GameObject boom;
    public GameObject score;
    public AudioClip[] PigAudios;
    public AudioClip BirdCollision;
    public bool isPig=false;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag=="Player") {
            GameManager.Instance.AudioPlay(BirdCollision);
        }
        //Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > MaxSpeed) { //相对速度大于最大速度
            Dead();
        } 
        else if (collision.relativeVelocity.magnitude < MaxSpeed&& collision.relativeVelocity.magnitude > MinSpeed) {
            render.sprite = hurt;
            GameManager.Instance.AudioPlay(PigAudios[0]);
        }
    }
    public void Dead() {
        if (isPig) {
            GameManager.Instance.Pigs.Remove(this);
        }
        Destroy(gameObject,0.1f);
        GameManager.Instance.AudioPlay(PigAudios[1]);
        GameObject clone=Instantiate(boom,transform.position,Quaternion.identity);
        Destroy(clone, 0.3f);
        clone = Instantiate(score, transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(clone, 1f);
    }
}
