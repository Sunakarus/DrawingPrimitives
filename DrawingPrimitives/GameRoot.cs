#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

#endregion Using Statements

namespace DrawingPrimitives
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameRoot : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Circle tempCircle;
        private Rectangle tempRect;
        private Line tempLine;
        private MouseState mouse;
        private Vector2 mousePos;

        private Vector2 pointA = Vector2.Zero;

        private enum Drawing { Nothing, Circle, Rectangle, Line };

        private Drawing state = Drawing.Nothing;

        public GameRoot()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Art.pixel = Content.Load<Texture2D>("health");
            Art.tahoma = Content.Load<SpriteFont>("Tahoma");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouse = Mouse.GetState();
            mousePos = new Vector2(mouse.X, mouse.Y);

            if (mouse.LeftButton == ButtonState.Pressed && (state == Drawing.Nothing || state == Drawing.Circle))
            {
                if (pointA == Vector2.Zero)
                {
                    pointA = new Vector2(mousePos.X, mousePos.Y);
                }
                Vector2 pointB = mousePos;
                Vector2 origin = new Vector2();
                origin.X = (pointB.X + pointA.X) / 2;
                origin.Y = (pointB.Y + pointA.Y) / 2;
                float radius = (pointB - pointA).Length() / 2;
                tempCircle = new Circle(origin, radius);
                state = Drawing.Circle;
            }

            if (mouse.LeftButton == ButtonState.Released && state == Drawing.Circle)
            {
                pointA = Vector2.Zero;
                state = Drawing.Nothing;
            }

            if (mouse.MiddleButton == ButtonState.Pressed && (state == Drawing.Nothing || state == Drawing.Line))
            {
                if (pointA == Vector2.Zero)
                {
                    pointA = new Vector2(mousePos.X, mousePos.Y);
                }
                Vector2 pointB = mousePos;
                tempLine = new Line(pointA, pointB);
                state = Drawing.Line;
            }

            if (mouse.MiddleButton == ButtonState.Released && state == Drawing.Line)
            {
                pointA = Vector2.Zero;
                state = Drawing.Nothing;
            }

            if (mouse.RightButton == ButtonState.Pressed && (state == Drawing.Nothing || state == Drawing.Rectangle))
            {
                if (pointA == Vector2.Zero)
                {
                    pointA = new Vector2(mousePos.X, mousePos.Y);
                }
                Vector2 pointB = mousePos;
                int topLeftX = pointA.X < pointB.X ? (int)pointA.X : (int)pointB.X;
                int topLeftY = pointA.Y < pointB.Y ? (int)pointA.Y : (int)pointB.Y;
                float width = Math.Abs(pointB.X - pointA.X);
                float height = Math.Abs(pointB.Y - pointA.Y);
                tempRect = new Rectangle(topLeftX, topLeftY, (int)width, (int)height);
                state = Drawing.Rectangle;
            }

            if (mouse.RightButton == ButtonState.Released && state == Drawing.Rectangle)
            {
                pointA = Vector2.Zero;
                state = Drawing.Nothing;
            }

            base.Update(gameTime);
        }

        public void DrawRect(SpriteBatch spriteBatch, Rectangle rect)
        {
            /*for (int ix = rect.Left; ix <= rect.Right; ix++)
            {
                spriteBatch.Draw(Art.pixel, new Vector2(ix, rect.Top), Color.White);
                spriteBatch.Draw(Art.pixel, new Vector2(ix, rect.Bottom), Color.White);
            }
            for (int iy = rect.Top; iy <= rect.Bottom; iy++)
            {
                spriteBatch.Draw(Art.pixel, new Vector2(rect.Left, iy), Color.White);
                spriteBatch.Draw(Art.pixel, new Vector2(rect.Right, iy), Color.White);
            }
            */
            int thickness = 2;
            spriteBatch.Draw(Art.pixel, new Rectangle(rect.Left, rect.Top, rect.Width, thickness), null, Color.White);
            spriteBatch.Draw(Art.pixel, new Rectangle(rect.Left, rect.Bottom, rect.Width + thickness, thickness), null, Color.White);
            spriteBatch.Draw(Art.pixel, new Rectangle(rect.Left, rect.Top, thickness, rect.Height), null, Color.White);
            spriteBatch.Draw(Art.pixel, new Rectangle(rect.Right, rect.Top, thickness, rect.Height), null, Color.White);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (state == Drawing.Circle)
            {
                tempCircle.Draw(spriteBatch);
                spriteBatch.DrawString(Art.tahoma, tempCircle.radius.ToString(), Vector2.Zero, Color.Black);
            }
            else if (state == Drawing.Rectangle)
            {
                DrawRect(spriteBatch, tempRect);
            }
            else if (state == Drawing.Line)
            {
                tempLine.DrawLine(spriteBatch, 1);
            }
            spriteBatch.Draw(Art.pixel, mousePos, null, Color.White, 0, new Vector2(0.5f, 0.5f), 10, 0, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}