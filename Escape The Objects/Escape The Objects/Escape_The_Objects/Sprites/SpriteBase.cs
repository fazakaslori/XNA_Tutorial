using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Escape_The_Objects.Sprites
{
    public abstract class SpriteBase
    {
        Texture2D texture;
        Point currentFrame;
        int framesPerSecond;
        Point sheetSize;
        Point frameSize;
        int timeSinceLastFrame;

        Vector2 currentPosition;
        protected Vector2 speed;

        Rectangle bounds;

        public SpriteBase(Texture2D texture, Point sheetSize, Point frameSize, int framesPerSec, 
            Point startFrame, Vector2 position, Rectangle bounds, Vector2 moveSpeed)
        {
            this.texture = texture;
            this.sheetSize.X = sheetSize.X; this.sheetSize.Y = sheetSize.Y;
            this.frameSize.X = frameSize.X; this.frameSize.Y = frameSize.X;
            this.framesPerSecond = framesPerSec;
            this.currentFrame.X = startFrame.X; this.currentFrame.Y = startFrame.Y;

            this.currentPosition.X = position.X; this.currentPosition.Y = position.Y;
            this.speed.X = moveSpeed.X; speed.Y = moveSpeed.Y;
            this.bounds = bounds;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw sprite
            spriteBatch.Draw(texture, currentPosition, new Rectangle(currentFrame.X * frameSize.X,
                currentFrame.Y * frameSize.Y, frameSize.X, frameSize.X), Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            SetNextFrame(gameTime);
            CalculatePosition();
        }

        private void SetNextFrame(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > 1000 / framesPerSecond)
            {
                timeSinceLastFrame -= 1000 / framesPerSecond;
                ++currentFrame.X;
                if (currentFrame.X == sheetSize.X)
                {
                    currentFrame.X = 0;

                    ++currentFrame.Y;
                    if (currentFrame.Y == sheetSize.Y)
                    {
                        currentFrame.Y = 0;
                    }
                }
            }
        }

        public void CalculatePosition()
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
