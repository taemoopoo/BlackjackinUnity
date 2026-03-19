using UnityEngine;

public class BlackjackManager : MonoBehaviour
{
    public HandManager playerHand;
    public HandManager dealerHand;
    public DeckManager deckManager;

    private bool playerTurn = false;

    void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        // Clear any previous cards
        playerHand.ClearHand();
        dealerHand.ClearHand();

        deckManager.RebuildAndShuffle();

        // Deal starting hands
        playerHand.AddCard(faceUp: true);
        dealerHand.AddCard(faceUp: false); // dealer's first card is hidden
        playerHand.AddCard(faceUp: true);
        dealerHand.AddCard(faceUp: true);

        playerTurn = true;

        Debug.Log($"Player score: {playerHand.GetScore()}");
        Debug.Log($"Dealer visible score: {dealerHand.GetScore()}");

        // Check for immediate blackjack
        if (playerHand.GetScore() == 21)
        {
            Debug.Log("Blackjack! Player wins!");
            EndRound();
        }
    }

    public void PlayerHit()
    {
        if (!playerTurn) return;

        playerHand.AddCard(faceUp: true);
        int score = playerHand.GetScore();
        Debug.Log($"Player score: {score}");

        if (score > 21)
        {
            Debug.Log("Player busts! Dealer wins!");
            EndRound();
        }
        else if (score == 21)
        {
            Debug.Log("Player hits 21!");
            PlayerStand();
        }
    }

    public void PlayerStand()
    {
        if (!playerTurn) return;
        playerTurn = false;
        DealerTurn();
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.H)) PlayerHit();
    //    if (Input.GetKeyDown(KeyCode.S)) PlayerStand();
    //    if (Input.GetKeyDown(KeyCode.R)) StartRound();
    //}

    private void DealerTurn()
    {
        // Reveal the hidden card
        dealerHand.RevealAll();

        // Dealer must hit until 17 or more
        while (dealerHand.GetScore() < 17)
        {
            dealerHand.AddCard(faceUp: true);
            Debug.Log($"Dealer hits: {dealerHand.GetScore()}");
        }

        EndRound();
    }

    private void EndRound()
    {
        playerTurn = false;

        int playerScore = playerHand.GetScore();
        int dealerScore = dealerHand.GetScore();

        Debug.Log($"--- Round Over ---");
        Debug.Log($"Player: {playerScore} | Dealer: {dealerScore}");

        if (playerScore > 21)
            Debug.Log("Player busts! Dealer wins!");
        else if (dealerScore > 21)
            Debug.Log("Dealer busts! Player wins!");
        else if (playerScore > dealerScore)
            Debug.Log("Player wins!");
        else if (dealerScore > playerScore)
            Debug.Log("Dealer wins!");
        else
            Debug.Log("Push! It's a tie!");
    }
}