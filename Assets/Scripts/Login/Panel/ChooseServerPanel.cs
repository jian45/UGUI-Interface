using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    //面板左右的滚动视图
    public ScrollRect svLefe;
    public ScrollRect svRight;
    
    //上一次登录的服务器相关信息
    public Text txtName;
    public Image imaState;
    //当前选择的区间范围
    public Text txtRange;

    //用于存储右侧的按钮们
     List<GameObject> itemList = new List<GameObject>();
    protected override void Init()
    {
        //动态创建左侧的 区间按钮

        //获取服务器列表的数据（json配置表）
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;

        //得到一个共要循环创建多少个区间按钮
        //由于向下取整 所以我们+1 就代表 平均分成了n份
        int num = infoList.Count / 5 + 1;

        for (int i= 0;i<num;i++) 
        {
            //动态创建预设体对象
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/ServerLeftltem"));
            
            //设置其父对象，false是保证SetParent方法不会根据父对象的位置缩放改变按钮的大小缩放
            item.transform.SetParent(svLefe.content, false);

            //初始化按钮
            //这一步需要丁意制作的按钮脚本所以会有所空缺
             ServerLeftItem  serverLeft =item.GetComponent<ServerLeftItem>();
            //开始索引与结束索引要从i算出
            int beginIndex = i * 5 + 1;
            int endIndex = 5 * (i+1);
            //保证尾数索引不超链表的长度
            if (endIndex > infoList.Count) 
            {
            endIndex = infoList.Count;
            }
            serverLeft.InitInfo(beginIndex, endIndex);
       
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();

        //显示自己时
        //应该初始化 上一次选择的服务器

        //更新当前选择

    }
 /// <summary>
 /// 提供给其它地方（如左侧按钮） 用于更新 当前选择区间的右侧按钮
 /// </summary>
 /// <param name="beginIndex"></param>
 /// <param name="endIndex"></param>
    public void UpdataPanle(int beginIndex ,int endIndex) 
    {
    
    }

}
