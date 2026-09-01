using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthTip : MonoBehaviour
{
    [Header("组件引用")]
    public GameObject HealthTipPanel;   // 健康提醒页面
    public GameObject NextPanel;    // 加载完后跳转的下一个场景
    public Slider LoadingSlider;    // 进度条组件
    public TextMeshProUGUI PercentText;    // 百分比文字显示
    public GameObject SettingsPanel;    // 设置页面

    [Header("加载设置")]
    public float loadDuration = 3f; // 进度条总时长s
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        this.ActiveInit();   
    }

    // Update is called once per frame
    void Update()
    {
        this.SliderShow();
    }
    // 初始化页面显示
    void ActiveInit()
    {
        this.HealthTipPanel.SetActive(true);
        this.NextPanel.SetActive(false);
        this.LoadingSlider.value = 0;   // 进度条数值归零
        this.timer = 0;
        this.SettingsPanel.SetActive(false);
    }
    // 进度条加载与显示
    void SliderShow()
    {
        if(this.timer < this.loadDuration)
        {
            this.timer += Time.deltaTime;
            float progress = this.timer / this.loadDuration;
            this.LoadingSlider.value = progress;
            // 百分比文字更新
            if(this.PercentText != null)
            {
                this.PercentText.text = Mathf.RoundToInt(progress * 100) + "%";
            }    
        }
        else
        {
            this.ToMainPanel();
        }
    }
    // 页面切换
    void ToMainPanel()
    {
        this.HealthTipPanel.SetActive(false);  // 隐藏健康提醒
        this.NextPanel.SetActive(true);        // 显示主面板
        this.enabled = false;   // 禁用脚本，不再执行Update
    }
}
