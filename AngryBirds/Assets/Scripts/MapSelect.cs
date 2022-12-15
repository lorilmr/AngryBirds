using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    public int StarsNum;
    private bool isSelect = false;
    public GameObject lockPanel;
    public GameObject starPanel;
    public GameObject panel;
    public Text StarsText;

    public int StartNum=1;
    public int EndNum = 3;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("totalNum",0)>=StarsNum) {
            //当前关卡可以解锁
            isSelect = true;
        }
        if (isSelect) {
            lockPanel.SetActive(false);
            starPanel.SetActive(true);
            //星星数值显示todo
            int count = 0;
            for (int i=StartNum;i<=EndNum;i++) {
                count += PlayerPrefs.GetInt("L"+i.ToString(),0);
                StarsText.text = count.ToString()+"/9";
            }
        }
    }
    public void MapClick() {
        if (isSelect) {
            panel.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }
    public void ReturnBtnClick()
    {
        //SceneManager.LoadScene(1);
        panel.SetActive(false);
        transform.parent.gameObject.SetActive(true);
    }
}
