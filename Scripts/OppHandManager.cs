using UnityEngine;
using cardgame;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class OppHandManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handTransform;
    public float arcRadius = -800f;
    public float xSpacing = 35f;

    public List<GameObject> cardsInHand = new List<GameObject>();

    private void Start()
    {
        
    }

    public void AddCardToHand(Card cardData)
    {
        cardPrefab.GetComponent<CardDisplay>().cardData = cardData;
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        UpdateHandVisuals();
    }

    void Update()
    {
        //UpdateHandVisuals();
    }

    public void BattleSetup()
    {
        
    }

    public void UpdateHandVisuals()

    {
        int cardCount = cardsInHand.Count;
        if (cardCount == 0) return;

        float arcAngle = Mathf.Min(xSpacing, cardCount * 15f);
        float startAngle = -arcAngle / 2f;

        for (int i = 0; i < cardCount; i++)
        {
            float t = cardCount == 1 ? 0.5f : (float)i / (cardCount - 1);
            float angle = startAngle + t * arcAngle;
            float rad = Mathf.Deg2Rad * angle;

            float x = Mathf.Sin(rad) * arcRadius;
            float y = (1 - Mathf.Cos(rad)) * arcRadius;

            cardsInHand[i].transform.localPosition = new Vector3(x, y, 0f);

            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
