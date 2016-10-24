using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace player_animation
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spritesheet;
        float animationTimer;
        float timePerFrame;
        const int speed = 3;

        //rotation of player 1
        float angle;

        //player 1 variables
        Vector2 origin = new Vector2(52,55);
        Rectangle[] frames;
        Vector2 pos = Vector2.Zero;
        int currentFrame;

        //player 2 variables
        Rectangle[] p2frames;
        Vector2 p2pos;
        int p2currentFrame;

        public Game1()
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
            frames = new Rectangle[4];

            frames[0] = new Rectangle(2, 180, 104, 110);
            frames[1] = new Rectangle(108, 180, 104, 117);
            frames[2] = new Rectangle(214, 180, 104, 129);
            frames[3] = new Rectangle(320, 180, 104, 129);

            p2frames = new Rectangle[4];

            p2frames[0] = new Rectangle(2, 311, 104, 110);
            p2frames[1] = new Rectangle(108, 311, 104, 117);
            p2frames[2] = new Rectangle(214, 311, 104, 129);
            p2frames[3] = new Rectangle(320, 311, 104, 129);

            animationTimer = 0;

            timePerFrame = 1.0f / 24;

            currentFrame = 0;

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

            spritesheet = Content.Load<Texture2D>("planessheet");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);

            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer >= timePerFrame)
            {
                currentFrame++;
                p2currentFrame++;
                animationTimer = 0;
            }
            if (currentFrame == frames.Length)
            {
                currentFrame = 0;
            }
            if (p2currentFrame == p2frames.Length)
            {
                p2currentFrame = 0;
            }

            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Left))
            {
                pos.X -= speed;
            }
            else if (kb.IsKeyDown(Keys.Right))
            {
                pos.X += speed;

            }
            else if (kb.IsKeyDown(Keys.Up))
            {
                pos.Y -= speed;

            }
            else if (kb.IsKeyDown(Keys.Down))
            {
                pos.Y += speed;

            }

            if (kb.IsKeyDown(Keys.A))
            {
                p2pos.X -= speed;
            }
            else if (kb.IsKeyDown(Keys.D))
            {
                p2pos.X += speed;
            }
            else if (kb.IsKeyDown(Keys.W))
            {
                p2pos.Y -= speed;
            }
            else if (kb.IsKeyDown(Keys.S))
            {
                p2pos.Y += speed;
            }


            //roating p1
            if (pad1.ThumbSticks.Right != Vector2.Zero)
            {
                angle = (float)Math.Atan2(-pad1.ThumbSticks.Right.Y, pad1.ThumbSticks.Right.X);
                angle += MathHelper.PiOver2;
            }

            //intersection of p1 with p2, does not work
            if (frames[0].Intersects(p2frames[0]))
            {
                pos = Vector2.Zero;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(spritesheet, pos, frames[currentFrame], Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(spritesheet, p2pos, p2frames[p2currentFrame], Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
