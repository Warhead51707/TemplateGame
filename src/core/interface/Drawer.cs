using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame.src.core;

public interface Drawer
{
    public RenderLayer RenderLayer { get; }
    void Draw();
}
