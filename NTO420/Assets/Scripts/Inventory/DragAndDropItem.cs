﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
/// IPointerDownHandler - Следит за нажатиями мышки по объекту на котором висит этот скрипт
/// IPointerUpHandler - Следит за отпусканием мышки по объекту на котором висит этот скрипт
/// IDragHandler - Следит за тем не водим ли мы нажатую мышку по объекту
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public SlotInventory oldSlot;
    private Transform player;
    
    private void Start()
    {
        //ПОСТАВЬТЕ ТЭГ "PLAYER" НА ОБЪЕКТЕ ПЕРСОНАЖА!
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Находим скрипт InventorySlot в слоте в иерархии
        oldSlot = transform.GetComponentInParent<SlotInventory>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Если слот пустой, то мы не выполняем то что ниже return;
        if (oldSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.GetComponentInParent<SlotInventory>().item.name == "Honey")
        {
            gameObject.GetComponentInParent<SlotInventory>().stats.CheckUpdates();
            gameObject.GetComponentInParent<SlotInventory>().stats.resources.honey -= 1;
            gameObject.GetComponentInParent<SlotInventory>().stats.resources.food += 5;
            /*gameObject.GetComponentInParent<SlotInventory>().count -= 1;
            gameObject.GetComponentInParent<SlotInventory>().itemCountText.text = gameObject.GetComponentInParent<SlotInventory>().count.ToString();*/
            gameObject.GetComponentInParent<SlotInventory>().stats.UpdateRes();
        }
        else
        {
            if (oldSlot.isEmpty)
                return;
            //Делаем картинку прозрачнее
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
            // Делаем так чтобы нажатия мышкой не игнорировали эту картинку
            GetComponentInChildren<Image>().raycastTarget = false;
            // Делаем наш DraggableObject ребенком InventoryPanel чтобы DraggableObject был над другими слотами инвенторя
            transform.SetParent(transform.parent.parent);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.GetComponentInParent<SlotInventory>().item.name != "Honey")
        {
            if (oldSlot.isEmpty)
                return;
            // Делаем картинку опять не прозрачной
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
            // И чтобы мышка опять могла ее засечь
            GetComponentInChildren<Image>().raycastTarget = true;

            //Поставить DraggableObject обратно в свой старый слот
            transform.SetParent(oldSlot.transform);
            transform.position = oldSlot.transform.position;
            //Если мышка отпущена над объектом по имени UIPanel, то...
            if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel")
            {
                // Выброс объектов из инвентаря - Спавним префаб обекта перед персонажем
                GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
                // Устанавливаем количество объектов такое какое было в слоте
                itemObject.GetComponent<Items>().count = oldSlot.count;
                // убираем значения InventorySlot
                NullifySlotData();
            }
            else if(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<SlotInventory>() != null)
            {
                //Перемещаем данные из одного слота в другой
                ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<SlotInventory>());
            }
        }
    }
    void NullifySlotData()
    {
        // убираем значения InventorySlot
        oldSlot.item = null;
        oldSlot.count = 0;
        oldSlot.isEmpty = true;
        oldSlot._icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot._icon.GetComponent<Image>().sprite = null;
        oldSlot.itemCountText.text = "";
    }
    void ExchangeSlotData(SlotInventory newSlot)
    {
        // Временно храним данные newSlot в отдельных переменных
        ItemScriptableObject item = newSlot.item;
        int amount = newSlot.count;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot._icon;
        TMP_Text itemAmountText = newSlot.itemCountText;

        // Заменяем значения newSlot на значения oldSlot
        newSlot.item = oldSlot.item;
        newSlot.count = oldSlot.count;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot._icon.GetComponent<Image>().sprite);
            newSlot.itemCountText.text = oldSlot.count.ToString();
        }
        else
        {
            newSlot._icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot._icon.GetComponent<Image>().sprite = null;
            newSlot.itemCountText.text = "";
        }
        
        newSlot.isEmpty = oldSlot.isEmpty;

        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
        oldSlot.item = item;
        oldSlot.count = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
            oldSlot.itemCountText.text = amount.ToString();
        }
        else
        {
            oldSlot._icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot._icon.GetComponent<Image>().sprite = null;
            oldSlot.itemCountText.text = "";
        }
        
        oldSlot.isEmpty = isEmpty;
    }
}
