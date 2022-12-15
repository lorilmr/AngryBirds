using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BirdControl : MonoBehaviour
{
    
    public float maxDistance = 1.5f;
    private bool isClick;
    public LineRenderer rightLine;
    public LineRenderer leftLine;
    public Transform rightPos;
    public Transform leftPos;
    public GameObject boom;
    public float smooth = 3;
    public AudioClip[] BirdAudios;
    public Sprite hurt;
    protected SpriteRenderer render;
    //拖尾
    protected TestMyTrail myTrail;
    [HideInInspector]
    public SpringJoint2D springJoint;
    protected Rigidbody2D rigid;//可被子类调用
    [HideInInspector]
    public bool CanTrag = false;
    private bool isFly = false;
    public bool isReleased=false;
    // Start is called before the first frame update
    private void Awake()
    {
        springJoint = GetComponent<SpringJoint2D>();
        rigid = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        if (CanTrag) { 
            isClick = true;
            rigid.isKinematic = true;
        }

    }
    private void OnMouseUp()
    {
        if (CanTrag) {
            GameManager.Instance.AudioPlay(BirdAudios[0]);
            isClick = false;
            rigid.isKinematic = false;
            Invoke("Fly", 0.1f);
            //禁用划线组件
            rightLine.enabled = false;
            leftLine.enabled = false;
            CanTrag = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        //控制拉线的最大距离
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x,transform.position.y, 0);
            if (Vector3.Distance(transform.position, rightPos.position) > maxDistance) {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位化向量
                pos *= maxDistance;//最大位移向量
                transform.position = pos + rightPos.position;
            }
            DrawLine();
        }
        //相机跟随
        float birdPosX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(Mathf.Clamp(birdPosX, 0, 15),0,-10),smooth*Time.deltaTime);//限制x的值,在0-15之间返回原值,超出返回0或者15
        //技能释放
        //如果点击了UI界面则不能释放技能
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}
        if (isFly) {
            if (Input.GetMouseButtonDown(0)&&! EventSystem.current.IsPointerOverGameObject()) {
                ShowSkills();
            }
        }
    }
    void Fly() {
        isFly = true;
        isReleased = true;
        GameManager.Instance.AudioPlay(BirdAudios[1]);
        springJoint.enabled = false;
        myTrail.StartTrails();
        Invoke("Next",5f);
    }
    //划线
    void DrawLine() {
        rightLine.enabled = true;
        leftLine.enabled = true;
        //右边划线
        rightLine.SetPosition(0,rightPos.position);
        rightLine.SetPosition(1,transform.position);
        //左边划线
        leftLine.SetPosition(0, leftPos.position);
        leftLine.SetPosition(1, transform.position);
    }
    protected virtual  void Next()
    {
        GameManager.Instance.Birds.Remove(this);
        Destroy(gameObject);
        GameObject clone = Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(clone, 0.3f);
        GameManager.Instance.NextBird();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        myTrail.ClearTrails();
        if (collision.transform.tag == "Enemy") {
            BirdHurt();
            //Debug.Log("hurt");
        }
    }
    public virtual void ShowSkills() {//虚方法可被子类重写
        isFly = false;
    }
    public void BirdHurt() {
        render.sprite = hurt;
    }
}
