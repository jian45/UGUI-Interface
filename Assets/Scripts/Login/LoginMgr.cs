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
    //只读属性，外部只能获取登录数据，不能修改
    public LoginData LoginData => loginData;

    private LoginMgr() 
    {
        //直接通过json管理器 来读取对应数据
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
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
}
