using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DrawingPrimitives
{
    internal class Circle
    {
        public Vector2 origin { get; set; }

        public float radius;
        const int MAGICNUMBER = 50;

        private List<Vector2> points = new List<Vector2>();

        public Circle(Vector2 origin, float radius)
        {
            this.origin = origin;
            this.radius = radius;
            GetPoints();
        }

        public void GetPoints()
        {
            points.Clear();
            float x, y;
            float increment = 1;
            if (radius >= MAGICNUMBER)
            {
                increment = 1 / (radius / MAGICNUMBER);
            }
            for (float i = 0; i < 360; i += increment)
            {
                x = origin.X + radius * (float)Math.Cos(MathHelper.ToRadians(i));
                y = origin.Y + radius * (float)Math.Sin(MathHelper.ToRadians(i));
                points.Add(new Vector2(x, y));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (radius > 0.001f)
            {
                foreach (Vector2 point in points)
                {
                    spriteBatch.Draw(Art.pixel, point, Color.White);
                }
                spriteBatch.DrawString(Art.tahoma, "No. of points: " + points.Count, origin, Color.Purple);
            }
        }
    }
}