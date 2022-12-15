using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false;
    public Sprite LevelBg;
    public GameObject[] Stars;
    private Image Img;
    private void Awake()
    {
        Img = GetComponent<Image>();
    }
    private void Start()
    {
        if (transform.parent.GetChild(0).name == transform.name)
        {
            isSelect = true;
        }
        else {
            //判断后续关卡是否可以选择
            int preLevel=int.Parse(gameObject.name.Replace("L",""))-1;
            if (PlayerPrefs.GetInt("L"+preLevel.ToString())>0) {
                isSelect = true;
            }
        }

        if (isSelect) {
            Img.sprite = LevelBg;
            transform.Find("Num").gameObject.SetActive(true);

            int count = PlayerPrefs.GetInt(gameObject.name);//获取当前关卡的星星数量
            if (count>0) {
                for (int i=0;i<count;i++) {
                    Stars[i].SetActive(true);
                }
            }
        }
    }
    //
    public void LevelBtnClick()
    {
        if (isSelect) {
            PlayerPrefs.SetString("nowLevel",gameObject.name);//存储当前被选择的关卡的名字
            SceneManager.LoadScene(2);
        }
    }
}
