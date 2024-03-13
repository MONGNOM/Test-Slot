using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSlot : MonoBehaviour
{

    [SerializeField] int    moveSpeed;
    [SerializeField] float  time;
                     float  slotStopTime;
                     float  autoStartTime;

    public Image            SlotBox;
    public Image[]          Slots;
    public Image            slotHeight;
    

    bool startButtonClick;
    bool auto;

    private void Start()
    {
        auto                = false;
        startButtonClick    = false;
    }
    

    void AutoGame()
    {
        // 배팅금추가시 금액 부족이면 게임 멈춤 auto = fals
        
        if (autoStartTime >= time)
            startButtonClick = true;
        if (!startButtonClick || slotStopTime >= time)
            MoveingStopSlot();
        else
            MoveingSlot();

        
    }

    public void AutoButton()
    {
        auto = !auto;
    }

    public void StartButton()
    {
        startButtonClick    = !startButtonClick;
    }

    void MoveingStopSlot()
    {
        startButtonClick    = false;
        slotStopTime        = 0;
        
        autoStartTime += Time.deltaTime;
    }

    void MoveingSlot()
    {
        slotStopTime += Time.deltaTime; // 멈춤 시간 체크 
        autoStartTime = 0;
        for (int i = 0; i < Slots.Length; i++) // 슬롯 내려가는 영역
        {
            Slots[i].transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

            if (Slots[i].transform.position.y < SlotBox.transform.position.y)
            {
                Slots[i].transform.Translate(Vector2.up * (slotHeight.rectTransform.sizeDelta.y * 4));
            }
        }
    }

    void Update()
    {
        if (auto)
            AutoGame();
        else if (!startButtonClick || slotStopTime >= time)
            MoveingStopSlot();
        else
            MoveingSlot();
    }

  
}
