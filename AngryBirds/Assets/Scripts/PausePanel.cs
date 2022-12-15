using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject PauseBtn;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void RetryBtnClick() {
        Time.timeScale = 1;
        GameManager.Instance.Repaly();
    }
    public void HomeBtnClick()
    {
        GameManager.Instance.Home();
        Time.timeScale = 1;
    }
    //暂定按钮按下先播放动画
    public void PauseBtnClick() {
        PauseBtn.SetActive(false);
        Debug.Log("zz");
        anim.SetBool("isPause", true);

        if (GameManager.Instance.Birds.Count > 0) {
            //第一只小鸟是否还处于准备状态
            if (GameManager.Instance.Birds[0].isReleased==false){
                //暂停的时候不能拖拽小鸟
                GameManager.Instance.Birds[0].CanTrag = false;
            }
        }
    } 
    //Pause动画播放完,时间停止
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }
    //点击继续按钮后播放resume动画
    public void ResumeBtnClick()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
        if (GameManager.Instance.Birds.Count > 0)
        {
            //第一只小鸟是否还处于准备状态
            if (GameManager.Instance.Birds[0].isReleased == false)
            { 
                //暂停结束的时候可以拖拽小鸟
                GameManager.Instance.Birds[0].CanTrag = true;
            }
        }
    }
    //resume动画播放完显示暂停按钮
    public void ResumeAnimEnd() {
        PauseBtn.SetActive(true);
    }
}
