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
        private Vector2 Direction
        {
            get
            {
                Vector2 input = Vector2.Zero;

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    input.X += -1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    input.X += +1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    input.Y += -1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    input.Y += 1;
                }

                return input * speed;
            }
        }

        public UserControlledSprite(Texture2D texture, Point sheetSize, Point frameSize, int framesPerSec,
            Point startFrame, Vector2 position, Rectangle bounds, Vector2 moveSpeed, Point collisionRectangle)
            : base(texture, sheetSize, frameSize, framesPerSec, startFrame, position, bounds, moveSpeed, collisionRectangle)
        {
            speed.X = Math.Abs(moveSpeed.X);
            speed.Y = Math.Abs(moveSpeed.Y);
        }

        public override void CalculatePosition()
        {
            if (speed.X != 0 && speed.Y != 0)
            {
                currentPosition.X += Direction.X;
                if ((currentPosition.X + frameSize.X) >= bounds.Width)
                {
                    currentPosition.X = bounds.Width - frameSize.X;
                }
                if (currentPosition.X <= 0)
                {
                    currentPosition.X = 0;
                }

                currentPosition.Y += Direction.Y;
                if ((currentPosition.Y + frameSize.Y) >= bounds.Height)
                {
                    currentPosition.Y = bounds.Height - frameSize.Y;
                }
                if (currentPosition.Y <= 0)
                {
                    currentPosition.Y = 0;
                }
            }
        }
    }
}
