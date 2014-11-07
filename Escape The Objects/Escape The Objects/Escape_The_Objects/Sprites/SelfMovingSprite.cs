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
            Point startFrame, Vector2 position, Rectangle bounds, Vector2 moveSpeed)
            : base(texture, sheetSize, frameSize, framesPerSec, startFrame, position, bounds, moveSpeed)
        {
        }
    }
}
