using UnityEngine;
/// <summary>
/// 本代码文件定义了一个LoginData类，用于存储登录相关的数据。
/// 目前包含了用户名/账号、密码、是否记住密码和是否自动登录等字段。
/// 但缺少服务器类没法存储和获取数据
/// 
/// </summary>
public class LoginData
{
    public string userName;// 用户名/账号
    public string passWord;//密码

    //是否记住密码
    public bool rememberPw;
    //是否自动登录
    public bool autoLogin;

    //服务器相关
    public int frontServerID=-1;//根据需求暂时改变为0
}
