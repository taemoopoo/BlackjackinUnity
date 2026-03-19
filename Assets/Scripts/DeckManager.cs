using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public PlayingCardData[] allCards;
    public GameObject cardPrefab;
    [Range(0f, 1f)] public float cardOverlap = 0f; // 0 = no overlap, 1 = full overlap

    private List<PlayingCardData> deck = new List<PlayingCardData>();
    private List<GameObject> dealtCards = new List<GameObject>();

    void Start()
    {
        BuildDeck();
        Shuffle();
        Debug.Log($"Deck ready: {deck.Count} cards.");
    }

    void BuildDeck()
    {
        deck.Clear();
        foreach (var card in allCards)
            deck.Add(card);
    }

    void Shuffle()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (deck[i], deck[j]) = (deck[j], deck[i]);
        }
    }

    public float GetCardWidth()
    {
        var sr = cardPrefab.GetComponent<SpriteRenderer>();
        if (sr != null && sr.sprite != null)
        {
            // bounds.size.x is the world-space width accounting for scale
            return cardPrefab.transform.localScale.x * sr.sprite.bounds.size.x;
        }
        return 1f; // fallback
    }

    public GameObject Deal(Vector3 position, bool faceUp = true)
    {
        if (deck.Count == 0)
        {
            Debug.Log("Deck is empty!");
            return null;
        }

        GameObject cardObj = Instantiate(cardPrefab, position, Quaternion.identity);

        CardDisplay display = cardObj.GetComponent<CardDisplay>();
        display.data = deck[0];
        display.SetFaceUp(faceUp);

        dealtCards.Add(cardObj);
        deck.RemoveAt(0);
        return cardObj;
    }

    public void RebuildAndShuffle()
    {
        BuildDeck();
        Shuffle();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float cardWidth = GetCardWidth();
            float spacing = cardWidth * (1f - cardOverlap);
            int count = dealtCards.Count;
            Deal(new Vector3(count * spacing, 0, 0), faceUp: true);
        }
    }
}