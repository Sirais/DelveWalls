using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.Elements;
using ExileCore.PoEMemory.Elements.InventoryElements;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.PoEMemory.Models;
using ExileCore.Shared;
using ExileCore.Shared.Abstract;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using SharpDX;


namespace DelveWalls
{
    public class DelveWalls : BaseSettingsPlugin<Settings>
    {
        private IngameUIElements IngameUI;


        public override void OnLoad()
        {
            Graphics.InitImage("directions.png");
        }

        public override bool Initialise()
        {
            IngameUI = GameController.IngameState.IngameUi;
            return true;
        }


        public override void Render()
        {
            if (!GameController.InGame)
                return;
            if (GameController.Area.CurrentArea.IsTown)
                return;
            if (GameController.Area.CurrentArea.IsHideout)
                return;
            if (GameController.IsLoading)
                return;
            if (IngameUI.StashElement.IsVisible)
                return;
            if (IngameUI.InventoryPanel.IsVisible)
                return;
            if (IngameUI.OpenLeftPanel.IsVisible)
                return;
            if (IngameUI.OpenRightPanel.IsVisible)
                return;

            var entites = GameController.Entities;
            foreach (Entity e in entites )
            {
                
                if (e.Path.Contains("DelveWall"))
                    wall(e);
            }
            // Run tests done. now the Plugin 

        }

        public void wall (Entity e)
        {
            if (e.IsAlive)
            {

                Vector2 delta = e.GridPos - GameController.Player.GridPos;
                double phi;
                double distance = delta.GetPolarCoordinates(out phi);
                if (distance > Settings.MaxRange) return;
                RectangleF Dir = MathHepler.GetDirectionsUV(phi, distance);

                //LogMessage($"Wall close Distance {distance}  Direction {Dir}", 1);

                RectangleF rect = GameController.Window.GetWindowRectangle();
                Vector2 center = new Vector2(rect.X + rect.Width / 2, rect.Height - 10);

                center = GameController.Game.IngameState.Camera.WorldToScreen(GameController.Player.Pos);

                RectangleF rectDirection = new RectangleF(center.X-20, center.Y-40, 40, 40);

                Graphics.DrawImage("directions.png", rectDirection, Dir, Color.LightGreen);
            }


        }

    }
}