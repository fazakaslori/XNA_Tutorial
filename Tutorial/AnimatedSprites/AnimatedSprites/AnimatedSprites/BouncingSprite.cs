using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimatedSprites
{
    class BouncingSprite : AutomatedSprite
    {
        public BouncingSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset,
            Point currentFrame, Point sheetSize, Vector2 speed, string collisionCueName)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, collisionCueName)
        { 

        }

        public BouncingSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset,
            Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame, string collisionCueName)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, millisecondsPerFrame, collisionCueName)
        { 

        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += Direction;

            //move the plus along the X AXIS
            position.X += moveSpeed.X;
            if (position.X > clientBounds.Width - frameSize.X || position.X <= 0)
            {
                moveSpeed.X *= -1;
            }

            //move the plus along the Y AXIS
            position.Y += moveSpeed.Y;
            if (position.Y >= clientBounds.Height - frameSize.Y || position.Y <= 0)
            {
                moveSpeed.Y *= -1;
            }

            base.Update(gameTime, clientBounds);
        }
    }
}
