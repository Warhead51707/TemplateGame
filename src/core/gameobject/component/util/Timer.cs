using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public delegate void TimerFinishEvent();

public class Timer : Component
{
    public event TimerFinishEvent OnTimerFinish;

    public float Time { get; private set; } = 0f;
    public bool Loop { get; private set; } = false;
    public bool Enabled { get; private set; } = true;
    private float elapsedTime = 0f;
    public Timer(GameObject parent, float time, TimerFinishEvent timerFinishEvent) : base("timer", parent)
    {
        Time = time;
        OnTimerFinish += timerFinishEvent;
    }

    public override void Update()
    {
        if (!Enabled) return;

        elapsedTime += Main.DeltaTime;

        if (elapsedTime >= Time)
        {
            OnTimerFinish?.Invoke();

            if (Loop)
            {
                elapsedTime = 0f;
            }
            else
            {
                Enabled = false;
            }
        }
    }
}
