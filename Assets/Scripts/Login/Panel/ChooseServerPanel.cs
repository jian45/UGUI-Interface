using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.U2D;
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
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (LoginMgr.Instance.LoginData.frontServerID <= 0)
        {
            txtName.text = "无";
            imaState.gameObject.SetActive(false);

        }
        else 
        {
            //根据上一次登录的 服务器id 获取到 服务器信息
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            //为什么减一因为ServerData是List，而id只是不同的int
            //当id=1时意味这上次选了第一个服务器这个服务器在ServerData[0]所以要减一
            txtName.text = info.id + "区  " + info.name;
            //一开状态图 显示
            imaState .gameObject.SetActive(true);
            //状态 
            //加载图集
            SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
            switch (info. state) 
            {
               case 0:
                    imaState .gameObject .SetActive (false );
               break;
                case 1://流畅
                    imaState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                    break;
                case 2://繁忙
                    imaState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                    break;
                case 3://火爆
                    imaState.sprite = sa.GetSprite("ui_DL_huobao_01");
                    break;
                case 4://维护
                    imaState.sprite = sa.GetSprite("ui_DL_weihu_01");
                    break;
            }
        }
        //更新当前选择区间
        UpdataPanle(1, 5 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 5);

    }
 /// <summary>
 /// 提供给其它地方（如左侧按钮） 用于更新 当前选择区间的右侧按钮
 /// </summary>
 /// <param name="beginIndex"></param>
 /// <param name="endIndex"></param>
    public void UpdataPanle(int beginIndex ,int endIndex) 
    {
        
        //更新服务区间显示
        txtRange.text = "服务器 "+beginIndex + "-"+endIndex;
        //第一步删出之前的单个按钮
        for (int i = 0; i <itemList .Count; i++)
        {
            //删除之前的 对象
            Destroy(itemList[i]); 
        }
        //清空列表
        itemList.Clear();

        //第二步：创建新的按钮
        for (int i = beginIndex; i <= endIndex; i++) 
        {
            //首先获取 服务器信息
            ServerInfo nowInfo = LoginMgr.Instance.ServerData[i - 1];

            //动态创建预制体
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/ServerRightitem"));
            serverItem.transform.SetParent(svRight.content,false );


            //根据信息 更新按钮数据
            ServerRightItem rightItem = serverItem.GetComponent<ServerRightItem>();
            rightItem.InitInfo (nowInfo);

            //创建成功后 把它加入到列表中
            itemList .Add (serverItem);

        }

    }

}
