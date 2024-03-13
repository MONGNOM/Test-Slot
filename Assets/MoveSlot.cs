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
        // ���ñ��߰��� �ݾ� �����̸� ���� ���� auto = fals
        
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
        slotStopTime += Time.deltaTime; // ���� �ð� üũ 
        autoStartTime = 0;
        for (int i = 0; i < Slots.Length; i++) // ���� �������� ����
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
