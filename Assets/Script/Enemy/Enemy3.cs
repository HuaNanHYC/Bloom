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
        yield return new WaitForSeconds(beforeActionInterval);//�ȴ�0.5��
        EnemyAction(true);
        yield return new WaitForSeconds(actionToReadyInterval);
        EnemyReady(false);
        //׼����ǹ
        yield return new WaitForSeconds(readyToShootInterval);
        if (!if_Immute)
        {
            if (battleSystem.JudegeShoot())
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
        else
        {
            battleSystem.JudegeShoot();
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
