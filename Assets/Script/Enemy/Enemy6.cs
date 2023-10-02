using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : Enemy
{
    public Sprite shootToYouImage; //��ǹ�ڶ�׼���ͼƬ
    private bool if_ShootYou=false;
    protected override void Start()
    {
        base.Start();
        //��������ߵ�һ���ӵ�
        InventoryManager.Instance.If_Immute = true;
    }
    public override IEnumerator EnemyShooting()
    {
        //������ǹ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        enemySpriteRenderer.sprite = actionImage;
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.sprite = readyImage;
        //׼����ǹ
        yield return new WaitForSeconds(0.5f);
        //ʣ1��Ѫʱ��ǹ�ڶ�׼��
        if(currentHealth==1&&!if_ShootYou)
        {
            if_ShootYou = true;
            enemySpriteRenderer.sprite = shootToYouImage;
            battleSystem.if_PlayerShoot = true;
            battleSystem.JudegeShoot();
            yield return new WaitForSeconds(0.5f);
            enemySpriteRenderer.sprite = actionImage;
            yield return new WaitForSeconds(0.5f);
            enemySpriteRenderer.sprite = dialogueImage;
            battleSystem.StartShoot();
            StopAllCoroutines();//ֹͣЭ��
        }
        if (battleSystem.JudegeShoot())
        {
            enemySpriteRenderer.sprite = shotImage;//��ǹ
        }
        else
        {
            enemySpriteRenderer.sprite = dodgeImage;//û��ǹ
        }
        //�ص���ʼװ̬�����˰�ǹ�Ż�
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.sprite = readyImage;
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.sprite = actionImage;
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.sprite = dialogueImage;
        battleSystem.StartShoot();
        yield return null;
    }
}
