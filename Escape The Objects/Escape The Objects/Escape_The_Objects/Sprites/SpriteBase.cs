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
        protected Texture2D texture;
        protected Point currentFrame;
        protected int framesPerSecond;
        protected Point sheetSize;
        protected Point frameSize;
        protected int timeSinceLastFrame;

        protected Vector2 currentPosition;

        public Point collisionOffset;
        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)currentPosition.X + collisionOffset.X, (int)currentPosition.Y + collisionOffset.Y,
                frameSize.X - 2 * collisionOffset.X, frameSize.Y - 2 * collisionOffset.Y);
            }
        }

        public SpriteBase(Texture2D texture, Point sheetSize, Point frameSize, int framesPerSec,
            Point startFrame, Vector2 position, Point collisionOffset)
        {
            this.texture = texture;
            this.sheetSize = sheetSize;
            this.frameSize = frameSize;
            this.framesPerSecond = framesPerSec;
            this.currentFrame = startFrame;
            this.currentPosition = position;
            this.collisionOffset = collisionOffset;
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
    }
}
