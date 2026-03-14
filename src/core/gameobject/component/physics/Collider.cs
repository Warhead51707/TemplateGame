using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TemplateGame;

public class Collider : Component
{
    public bool IsColliding { get; private set; } = false;
    public Rectangle CollidingRectangle { get; private set; } = Rectangle.Empty;
    public Rectangle OverlapRectangle { get; private set; } = Rectangle.Empty;
    public Rectangle CollisionBox = Rectangle.Empty;
    public Vector2 MovementOffset = Vector2.Zero;

    private Color collisionBoxColor = new Color(Color.Blue, 0.3f);

    public Collider(GameObject parent, Rectangle rectangle) : base("collider", parent)
    {
        CollisionBox = rectangle;
    }

    public Rectangle GetCollider()
    {
        return new Rectangle((int)Parent.Position.X + CollisionBox.X + (int)MovementOffset.X, (int)Parent.Position.Y + CollisionBox.Y + (int)MovementOffset.Y, CollisionBox.Width, CollisionBox.Height);
    }

    public override void Update()
    {
        CheckColliding();
    }

    public void CheckColliding()
    {
        Rectangle parentBox = GetCollider();

        List<Rectangle> collisionBoxes = new List<Rectangle>();

        Scene currentScene = Main.SceneManager.CurrentScene;

        foreach (Collider collider in currentScene.GetGameObjectComponents<Collider>())
        {
            if (collider == this)
            {
                continue;
            }

            collisionBoxes.Add(GetCollider());
        }

        foreach (TileGrid tilegrid in currentScene.GetGameObjectComponents<TileGrid>())
        {
            collisionBoxes.AddRange(tilegrid.GetCollisionBoxes());
        }

        foreach (Rectangle rectangle in collisionBoxes)
        {
            if (parentBox == rectangle)
            {
                continue;
            }

            if (parentBox.Intersects(rectangle))
            {
                //Debug.WriteLine($"Collision detected between {Parent.Name} and rectangle at {rectangle.X}, {rectangle.Y}");

                IsColliding = true;
                CollidingRectangle = rectangle;
                OverlapRectangle = Rectangle.Intersect(parentBox, rectangle);
                return;
            }
        }

        IsColliding = false;
        CollidingRectangle = Rectangle.Empty;
        OverlapRectangle = Rectangle.Empty;
    }

    public override void Draw()
    {
        base.Draw();

        if (!DebugToggles.ShowColliders) { return; }

        Vector2 boxPosition = new Vector2((int)Parent.Position.X + CollisionBox.X + (int)MovementOffset.X, (int)Parent.Position.Y + CollisionBox.Y + (int)MovementOffset.Y);
        Rectangle destination = new Rectangle((int)boxPosition.X, (int)boxPosition.Y, CollisionBox.Width, CollisionBox.Height);

        DrawManager.SpriteBatch.Draw(DrawManager.Pixel, destination, null, collisionBoxColor, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
    }
}
