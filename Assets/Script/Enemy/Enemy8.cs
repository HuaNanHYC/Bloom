using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8 : Enemy
{
    protected override void Start()
    {
        base.Start();
        BanLoad();
    }
    public void BanLoad()//���޷�װ���κ��ӵ�
    {
        for(int i=0;i< BulletManager.Instance.revolver.transform.childCount; i++)
        {
            BulletManager.Instance.revolver.transform.GetChild(i).GetComponent<BulletHole>().if_Load = true;
        }
    }
}
