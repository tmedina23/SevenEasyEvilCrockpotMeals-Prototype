using System.Collections.Generic;
using TMPro;
using UnityEngine;
using cardgame;
using Unity.VisualScripting;
using NUnit.Framework;

public class OppDiscardManager : MonoBehaviour
{
    [SerializeField] public List<Card> discardPile = new List<Card>();
    public TextMeshProUGUI countText;
    public int discardCount;

    void Awake()
    {
        UpdateDiscardCount();
    }

    public void UpdateDiscardCount()
    {
        discardCount = discardPile.Count;
        countText.text = discardCount.ToString();
    }

    public void AddCardToDiscard(Card card)
    {
        if (card != null)
        {
            discardPile.Add(card);
            UpdateDiscardCount();
        }
    }

    public Card GetCardFromDiscard()
    {
        if (discardPile.Count > 0)
        {
            Card card = discardPile[discardPile.Count - 1];
            discardPile.RemoveAt(discardPile.Count - 1);
            UpdateDiscardCount();
            return card;
        }
        else
        {
            return null;
        }
    }

    public bool getSelectedCard(Card card)
    {
        if (discardPile.Count > 0)
        {
            discardPile.Remove(card);
            UpdateDiscardCount();
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Card> getAllDiscardedCards()
    {
        if (discardPile.Count > 0)
        {
            List<Card> allDiscardedCards = new List<Card>(discardPile);
            discardPile.Clear();
            UpdateDiscardCount();
            return allDiscardedCards;
        }
        else
        {
            return new List<Card>();
        }
    }
}
