using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Escape_The_Objects.Sprites
{
    public abstract class MovableSprite : SpriteBase
    {
        protected Vector2 speed;
        protected Rectangle bounds;

        public MovableSprite(Texture2D texture, Point sheetSize, Point frameSize, int framesPerSec,
            Point startFrame, Vector2 position, Rectangle bounds, Vector2 moveSpeed, Point collionRectangle)
            : base(texture, sheetSize, frameSize, framesPerSec, startFrame, position, collionRectangle)
        {
            this.speed = moveSpeed;
            this.bounds = bounds;
        }

        public abstract void CalculatePosition();

        public override void Update(GameTime gameTime)
        {
            CalculatePosition();
            base.Update(gameTime);
        }
    }
}
