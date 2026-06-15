using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ServerPanel : BasePanel
{
    public Button btnStart;
    public Button btnChange;
    public Button btnBack;

    public Text txtName;
    protected override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
        });

        btnStart.onClick.AddListener(() =>
        {
            //进入游戏
            //由于过场景 Canvas对象不会被移除 所以下面的面板应该也要隐藏了

            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
            //切换场景
            SceneManager.LoadScene("GameScene");
        });

        btnChange.onClick.AddListener(() =>
        {
            //显示服务器列表面板
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }

      public override void ShowMe()
    {
            base.ShowMe();

        //显示自己的时候 更新 当前选择的服务器名字
        //之后 我们会通过记录上一次登录的服务器ID 来更新内容

        //在ServerRightItem 类里的按钮事件中会记录按下的id
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (id<=0 )
        {
            txtName.text = "无";
        } else 
        {
            //根据id我们可以加载对应的服务器
            ServerInfo serverInfo = LoginMgr.Instance.ServerData[id - 1];

            //得到对应的服务器数据后可以就可以更新服务器面板数据了
            txtName.text = serverInfo.id + "区  " + serverInfo.name;
        }
        

    }
    
}
