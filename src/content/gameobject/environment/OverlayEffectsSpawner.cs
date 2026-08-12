using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;
public class OverlayEffectsSpawner : GameObject
{
    private Timer spawnTimer;
    public OverlayEffectsSpawner() : base("overlay_effects_spawner", Vector2.Zero, () => new OverlayEffectsSpawner())
    {
        RenderLayer = RenderLayers.Overlay;
    }
    public override void SetComponents()
    {
        spawnTimer = new Timer(this, 0.1f, SpawnEffect);
        spawnTimer.Enabled = false;
        spawnTimer.Loop = true;
        AddComponents(spawnTimer);
    }
    private void SpawnEffect()
    {
        Random random = new Random();

        float randomX = (float)random.NextDouble() * Main.GameWindow.ClientBounds.Width;

        Vector2 randomPos = new Vector2(randomX, Position.Y);

        SnowballOverlayParticle particle = new SnowballOverlayParticle(randomPos);
        Main.SceneManager.CurrentScene.AddGameObject(particle);
    }

    public void StartSpawning()
    {
        spawnTimer.Enabled = true;
    }

    public void StopSpawning()
    {
        spawnTimer.Enabled = false;
    }

    public bool isEnabled()
    {
        return spawnTimer.Enabled;
    }
}
