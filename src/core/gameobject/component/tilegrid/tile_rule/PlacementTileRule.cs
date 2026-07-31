using MonoGame.Framework.Content.Pipeline.Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGame;

public class PlacementTileRule : TileRule
{
    public PlacementTileRule(Tile parent, TileRuleModel.RuleProperties ruleProperties) : base(parent, ruleProperties)
    {
    }

    public override void OnPlace()
    {
        NeighborUpdate();
    }

    public override void NeighborUpdate()
    {
        List<Tile> neighborTiles = Parent.GetNeighbors();

        // Valid neighbor indexes:
        foreach (Tile tile in neighborTiles) {
            if (tile != null) {
              //  Debug.WriteLine($"Neighbor tile at index {neighborTiles.IndexOf(tile)}: {tile.Name}");
            } else {
              //  Debug.WriteLine($"Neighbor tile at index {neighborTiles.IndexOf(tile)}: null");
            }
        }

        foreach (TileRuleModel.NeighborTransformation neighborTransformation in Properties.NeighborTransformations)
        {
            List<List<(int index, string type)>> neighborPatternsIndexes = GetNeighborPatternsIndexes(neighborTransformation.NeighborRules);

           // Debug.WriteLine($"Checking neighbor transformation with {neighborPatternsIndexes.Count} patterns");

            foreach (List<(int index, string type)> neighborIndex in neighborPatternsIndexes)
            {
                int matches = 0;

                foreach ((int index, string type) indexPair in neighborIndex)
                {
                   // Debug.WriteLine($"Checking neighbor at index {indexPair.index} with name {neighborTiles[indexPair.index]?.Name ?? "null"} against transformation neighbors: {string.Join(", ", neighborTransformation.Neighbors)}");

                    if (neighborTiles[indexPair.index] == null && indexPair.type == "normal")
                    {
                        continue;
                    }

                    if (neighborTiles[indexPair.index] == null && indexPair.type == "empty")
                    {
                        matches++;
                        continue;
                    }

                    if (!neighborTransformation.Neighbors.Contains(neighborTiles[indexPair.index].GetTileRule<RenderTileRule>().TextureName) && !neighborTransformation.Neighbors.Contains(neighborTiles[indexPair.index].Name) && indexPair.type == "normal") {
                        continue;
                    }

                    if (neighborTiles[indexPair.index] != null && indexPair.type == "empty")
                    {
                        continue;
                    }

                    matches++;
                }

                //Debug.WriteLine($"Pattern with indexes {string.Join(", ", neighborIndex)} has {matches} matches");

                if (matches == neighborIndex.Count)
                {
                    ApplyNeighborTransformationRules(neighborTransformation.TransformationRules);

                   // Debug.WriteLine($"Applied neighbor transformation with {neighborIndex.Count} matches");

                    return;
                }
            }
        }
    }

    public void ApplyNeighborTransformationRules(List<TileRuleModel.Rule> neighborTransformationRules)
    {
        foreach (TileRuleModel.Rule neighborTransformationRule in neighborTransformationRules)
        {
            if (neighborTransformationRule.Type == "render")
            {
                Parent.TileRules.RemoveAll(rule => rule is RenderTileRule);

                RenderTileRule renderTileRule = new RenderTileRule(Parent, neighborTransformationRule.Properties);
                Parent.TileRules.Add(renderTileRule);
            }    
        }
    }

    public List<List<(int index, string type)>> GetNeighborPatternsIndexes(List<List<string>> patterns)
    {
        List<List<(int index, string type)>> indexes = new List<List<(int index, string type)>>();

        foreach (List<string> pattern in patterns)
        {
            List<(int index, string type)> patternIndexes = new List<(int index, string type)>();
            int patternTileIndex = 0;

            for (int i = 0; i < 3; i++)
            {
                string patternLine = pattern[i];

                for (int j = 0; j < 3; j++)
                {
                    string patternTile = patternLine[j].ToString();

                    if (patternTile == "#")
                    {
                        patternIndexes.Add((patternTileIndex, "normal"));
                    }

                    if (patternTile == "!")
                    {
                        patternIndexes.Add((patternTileIndex, "empty"));
                    }

                    patternTileIndex++;
                }
            }

            indexes.Add(patternIndexes);
        }

        return indexes;
    }
}
