using Assimp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TemplateGame;
public class SceneCamera
{
    public float DefaultZoom { get; set; } = 1.0f;
    public float Zoom { get; set; } = 1.0f;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Matrix Matrix { get; private set; } = Matrix.Identity;

    private float followSpeed = 5f;
    private float deadzoneMultiplier = 0.2f;

    private GameObject target;

    public SceneCamera()
    {

    }

    public void Update()
    {
        float screenWidth = Main.GraphicsDeviceManager.GraphicsDevice.Viewport.Width;
        float screenHeight = Main.GraphicsDeviceManager.GraphicsDevice.Viewport.Height;

        if (target != null)
        {
            if (Vector2.Distance(Position, target.Position) > followSpeed * deadzoneMultiplier)
            {
                Vector2 roundedLerpPosition = Vector2.Round(Position * Zoom * 16) / (Zoom * 16);
                Vector2 roundedLerpTargetPosition = Vector2.Round(target.Position * Zoom * 16) / (Zoom * 16);
                float lerpAmount = 1.0f - (float)Math.Exp(-followSpeed * Main.DeltaTime);

                Position = Vector2.Lerp(roundedLerpPosition, roundedLerpTargetPosition, lerpAmount);
            }

        }

        float roundedX = -MathF.Round(Position.X * Zoom * 16) / (Zoom * 16);
        float roundedY = -MathF.Round(Position.Y * Zoom * 16) / (Zoom * 16);

        Matrix = Matrix.CreateTranslation(roundedX, roundedY, 0) *
            Matrix.CreateScale(Zoom) *
            Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
    }

    public void Translate(Vector2 translation)
    {
        Position += translation;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        Position = target.Position;
    }

    public void ResetTarget()
    {
        target = null;
    }

    public Vector2 GetScreenPostion()
    {
        return new Vector2(Matrix.Translation.X, Matrix.Translation.Y);
    }
}
