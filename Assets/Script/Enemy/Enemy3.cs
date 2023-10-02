using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    protected override void Start()
    {
        base.Start();
        //����Һ͵��˶�����
        if_Immute = true;
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
        if(if_Immute)
        {
            enemySpriteRenderer.sprite = dodgeImage;//û��ǹ
            battleSystem.JudegeShoot();
        }
        else if (battleSystem.JudegeShoot())
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
