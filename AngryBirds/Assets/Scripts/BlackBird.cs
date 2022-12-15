using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : BirdControl
{
    public List<PigControl> Blocks=new List<PigControl>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy") {
            Blocks.Add(collision.gameObject.GetComponent<PigControl>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Blocks.Remove(collision.gameObject.GetComponent<PigControl>());
        }
    }
    public override void ShowSkills()
    {
        base.ShowSkills();
        if (Blocks.Count > 0 && Blocks != null) {
            for (int i = 0; i < Blocks.Count; i++) {
                Blocks[i].Dead();
            }
        }
        OnClear();
    }
    void OnClear()
    {
        rigid.velocity = Vector3.zero;
        GameObject clone=Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(clone, 0.3f);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.ClearTrails();
    }
    protected override void Next()
    {
        GameManager.Instance.Birds.Remove(this);
        Destroy(gameObject);
        GameManager.Instance.NextBird();
    }
}
