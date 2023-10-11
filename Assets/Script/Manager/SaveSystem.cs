using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{
    #region ��ͨ���ݱ��棺���õ�

    /// <summary>
    /// �浵
    /// </summary>
    /// <param name="saveFileName"></param>
    /// <param name="data"></param>
    public static bool SaveByJson(string saveFileName, object data, string savePath)
    {
        var jason = JsonUtility.ToJson(data, true);
        var path = Path.Combine(savePath, saveFileName);
        try
        {
            File.WriteAllText(path, jason);
#if UNITY_EDITOR
            Debug.Log($"����ɹ�{path}");
#endif
            return true;
        }
        catch (System.Exception e)
        {
#if UNITY_EDITOR
            Debug.Log($"����ʧ��{path}.\n{e}");
#endif
            return false;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="saveFileName"></param>
    /// <returns></returns>
    public static T LoadFromJson<T>(string saveFileName, string savePath)
    {
        try
        {
            var path = Path.Combine(savePath, saveFileName);

            var json = File.ReadAllText(path);

            var data = JsonUtility.FromJson<T>(json);
#if UNITY_EDITOR
            Debug.Log($"��ȡ�ɹ�");
#endif
            return data;
        }
        catch (System.Exception e)
        {
#if UNITY_EDITOR
            Debug.Log($"��ȡʧ��.\n{e}");
#endif
            return default;
        }
    }

    /// <summary>
    /// ɾ���浵
    /// </summary>
    /// <param name="saveFileName"></param>
    /*public static bool DeleteSaveFile(string saveFileName, string savePath)
    {
        var path = Path.Combine(savePath, saveFileName);
        try
        {
            File.Delete(path);
            return true;
        }
        catch (System.Exception e)
        {
#if UNITY_EDITOR
            Debug.Log($"failed{e}");
#endif
            return false;
        }
    }*/
    #endregion
}