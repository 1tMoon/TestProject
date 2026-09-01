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
    public CanvasGroup FadeOutCanvas;   // 控制页面淡出组件

    [Header("加载设置")]
    public float loadDuration = 2.5f; // 进度条总时长s
    private float timer;
    private float fadeTimer = 0.2f; // 淡出所需时长s

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
        this.timer = 0; // 时间归零
        this.FadeOutCanvas.alpha = 1;   // 淡出数值
        this.SettingsPanel.SetActive(false);
    }
    // 进度条加载与显示
    void SliderShow()
    {
        this.timer += Time.deltaTime;
        if (this.timer < this.loadDuration)
        {
            
            float progress = this.timer / this.loadDuration;    // 进度条进度数值
            this.LoadingSlider.value = progress;
            // 百分比文字更新
            if (this.PercentText != null)
            {
                this.PercentText.text = Mathf.RoundToInt(progress * 100) + "%";
            }
        }
        else if(this.timer >= this.loadDuration + 0.5)  // 加载完毕后进行延迟切换
        {
            this.PanelFadeOut();
        }
    }
    // 页面淡出效果控制
    void PanelFadeOut()
    {
        this.NextPanel.SetActive(true);
        this.fadeTimer -= Time.deltaTime;
        this.FadeOutCanvas.alpha = this.fadeTimer;  // alpha 数值逐渐递减
        if(this.FadeOutCanvas.alpha <= 0)
        {
            this.HealthTipPanel.SetActive(false);  // 隐藏健康提醒
            this.enabled = false;   // 禁用脚本，不再执行Update
            Debug.Log("页面加载完毕");
        }

    }
}
