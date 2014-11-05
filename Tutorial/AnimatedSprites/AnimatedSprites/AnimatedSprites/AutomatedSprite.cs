using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimatedSprites
{
    class AutomatedSprite : Sprite
    {
        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, 
            Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed)
        { 

        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, 
            Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, millisecondsPerFrame)
        { 

        }

        public override Vector2 Direction
        {
            get { return moveSpeed; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += Direction;

            base.Update(gameTime, clientBounds);
        }
    }
}
