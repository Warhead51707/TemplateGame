using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public abstract class TileRule
{
    public Tile Parent { get; set; }
    public TileRuleModel.RuleProperties Properties { get; set; }
    public TileRule(Tile parent, TileRuleModel.RuleProperties ruleProperties)
    {
        Parent = parent;
        Properties = ruleProperties;
    }

    public virtual void Initialize() { }

    public virtual void OnPlace() { }

    public virtual void NeighborUpdate() { }

    public virtual void Draw() { }
}
