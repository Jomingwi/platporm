using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//드래그 가능한 인벤토리 아이템

public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler , IEndDragHandler
{
    Transform canvas; //드래그할때 UI뒤로 그려지는것을 방지하기위해 잠깐 이용할 캔버스
    Transform beforeParent; // 혹시 잘못된 위치에 드롭하게 되면 돌아오게되는 위치

    CanvasGroup canvasGroup; //자식들을 통합 관리하는 컴포먼트

    Image imgItem;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        imgItem = GetComponent<Image>();
    }

    void Start()
    {
        canvas = InventoryManager.Instance.CanvasInventory;
        
    }

    /// <summary>
    /// idx넘버를 전달받으면 해당 아이템을 Json으로 부터 검색하여 찾고
    /// 해당 아이템 데이터에서 필요한 정보만을 가져와서 해당 스크립트에 채워줌
    /// </summary>
    /// <param name="_idx"> 아이템의 인덱스 넘버</param>

    public void SetItem(string _idx)
    {  
        string spriteName = JsonManager.instance.GetSpriteNameFromIdx(_idx);
        imgItem = GetComponent<Image>();
        imgItem.sprite = SpriteManager.instance.GetSprite(spriteName);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeParent = transform.parent;

        transform.SetParent(canvas);

        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent == canvas)
        {
            transform.SetParent (beforeParent);
            transform.position = beforeParent.position;
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }


}

    
