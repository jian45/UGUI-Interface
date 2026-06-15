using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    //确定与取消按钮
    public Button btnSur;
    public Button btnCancel;

    public InputField inputUN;//用户名输入框
    public InputField inputPW;//密码
    protected override void Init()
    {
        //点击取消
        btnCancel.onClick.AddListener(() => {
            //当点击取消按钮时 隐藏自己 显示登录面板
            UIManager.Instance.HidePanel<RegisterPanel>();
            UIManager.Instance.ShowPanel < LoginPanel>();
        });
        //点击确定按钮
        btnSur.onClick.AddListener(() => {
            //判断新建用户名是否正确
            if (inputPW.text.Length <= 6 || inputPW.text.Length <= 6)
            {
                //出提示面板
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示面板上的内容
                panel.ChangeInfo("账号密码必须大于6位");
                return;
            }
            //注册账号密码
            if (LoginMgr.Instance.RegisterUser(inputUN.text, inputPW.text))
            {
                //清理登录数据 用于 新注册账号的数据重置 不然会残留上一个账号的相关数据
                LoginMgr.Instance.ClearLoginData();
                //注册成功
                //显示登录界面
                LoginPanel panel=UIManager.Instance.ShowPanel<LoginPanel>();
                //更新登录面板上的用户名和密码
                panel.SetInfo(inputUN.text,inputPW.text);

                //隐藏自己
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else 
            {

                //注册失败
                //弹出提示界面
                TipPanel panel=UIManager.Instance.ShowPanel<TipPanel>();
                //更改提示内容
                panel.ChangeInfo("该账号名已经存在");


                //方便别人重新输入
                inputUN.text = "";
                inputPW.text = "";
            }
        }
        );
    }


}
