using System.Collections.Generic;
using UnityEngine;
using cardgame;
using UnityEngine.UI;
using TMPro;

public class OppDrawManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();
    private int currentCardIndex = 0;
    private OppHandManager handManager;
    private OppDiscardManager discardManager;
    public TextMeshProUGUI countText;
    public int currentHandSize = 0;
    public int startingHandSize = 5;

    private void Start()
    {
        handManager = FindFirstObjectByType<OppHandManager>();
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void InitDrawPile(List<Card> cards)
    {
        drawPile.AddRange(cards);
        Utility.Shuffle(drawPile);
        UpdateDrawCount();
    }

    public void battleSetup(int initDrawCount)
    {
        for (int i = 0; i < initDrawCount; i++)
        {
            DrawCard(handManager);
        }
    }

    public void DrawCard(OppHandManager handManager)
    {
        if (drawPile.Count == 0)
        {
            return;
        }

        Card nextCard = drawPile[currentCardIndex];
        handManager.AddCardToHand(nextCard);
        drawPile.RemoveAt(currentCardIndex);
        if (drawPile.Count > 0) currentCardIndex %= drawPile.Count;
        UpdateDrawCount();
    }
    
    public void UpdateDrawCount()
    {
        countText.text = drawPile.Count.ToString();
    }
}
