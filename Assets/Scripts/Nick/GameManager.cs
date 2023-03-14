using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Hand hand;
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    
                    randCard.transform.position = cardSlots[i].position;
                    availableCardSlots[i] = false;
                   

                    hand.AddToHand(randCard.cardValue, i);
                    return;
                }
            }
        }
    }

    public void AnalyzeHand(Hand thisHand)
    {
        
    }
    
    // Start is called before the first frame update
    void Start() 
    {
        for (int i = 1; i <= 51; i++)
        {
            deck[i].cardValue = i+1;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("drawKey"))
        {
            DrawCard();
        }
    }
}
