using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Escape_The_Objects.Sprites
{
    public class StaticSprite : SpriteBase
    {
        public StaticSprite(Texture2D texture, Point sheetSize, Point frameSize, int framesPerSec,
            Point startFrame, Vector2 position)
            : base(texture, sheetSize, frameSize, framesPerSec, startFrame, position, new Rectangle(0, 0, (int)position.X, (int)position.Y), Vector2.Zero)
        {
        }
    }
}
