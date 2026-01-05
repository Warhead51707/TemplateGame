using Assimp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TemplateGame;

public struct RenderSettings
{
    public static RenderSettings Default = new RenderSettings(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Matrix.Identity);

    public SpriteSortMode SortMode;
    public BlendState BlendState;
    public SamplerState SamplerState;
    public DepthStencilState DepthStencilState;
    public RasterizerState RasterizerState;
    public Effect Effect;
    public Matrix TransformMatrix;
    
    public RenderSettings(SpriteSortMode spriteSortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix matrix)
    {
        SortMode = spriteSortMode;
        BlendState = blendState;
        SamplerState = samplerState;
        DepthStencilState = depthStencilState;
        RasterizerState = rasterizerState;
        Effect = effect;
        TransformMatrix = matrix;
    }
}
