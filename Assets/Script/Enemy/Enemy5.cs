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
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        EnemyAction(true);
        yield return new WaitForSeconds(0.5f);
        EnemyReady(false);
        //׼����ǹ
        yield return new WaitForSeconds(0.5f);
        if (immuteCount == 3)
        {
            if_Immute = true;
            battleSystem.JudegeShoot();
            EnemyDodge();//û��ǹ
            yield return new WaitForSeconds(0.5f);
            EnemyReady(false);
            yield return new WaitForSeconds(0.5f);
            EnemyDodgeAction();
            actionHand.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actionHand.SetActive(false);
            EnemyIdle();
            battleSystem.StartShoot();
            StopAllCoroutines();
        }
        else if (battleSystem.JudegeShoot())
        {
            EnemyShot();//��ǹ
            yield return new WaitForSeconds(0.5f);
            EnemyReady(false);
            yield return new WaitForSeconds(0.5f);
            EnemyAction(true);
            yield return new WaitForSeconds(0.5f);
            actionHand.SetActive(false);
            EnemyIdle();
            battleSystem.StartShoot();
            StopAllCoroutines();
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
            battleSystem.StartShoot();
            StopAllCoroutines();
        }

    }
}
