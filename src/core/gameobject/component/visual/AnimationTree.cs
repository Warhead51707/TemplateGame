using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public class AnimationTree : Component
{
    public Dictionary<Predicate<bool>, Animation> Animations { get; private set; } = new Dictionary<Predicate<bool>, Animation>();
    public Animation CurrentAnimation { get; private set; }

    public AnimationTree(GameObject parent) : base(parent)
    {
    }

    public void AddAnimation(string path, Predicate<bool> condition)
    {
        Animation animation = new Animation(Parent, path);
        Animations.Add(condition, animation);
    }

    public override void Update()
    {
        foreach (KeyValuePair<Predicate<bool>, Animation> animation in Animations)
        {
            if (!animation.Key(true)) continue;

            if (CurrentAnimation != null)
            {
                if (CurrentAnimation == animation.Value) continue;

                CurrentAnimation.Stop();
            }

            CurrentAnimation = animation.Value;
            CurrentAnimation.Play();
        }
    }

    public override void Draw()
    {
        if (CurrentAnimation == null) return;

        CurrentAnimation.Draw();
    }
}
