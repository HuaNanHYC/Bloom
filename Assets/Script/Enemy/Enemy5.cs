using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : Enemy
{
    private int immuteCount;//���߼���
    public override IEnumerator EnemyShooting()
    {
        immuteCount++;
        //���������ʱ�����˺�
        /*if (battleSystem.BulletIndexShoot == 2)
        {
            battleSystem.bullets[battleSystem.BulletIndexShoot].actualDamage = 0;
        }*/
        //������ǹ
        yield return new WaitForSeconds(beforeActionInterval);//�ȴ�0.5��
        EnemyAction(true);
        yield return new WaitForSeconds(actionToReadyInterval);
        EnemyReady(false,true);
        EnemyBulletTurnAudio();
        //׼����ǹ
        yield return new WaitForSeconds(readyToShootInterval);
        if (immuteCount == 3)
        {
            if_Immute = true;
            battleSystem.JudegeShoot();
            EnemyDodge();//û��ǹ
            yield return new WaitForSeconds(shootToReadyInterval);
            EnemyReady(false, false);
            yield return new WaitForSeconds(readyToActionInterval);
            EnemyDodgeAction();
            actionHand.SetActive(true);
            yield return new WaitForSeconds(actionToIdleInterval);
            actionHand.SetActive(false);
            EnemyIdle();
            battleSystem.StartShoot();
            StopAllCoroutines();
        }
        else if (battleSystem.JudegeShoot())
        {
            EnemyShot();//��ǹ
            yield return new WaitForSeconds(shootToReadyInterval);
            EnemyReady(false,false);
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
            EnemyReady(false,false);
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
