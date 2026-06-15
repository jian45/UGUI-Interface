using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class LoginPanel : BasePanel
{
    //用户名输入框
    public InputField inputUser;
    //密码输入框
    public InputField inputPass;
    //确定按钮
    public Button butSure;
    //注册按钮
    public Button butReg;
    //记住密码的Toggle
    public Toggle RememberPw;
    //自动登录的Toggle
    public Toggle AutoLogin;

    protected override void Init()
    {
        //点击注册做什么
        butReg.onClick.AddListener(() => {
            //当点击注册按钮时 显示注册面板

            UIManager.Instance.ShowPanel<RegisterPanel>();

           
            //隐藏自己
            UIManager.Instance.HidePanel<LoginPanel>();
        }
        );
        //点击登录做什么
        butSure.onClick.AddListener(() => 
        {
            //账号密码必须小于6位出提示
            if (inputUser.text.Length <= 6 || inputPass.text.Length <= 6)
            {
                //出提示面板
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示面板上的内容
                panel.ChangeInfo("账号密码必须大于6位");
                return;
            }

            //点击登录后要验证用户名和密码是否正确 
            if (LoginMgr.Instance.CheckInfo(inputUser.text, inputPass.text))
            {
                
                //记录数据LoginData类以支持面板的记住密码功能与自动登录功能
                LoginMgr.Instance.LoginData.userName = inputUser.text;
                LoginMgr.Instance.LoginData.passWord = inputPass.text;
                //记住记住密码功能与自动登录功能是否使用
                LoginMgr.Instance.LoginData.RememberPw = RememberPw.isOn;
                LoginMgr.Instance.LoginData.AutoLogin = AutoLogin.isOn;

                //使用json管理器保存此次数据
                LoginMgr.Instance.SaveLoginData();

                //根据服务器 来进行判断 显示哪个面板
                //该账号上次从未进入服务器则进选服面板进过则进服务器面板
                if (LoginMgr.Instance.LoginData.frontServerID == -1) 
                {
                    //如果从来没有选择过服务器 id为-1时 就应该直接打开 选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //打开我们的服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                //隐藏自身
                UIManager.Instance.HidePanel<LoginPanel>();
            }
            else 
            {
                //失败

                //出提示面板
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示面板上的内容
                panel.ChangeInfo("账号或密码错误");
            }
        });
        //记住密码的Toggle做什么
        RememberPw.onValueChanged.AddListener((isOn) =>
        {
            //当我们没选中 记住密码时 如果自动登录 有被选中 应该让它没有选中
            if (!isOn) 
            {
                AutoLogin.isOn = false;
            }
        });
        //自动登录的Toggle做什么
        AutoLogin.onValueChanged.AddListener((isOn) =>
        {
            //当我们选中 自动登录时 如果记住密码 没有被选中 应该让它选中
            if (isOn ) 
            {
                RememberPw.isOn = true;
            }
        });

    }
    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己时 根据之前保存的数据来设置输入框和Toggle的状态

        // 获取数据
        LoginData loginData = LoginMgr.Instance.LoginData;
        //初始化面板
        //更新两个多选框的状态
        RememberPw.isOn=loginData.RememberPw;
        AutoLogin.isOn = loginData.AutoLogin;
        //更新输入框的内容
        inputUser.text = loginData.userName;
        //根据上次是否记住密码来决定是否显示密码
        if (RememberPw.isOn)
        {
            inputPass.text = loginData.passWord;
        }
        //如果自动登入 做什么
        if (AutoLogin.isOn) 
        {
            //自动验证账号密码相关

            //隐藏自身
           // UIManager.Instance.HidePanel<LoginPanel>();
        }
    }

    //提供外部改变面板内容的方法
    public void SetInfo(string name,string password) 
    {
        inputUser.text = name;
        inputPass .text = password;
    }
}
