using UnityEngine;
using cardgame;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class OppDeckManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();

    private OppHandManager handManager;
    private OppDrawManager drawManager;
    public int currentHandSize = 0;
    public int startingHandSize = 5;
    public bool startBattleRun = true;

    private void Start()
    {
        Card[] cards = Resources.LoadAll<Card>("Cards/Opp");

        this.deck.AddRange(cards);
    }

    void Awake()
    {
        if (handManager == null)
        {
            handManager = FindFirstObjectByType<OppHandManager>();
        }
        if (drawManager == null)
        {
            drawManager = FindFirstObjectByType<OppDrawManager>();
        }
    }

    void Update()
    {
        if (startBattleRun)
        {
            BattleSetup();
        }
    }

    public void BattleSetup()
    {
        handManager.BattleSetup();
        drawManager.InitDrawPile(deck);
        drawManager.battleSetup(startingHandSize);
        startBattleRun = false;
    }
}
