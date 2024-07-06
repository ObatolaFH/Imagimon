using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImagimonData
{
    public string imagimonName;
    public byte[] spriteData;
    public List<int> stats;
    public List<string> attacks;

    public ImagimonData(string imagimonName, Sprite sprite, List<int> stats, List<string> attacks)
    {
        this.imagimonName = imagimonName;
        this.spriteData = SpriteToBytes(sprite);
        this.stats = stats;
        this.attacks = attacks;
    }

    private byte[] SpriteToBytes(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        return texture.EncodeToPNG();
    }

    public Sprite BytesToSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(spriteData);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
