using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TemplateGame;
public class SceneCamera
{
    public float Zoom { get; set; } = 1.0f;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Matrix Matrix { get; private set; } = Matrix.Identity;

    private GameObject target;

    public SceneCamera()
    {

    }

    public void Update()
    {
        if (target != null)
        {
            Position = target.Position;
        }

        float screenWidth = Main.GraphicsDeviceManager.GraphicsDevice.Viewport.Width;
        float screenHeight = Main.GraphicsDeviceManager.GraphicsDevice.Viewport.Height;

        Matrix = Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
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
