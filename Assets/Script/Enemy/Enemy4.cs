using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    bool have_ShootTwice = false;
    public override IEnumerator EnemyShooting()//���˿�ǹ
    {
        //������ǹ
        yield return new WaitForSeconds(beforeActionInterval);//�ȴ�0.5��
        EnemyAction(true);
        yield return new WaitForSeconds(actionToReadyInterval);
        EnemyReady(false);
        EnemyBulletTurnAudio();
        //׼����ǹ
        yield return new WaitForSeconds(readyToShootInterval);
A:      if (battleSystem.JudegeShoot())
        {
            EnemyShot();//��ǹ
            yield return new WaitForSeconds(shootToReadyInterval);
            EnemyReady(false);
            yield return new WaitForSeconds(readyToActionInterval);
            if (!have_ShootTwice)
            {
                battleSystem.if_PlayerShoot = false;
                have_ShootTwice = true;
                goto A;
            }
            else
            {
                have_ShootTwice = false;
            }
            EnemyAction(true);
            yield return new WaitForSeconds(actionToIdleInterval);
            actionHand.SetActive(false);
            EnemyIdle();
        }
        else
        {
            EnemyDodge();//û��ǹ
            yield return new WaitForSeconds(shootToReadyInterval);
            EnemyReady(false);
            yield return new WaitForSeconds(readyToActionInterval);
            if (!have_ShootTwice)
            {
                battleSystem.if_PlayerShoot = false;
                have_ShootTwice = true;
                goto A;
            }
            else
            {
                have_ShootTwice = false;
            }
            EnemyDodgeAction();
            actionHand.SetActive(true);
            yield return new WaitForSeconds(actionToIdleInterval);
            actionHand.SetActive(false);
            EnemyIdle();
        }
       
        //������ǹ���ж�
        /*if (!have_ShootTwice)
        {
            battleSystem.if_PlayerShoot = false;
            have_ShootTwice = true;
            goto A;
        }
        else
        {
            have_ShootTwice = false;
        }*/
        battleSystem.StartShoot();
        yield return null;
    }
}
