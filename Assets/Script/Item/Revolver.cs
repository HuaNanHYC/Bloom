using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    int[] steadyBullet;
    int[] steadyPos;
    private void OnEnable()
    {
        if (LevelManager.Instance == null) return;
        steadyBullet = LevelManager.Instance.GetCurentLevelSteadyBulletId();
        steadyPos = LevelManager.Instance.GetCurentLevelSteadyBulletPosition();
        SetHole();
    }
    public void SetHole()//���ݹؿ��������õ�ǰ�ĵ�ϻ���ӵ�
    {
        for(int i = 0; i < steadyBullet.Length; i++)
        {
            transform.GetChild(steadyPos[i]-1).GetComponent<BulletHole>().LoadBulletAuto(steadyBullet[i]);
        }
    }
}
