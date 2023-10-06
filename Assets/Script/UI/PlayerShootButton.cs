using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootButton : MonoBehaviour
{
    public BattleSystem battleSystem;
    private SpriteRenderer playerSprite;
    private void Start()
    {
        playerSprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
    }
    public void JudgeShoot()
    {
        StartCoroutine(Shoot());
    }
    public IEnumerator Shoot()//��ҽ������
    {
        //��ǹ
        playerSprite.sprite = InventoryManager.Instance.PlayerActionImage;
        yield return new WaitForSeconds(0.5f);
        //��ͷ��
        playerSprite.sprite = null;
        yield return new WaitForSeconds(1f);
        //��ǹ
        
        if (battleSystem.JudegeShoot())
        {
            playerSprite.sprite = InventoryManager.Instance.PlayerShotImage;
        }
        //��ǹ�Ż�ȥ
        yield return new WaitForSeconds(0.5f);
        playerSprite.sprite = InventoryManager.Instance.PlayerActionImage;
        yield return new WaitForSeconds(0.5f);
        playerSprite.sprite = null;
        battleSystem.StartShoot();
    }
}
