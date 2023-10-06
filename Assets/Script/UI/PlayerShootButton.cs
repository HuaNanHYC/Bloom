using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootButton : MonoBehaviour
{
    public BattleSystem battleSystem;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer playerHurtSpriteRenderer;
    private GameObject gunSprite;
    private void Start()
    {
        playerSpriteRenderer = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        playerHurtSpriteRenderer = playerSpriteRenderer.transform.GetChild(0).GetComponent<SpriteRenderer>();
        gunSprite = GameObject.FindWithTag("Gun");
    }
    public void JudgeShoot()
    {
        StartCoroutine(Shoot());
    }
    public IEnumerator Shoot()//��ҽ������
    {
        //��ǹ
        playerSpriteRenderer.sprite = InventoryManager.Instance.PlayerActionImage;
        yield return new WaitForSeconds(0.5f);
        //��ͷ��
        playerSpriteRenderer.sprite = null;
        gunSprite.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //��ǹ
        
        if (battleSystem.JudegeShoot())
        {
            playerHurtSpriteRenderer.sprite = InventoryManager.Instance.PlayerHurtImage;
        }
        //��ǹ�Ż�ȥ
        yield return new WaitForSeconds(0.5f);
        playerHurtSpriteRenderer.sprite = null;
        yield return new WaitForSeconds(0.5f);
        playerSpriteRenderer.sprite = InventoryManager.Instance.PlayerActionImage;
        gunSprite.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerSpriteRenderer.sprite = null;
        battleSystem.StartShoot();
    }
}
