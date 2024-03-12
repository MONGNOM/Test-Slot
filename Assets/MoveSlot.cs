using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSlot : MonoBehaviour
{

    [SerializeField] int moveSpeed;
    [SerializeField] int time;

    public Image SlotBox;
    public Image[] Slots;

    public Image slotHeight;

    void Update()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

            if (Slots[i].transform.position.y < SlotBox.transform.position.y) 
            {
                Slots[i].transform.Translate( Vector2.up * (slotHeight.rectTransform.sizeDelta.y * 4));
            }
        }
    }

  
}
