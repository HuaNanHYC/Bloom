using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    bool have_ShootTwice = false;
    public override IEnumerator EnemyShooting()//���˿�ǹ
    {
        //������ǹ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        EnemyAction(true);
        yield return new WaitForSeconds(0.5f);
        EnemyReady(false);
        //׼����ǹ
        yield return new WaitForSeconds(0.5f);
        if (battleSystem.JudegeShoot())
        {
            EnemyShot();//��ǹ
            yield return new WaitForSeconds(0.5f);
            EnemyReady(false);
            yield return new WaitForSeconds(0.5f);
            EnemyAction(true);
            yield return new WaitForSeconds(0.5f);
            actionHand.SetActive(false);
            EnemyIdle();
        }
        else
        {
            EnemyDodge();//û��ǹ
            yield return new WaitForSeconds(0.5f);
            EnemyReady(false);
            yield return new WaitForSeconds(0.5f);
            EnemyDodgeAction();
            actionHand.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actionHand.SetActive(false);
            EnemyIdle();
        }
       
        //������ǹ���ж�
        if (!have_ShootTwice)
        {
            battleSystem.if_PlayerShoot = false;
            have_ShootTwice = true;
        }
        else
        {
            have_ShootTwice = false;
        }
        battleSystem.StartShoot();
        yield return null;
    }
}
