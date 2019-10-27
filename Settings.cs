using System;
using System.Collections.Generic;
using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ImGuiNET;

namespace DelveWalls
{
    //All properties and public fields of this class will be saved to file
    public class Settings : ISettings
    {
        [Menu("Enable")]
        public ToggleNode Enable { get; set; }

        [Menu("Maximum Range")]
        public RangeNode<int> MaxRange { get; set; }


        public Settings()
        {
            Enable = new ToggleNode(false);
            MaxRange = new RangeNode<int>(300, 1, 1000);
        }

    }
}