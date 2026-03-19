using UnityEngine;
using UnityEditor;

public class CardGenerator
{
    [MenuItem("Card Game/Generate All 52 Cards")]
    public static void GenerateCards()
    {
        string folder = "Assets/Cards";

        // Create the folder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets", "Cards");

        Suit[] suits = (Suit[])System.Enum.GetValues(typeof(Suit));

        foreach (Suit suit in suits)
        {
            for (int value = 1; value <= 13; value++)
            {
                PlayingCardData card = ScriptableObject.CreateInstance<PlayingCardData>();
                card.suit = suit;
                card.value = value;

                string name = $"{card.DisplayName().Replace(" ", "_")}";
                AssetDatabase.CreateAsset(card, $"{folder}/{name}.asset");
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Generated 52 cards!");
    }
}