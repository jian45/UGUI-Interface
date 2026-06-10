using UnityEngine;

public class Main : MonoBehaviour
{
   /// <summary>
   /// 这个脚本是测试脚本，用于测试面板是否能够正常工作
   /// </summary>
    void Start()
    {
        //显示面板
        //之后还有新的面板需要测试时，可以在这里添加显示新面板的代码
        //TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
   
       // tipPanel .ChangeInfo("这是一个测试面板，显示成功了哦！");
    
        LoginPanel loginPanel =UIManager.Instance.ShowPanel<LoginPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
