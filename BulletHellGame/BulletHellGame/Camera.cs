using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurora
{
    class Camera
    {
        public Matrix transform { get; private set; }
        public void Follow(Vector2 pos,int screenWidth,int screenHeight)
        {
            var offset = Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
            transform = Matrix.CreateTranslation(-pos.X-8,-pos.Y-8,0)*offset;

        }
    }
}
