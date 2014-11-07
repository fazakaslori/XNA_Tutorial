using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Escape_The_Objects.Sprites
{
    public class SelfMovingSprite : MovableSprite
    {
        public SelfMovingSprite(Texture2D texture, Point sheetSize, Point frameSize, int framesPerSec,
            Point startFrame, Vector2 position, Rectangle bounds, Vector2 moveSpeed, Point collisionRectangle)
            : base(texture, sheetSize, frameSize, framesPerSec, startFrame, position, bounds, moveSpeed, collisionRectangle)
        {
        }

        public override void CalculatePosition()
        {
            if (speed.X != 0 && speed.Y != 0)
            {
                currentPosition.X += speed.X;
                if ((currentPosition.X + frameSize.X) >= bounds.Width || currentPosition.X <= 0)
                {
                    speed.X *= -1;
                }
                currentPosition.Y += speed.Y;
                if ((currentPosition.Y + frameSize.Y) >= bounds.Height || currentPosition.Y <= 0)
                {
                    speed.Y *= -1;
                }
            }
        }
    }
}
