using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    //确定按钮
    public Button ButSure;
    //提示文本
    public Text TipText;
    protected override void Init()
    {
        //监听确定按钮的点击事件
        ButSure.onClick.AddListener(() => 
        {
            //当点击确定按钮时 隐藏自己
            UIManager.Instance.HidePanel<TipPanel>();
        });

    }

    /// <summary>
    /// 提供一个方法 让外部能够改变提示文本的内容
    /// </summary>
    /// <param name="info">提示内容</param>
    public void ChangeInfo(string  info) 
    {
        TipText.text = info;
    }
   
}
