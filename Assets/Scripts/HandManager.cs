using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    public DeckManager deckManager;
    public Vector3 startPosition;        // where the first card spawns
    public bool isDealer = false;

    private List<GameObject> cardObjects = new List<GameObject>();
    private List<PlayingCardData> cardData = new List<PlayingCardData>();

    public void AddCard(bool faceUp = true)
    {
        float cardWidth = deckManager.GetCardWidth();
        Vector3 position = startPosition + new Vector3(cardObjects.Count * cardWidth, 0, 0);
        GameObject card = deckManager.Deal(position, faceUp);

        if (card != null)
        {
            cardObjects.Add(card);
            cardData.Add(card.GetComponent<CardDisplay>().data);
        }
    }

    public int GetScore()
    {
        int score = 0;
        int aceCount = 0;

        foreach (var card in cardData)
        {
            int val = Mathf.Min(card.value, 10); // J, Q, K all become 10
            if (card.value == 1)
            {
                aceCount++;
                val = 11; // start aces as 11
            }
            score += val;
        }

        // Downgrade aces from 11 to 1 if busting
        while (score > 21 && aceCount > 0)
        {
            score -= 10;
            aceCount--;
        }

        return score;
    }

    public void RevealAll()
    {
        foreach (var cardObj in cardObjects)
            cardObj.GetComponent<CardDisplay>().SetFaceUp(true);
    }

    public void ClearHand()
    {
        foreach (var cardObj in cardObjects)
            Destroy(cardObj);
        cardObjects.Clear();
        cardData.Clear();
    }
}