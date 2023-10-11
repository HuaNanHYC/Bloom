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
        yield return new WaitForSeconds(beforeActionInterval);//�ȴ�0.5��
        EnemyAction(true);
        yield return new WaitForSeconds(actionToReadyInterval);
        EnemyReady(false);
        //׼����ǹ
        yield return new WaitForSeconds(readyToShootInterval);
        //ʣ1��Ѫʱ��ǹ�ڶ�׼��
        if(currentHealth==1&&!if_ShootYou)
        {
            if_ShootYou = true;
            enemySpriteRenderer.sprite = shootToYouImage;
            battleSystem.if_PlayerShoot = true;
            yield return new WaitForSeconds(shootToReadyInterval);
            battleSystem.JudegeShoot();
            yield return new WaitForSeconds(readyToActionInterval);
            EnemyAction(true);
            yield return new WaitForSeconds(actionToIdleInterval);
            actionHand.SetActive(false);
            EnemyIdle();
            battleSystem.if_PlayerShoot = true;
            battleSystem.StartShoot();
            StopAllCoroutines();//ֹͣЭ��
        }
        else if (battleSystem.JudegeShoot())
        {
            EnemyShot();//��ǹ
            yield return new WaitForSeconds(shootToReadyInterval);
            EnemyReady(false);
            yield return new WaitForSeconds(readyToActionInterval);
            EnemyAction(true);
            yield return new WaitForSeconds(actionToIdleInterval);
            actionHand.SetActive(false);
            EnemyIdle();
            battleSystem.StartShoot();
            StopAllCoroutines();
        }
        else
        {
            EnemyDodge();//û��ǹ
            yield return new WaitForSeconds(shootToReadyInterval);
            EnemyReady(false);
            yield return new WaitForSeconds(readyToActionInterval);
            EnemyDodgeAction();
            actionHand.SetActive(true);
            yield return new WaitForSeconds(actionToIdleInterval);
            actionHand.SetActive(false);
            EnemyIdle();
            battleSystem.StartShoot();
            StopAllCoroutines();
        }
    }
}
