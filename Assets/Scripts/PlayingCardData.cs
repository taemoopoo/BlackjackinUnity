using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Game/Playing Card")]
public class PlayingCardData : ScriptableObject
{
    public Suit suit;
    public int value; // 1=Ace, 2-10, 11=Jack, 12=Queen, 13=King
    public Sprite artwork;
    public Sprite backArtwork;

    public CardColor Color => (suit == Suit.Hearts || suit == Suit.Diamonds)
        ? CardColor.Red
        : CardColor.Black;

    public string DisplayName()
    {
        string face = value switch
        {
            1 => "Ace",
            11 => "Jack",
            12 => "Queen",
            13 => "King",
            _ => value.ToString()
        };
        return $"{face} of {suit}";
    }
}