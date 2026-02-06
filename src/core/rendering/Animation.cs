using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TemplateGame;
public class Animation
{
    public Sprite Sprite { get; private set; }
    public Vector2 FrameSize { get; private set; }
    public float FrameLength { get; private set; }
    public bool Loop { get; private set; }
    public bool Playing { get; private set; } = false;

    private Vector2 Size = Vector2.Zero;

    public Animation(GameObject parent, string path)
    {
        string jsonFileContents = File.ReadAllText("Content/animation/" + path + ".json");
        AnimationModel animationModel = JsonSerializer.Deserialize<AnimationModel>(jsonFileContents);

        Sprite = new Sprite(parent, animationModel.SpriteSheet);
        FrameSize = new Vector2(animationModel.FrameSize.Width, animationModel.FrameSize.Height);
        FrameLength = animationModel.FrameLength;
        Loop = animationModel.Loop;
        Size = Sprite.Size / FrameSize;
    }

    public void Play()
    {
        if (Playing) return;

        Playing = true;

        Task.Run(async () =>
        {
            await Task.Delay(50);

            while (Playing)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    if (!Playing) break;

                    Sprite.Coordinates = new Vector2(0, y);

                    await Task.Delay((int)(FrameLength * 1000));
                }

                if (!Loop) break;
            }
        });
    }

    public void Stop()
    {
        Playing = false;
    }

    public void Draw()
    {
        Sprite.Draw(FrameSize, Size);
    }
}
