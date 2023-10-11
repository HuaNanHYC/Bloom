using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IPointerMoveHandler
{
    private Image selfImage;
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite pressedSprite;


    private void Awake()
    {
        selfImage = GetComponent<Image>();
        selfImage.sprite = normalSprite;
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        selfImage.sprite = hoverSprite;
        AudioManager.Instance.AudioSource2EffectSource.PlayOneShot(AudioManager.Instance.HangMusic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selfImage.sprite = normalSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selfImage.sprite = pressedSprite;
        AudioManager.Instance.AudioSource2EffectSource.PlayOneShot(AudioManager.Instance.ClickMusic);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selfImage.sprite = normalSprite;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        selfImage.sprite = hoverSprite;
    }
}
