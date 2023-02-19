using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UniFramework.Module;
using UnityEngine;
using UnityEngine.Networking;
using YooAsset;

public partial class LoadDll
{
    public static List<string> AOTMetaAssemblyNames { get; } = new List<string>()
    {
        "mscorlib.dll",
        "System.dll",
        "System.Core.dll",
        "ThirdParty.dll",
    };

    private static Dictionary<string, byte[]> s_assetDatas = new Dictionary<string, byte[]>();

    public static byte[] GetAssetData(string dllName)
    {
        return s_assetDatas[dllName];
    }

    public static void DownLoadAssets()
    {
        var assets = new List<string>
        {
            "Game.JIT.dll",
        }.Concat(AOTMetaAssemblyNames);


        foreach (string asset in assets)
        {
            s_assetDatas[asset] = YooAssets.LoadRawFileSync(asset)?.GetRawFileData();
        }

//         foreach (var asset in assets)
//         {
//             string dllPath = GetWebRequestPath(asset);
//             Debug.Log($"start download asset:{dllPath}");
//             UnityWebRequest www = UnityWebRequest.Get(dllPath);
//             yield return www.SendWebRequest();
//
// #if UNITY_2020_1_OR_NEWER
//             if (www.result != UnityWebRequest.Result.Success)
//             {
//                 Debug.Log(www.error);
//             }
// #else
//             if (www.isHttpError || www.isNetworkError)
//             {
//                 Debug.Log(www.error);
//             }
// #endif
//             else
//             {
//                 // Or retrieve results as binary data
//                 byte[] assetData = www.downloadHandler.data;
//                 Debug.Log($"dll:{asset}  size:{assetData.Length}");
//                 s_assetDatas[asset] = assetData;
//             }
//         }

        LoadMetadataForAOTAssemblies();

#if !UNITY_EDITOR
        Assembly ass = System.Reflection.Assembly.Load(GetAssetData("Game.JIT.dll"));

        // ass 为Assembly.Load返回的热更新assembly。
        // 你也可以在Assembly.Load后通过类似如下代码查找获得。
        // Assembly ass = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "Assembly-CSharp");
        Type entryType = ass.GetType("HotUpdateEntry");
        MethodInfo method = entryType.GetMethod("Main");
        method.Invoke(null, null);

        Debug.Log("GameManager   热更结束了");
#elif UNITY_EDITOR
        // Assembly ass = System.Reflection.Assembly.Load(GetAssetData("Game.JIT.dll"));

        // ass 为Assembly.Load返回的热更新assembly。
        // 你也可以在Assembly.Load后通过类似如下代码查找获得。
        Assembly ass = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "Game.JIT");
        Type entryType = ass.GetType("HotUpdateEntry");
        MethodInfo method = entryType.GetMethod("Main");
        method.Invoke(null, null);

        Debug.Log("GameManager   热更结束了");
#endif

        // AssetBundle prefabAb = AssetBundle.LoadFromMemory(GetAssetData("prefabs"));
        // GameObject testPrefab = Instantiate(prefabAb.LoadAsset<GameObject>("HotUpdatePrefab.prefab"));

        Debug.Log("热更结束了");
    }

    /// <summary>
    /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
    /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
    /// </summary>
    private static void LoadMetadataForAOTAssemblies()
    {
        /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
        /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
        /// 
        HomologousImageMode mode = HomologousImageMode.SuperSet;
        foreach (var aotDllName in AOTMetaAssemblyNames)
        {
            byte[] dllBytes = GetAssetData(aotDllName);
            // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
            LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
            Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
        }
    }
}