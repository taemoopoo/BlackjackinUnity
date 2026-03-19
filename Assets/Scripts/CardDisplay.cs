using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public PlayingCardData data;
    private bool faceUp = false;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = faceUp ? data.artwork : data.backArtwork;
    }

    public void Flip()
    {
        faceUp = !faceUp;
        Refresh();
    }

    public void SetFaceUp(bool value)
    {
        faceUp = value;
        Refresh();
    }
}