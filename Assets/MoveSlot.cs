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

    public void SubtractMoney() // �� ���� �Լ�
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
        slotStopTime += Time.deltaTime;                     // ���� �ð� üũ 
        autoStartTime = 0;
        for (int i = 0; i < Slots.Length; i++)              // ���� �������� ����
        {
            Slots[i].transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);                    // ���� �Ʒ��� ������

            if (Slots[i].transform.position.y < SlotBox.transform.position.y)
            {
                Slots[i].transform.Translate(Vector2.up * (slotHeight.rectTransform.sizeDelta.y * 4));  // ������ ���� ��ŭ ���� �ø�
            }
        }
    }

    void Money()
    {
        battingtextmoney.text = battingMoney.ToString();
        textmoney.text = money.ToString();
    }

    void AutoGame()            // �ڵ� ���� ���� �Լ�
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

       
        if (money < battingMoney)   // ���� ������ ��Ȳ
        {
            auto = false;
            return;
        }
        else if (auto)              // �� ���� ��� + Auto��ư Ŭ��
            AutoGame();
        else if (!startButtonClick || slotStopTime >= time) // Not Start Button �Ǵ� Slot�� ���� �ð��� �Ǿ�����
        {
            MoveingStopSlot();
        }
        else
        {      // �� ���� ��� + Start��ư Ŭ��
            MoveingSlot();
        }

        Debug.Log(donmoney);
    }

  
}
