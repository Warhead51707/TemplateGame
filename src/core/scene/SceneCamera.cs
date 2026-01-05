using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TemplateGame;
public class SceneCamera
{
    public float Zoom { get; set; } = 1.0f;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Viewport Viewport { get; private set; }
    public Matrix Matrix { get; private set; } = Matrix.Identity;

    private GameObject target;

    public SceneCamera()
    {
        Viewport = new Viewport(0, 0, Main.GraphicsDeviceManager.PreferredBackBufferWidth, Main.GraphicsDeviceManager.PreferredBackBufferHeight);
    }

    public void Update()
    {
        if (target != null)
        {
            Position = target.Position;
        }

        Matrix = Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
            Matrix.CreateScale(Zoom) *
            Matrix.CreateTranslation(Viewport.Width / 2, Viewport.Height / 2, 0);
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
