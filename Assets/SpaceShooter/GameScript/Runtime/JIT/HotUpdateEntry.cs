using System.Collections;
using System.Collections.Generic;
using UniFramework.Module;
using UniFramework.Pooling;
using UnityEngine;
using YooAsset;

public class HotUpdateEntry
{
    public static void Main()
    {
        
        // 创建游戏管理器
        UniModule.CreateModule<GameManager>();

        // 开启游戏流程
        GameManager.Instance.Run();
        
        
        UnityEngine.Debug.Log("hello, HybridCLR");
        UnityEngine.Debug.Log("开启游戏流程  GameManager.Instance.Run();");

        var gameObject = new GameObject();
        var _AddComponentcollider = gameObject.AddComponent<BoxCollider>();
        if (_AddComponentcollider)
        {
            UnityEngine.Debug.Log("创建一个GameObject,  添加BoxCollider");
        }
        
        var _GetComponentcollider =gameObject.GetComponent<BoxCollider>();
        if (_GetComponentcollider)
        {
            UnityEngine.Debug.Log(" gameObject.GetComponent<BoxCollider>() 成功 ");
        }
        

    }
}
