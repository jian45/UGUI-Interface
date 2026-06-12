using UnityEngine;

/// <summary>
/// 这个脚本是作为一个服务器类来使用的，负责处理登录相关的逻辑和数据交互。
/// 单例模式
/// </summary>
public class LoginMgr
{
    //静态实例，确保全局只有一个LoginMgr对象
    private static LoginMgr instance = new LoginMgr();
    //全局访问点
    public static LoginMgr Instance => instance;
    // 私有构造函数，防止外部实例化
    private LoginData loginData;
    //全局LoginData单例访问点
    public LoginData LoginData => loginData;

    //注册数据
    private  RegisterData registerData;
    //方便外部访问注册数据
    public RegisterData RegisterData => registerData;

    private LoginMgr() 
    {
        //直接通过json管理器 来读取对应数据
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");

        //读取注册数据
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");
    }
    #region 登录数据
    /// <summary>
    /// 这个方法用于保存登录信息的
    /// </summary>
    public void SaveLoginData() 
    {
        JsonMgr.Instance.SaveData(loginData , "LoginData");
    }
    #endregion
    #region 注册数据
    //提供外部存储注册数据的方法
    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "RegisterData");
    }
    //注册方法
    public bool RegisterUser(string username,string password)
    {
        //检测字典里是否已经存在这个用户名，如果存在则注册失败，返回false
        if (registerData.ragisterInfo.ContainsKey(username)) 
        {
            return false;
         }
        else 
        {
            //如果不存在这个用户名，则将用户名和密码添加到字典中，并保存注册数据，返回true表示注册成功
            registerData.ragisterInfo.Add(username, password);
            //进行本地存储数据
            SaveRegisterData();
            return true;
        }
    }
    //验证用户名和密码是否合法
    public bool CheckInfo(string username, string password)
    {
        //首先检查登录数据[字典]中是否存在这个用户名，如果不存在则返回false
        if (registerData.ragisterInfo.ContainsKey(username))
        {
            if (registerData.ragisterInfo[username] == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else 
        {
            return false;
        }
    }
    #endregion 
}
