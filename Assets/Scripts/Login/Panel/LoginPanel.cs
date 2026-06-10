using UnityEngine;
using UnityEngine.UI;

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
            //当下没有注册面板 先用写下注释
            // UIManager.Instance.ShowPanel

            //隐藏自己
            UIManager.Instance.HidePanel<LoginPanel>();
        }
        );
        //点击登录做什么
        butSure.onClick.AddListener(() => 
        {
        //点击登录后要验证用户名和密码是否正确 这里先用写下注释
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
        }
    }
}
