using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Escape_The_Objects.Sprites
{
    public class UserControlledSprite : MovableSprite
    {
        public UserControlledSprite(Texture2D texture, Point sheetSize, Point frameSize, int framesPerSec,
            Point startFrame, Vector2 position, Rectangle bounds, Vector2 moveSpeed)
            : base(texture, sheetSize, frameSize, framesPerSec, startFrame, position, bounds, moveSpeed)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 inputPosition = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                inputPosition.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                inputPosition.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                inputPosition.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                inputPosition.Y += 1;

            speed.X = inputPosition.X * speed.X;
            speed.Y = inputPosition.Y * speed.Y;

            base.Update(gameTime);
        }
    }
}
