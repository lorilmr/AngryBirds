using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<BirdControl> Birds;
    public List<PigControl> Pigs;
    public GameObject[] Stars;
    private Vector3 OriginPos;
    public static GameManager Instance;
    private int StarsNum=0;
    private int TotalNum = 10;//总关卡数

    //UI
    public GameObject Win;
    public GameObject Lose;
    private void Awake()
    {
        Instance = this;
        if (Birds.Count > 0)
        {
            OriginPos = Birds[0].transform.position;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //初始化
    private void Initialized()
    {
        for (int i = 0; i < Birds.Count; i++) {
            if (i==0) {
                Birds[i].transform .position= OriginPos;
                Birds[i].enabled = true;
                Birds[i].springJoint.enabled = true;
                Birds[i].CanTrag = true;
            }
            else
            {
                Birds[i].enabled = false;
                Birds[i].springJoint.enabled = false;
                Birds[i].CanTrag = false;
            }
        }  
    }
    public void NextBird() {
        if (Pigs.Count > 0)
        {
            if (Birds.Count > 0)
            {
                Initialized();//游戏继续
            }
            else {
                //输了
                Lose.SetActive(true);
            }
        }
        else {
            //赢了
            Win.SetActive(true);
            Invoke("ShowStars",0.5f);
        }
    }
    public void ShowStars() {
        StartCoroutine(Show());
    }
    IEnumerator Show() {
        for (; StarsNum <= Birds.Count; StarsNum++)
        {
            if (StarsNum >= Stars.Length) {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            Stars[StarsNum].SetActive(true);
        }
    }
    public void Repaly() {
        SaveStarsNum();
        SceneManager.LoadScene(2);
    }
    public void Home() {
        SaveStarsNum();
        SceneManager.LoadScene(1);
    }
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    public void SaveStarsNum() {
        if (StarsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"))) {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), StarsNum);//存储本关获得的星星数量
        }
        //存储所有关卡获得的星星个数
        int sum = 0;
        for (int i=1;i<=TotalNum;i++) {
            sum += PlayerPrefs.GetInt("L"+i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
        print(PlayerPrefs.GetInt("totalNum", sum));
    }
}
