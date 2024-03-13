using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveSlot : MonoBehaviour
{
    [SerializeField] int    battingMoney;
    [SerializeField] int    money;
    [SerializeField] int    moveSpeed;
    [SerializeField] float  time;
                     float  slotStopTime;
                     float  autoStartTime;

    public TextMeshProUGUI textmoney;
    public TextMeshProUGUI battingtextmoney;
    public Image            SlotBox;
    public Image[]          Slots;
    public Image            slotHeight;
    

    bool startButtonClick;
    bool auto;
    bool donmoney;
    private void Start()
    {
        donmoney = false;  
        auto                = false;
        startButtonClick    = false;
    }

    public void SubtractMoney() // 돈 차감 함수
    {
        if (!startButtonClick)
            return;

        money -= battingMoney;
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
        donmoney = true;
        autoStartTime += Time.deltaTime;
    }

    void MoveingSlot()
    {
        donmoney = false;
        slotStopTime += Time.deltaTime;                     // 멈춤 시간 체크 
        autoStartTime = 0;
        for (int i = 0; i < Slots.Length; i++)              // 슬롯 내려가는 영역
        {
            Slots[i].transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);                    // 슬롯 아래로 내리기

            if (Slots[i].transform.position.y < SlotBox.transform.position.y)
            {
                Slots[i].transform.Translate(Vector2.up * (slotHeight.rectTransform.sizeDelta.y * 4));  // 슬롯의 길이 만큼 위로 올림
            }
        }
    }

    void Money()
    {
        battingtextmoney.text = battingMoney.ToString();
        textmoney.text = money.ToString();
    }

    void AutoGame()            // 자동 게임 진행 함수
    {
        if (autoStartTime >= time)
            startButtonClick = true;
        if (!startButtonClick || slotStopTime >= time)
        {
            SubtractMoney();
            MoveingStopSlot();
        }
        else
            MoveingSlot();
    }

    void Update()
    {
        Money();

       
        if (money < battingMoney)   // 돈이 부족한 상황
        {
            auto = false;
            return;
        }
        else if (auto)              // 돈 있을 경우 + Auto버튼 클릭
            AutoGame();
        else if (!startButtonClick || slotStopTime >= time) // Not Start Button 또는 Slot이 멈출 시간이 되었을때
        {
            MoveingStopSlot();
        }
        else
        {      // 돈 있을 경우 + Start버튼 클릭
            MoveingSlot();
        }

        Debug.Log(donmoney);
    }

  
}
