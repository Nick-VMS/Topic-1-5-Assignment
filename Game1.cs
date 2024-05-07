using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text.Encodings.Web;
using System.Threading;

namespace Topic_1_5_Assignment
{
    public class Game1 : Game
    {
        // Nick
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        float rockRotation = 0f, clarinetRotation = 0f;

        Random generator = new Random();
        Texture2D bikiniBottomTexture, jellyFishTexture, spongebobHouseTexture, squidwardHouseTexture, patrickHouseTexture,
            backArrowTexture, squidwardSleepingTexture, SquidwardAngryTexture, clarinetTexture, cursorTexture, garyTexture, spongebobTexture,
            patrickRockTexture, patrickFallenTexture, patrickFallingTexture;
        Vector2 jellyFishSpeed, garySpeed, spongeSpeed, patrickFallingSpeed;
        Rectangle jellyFishOneRect, jellyFishTwoRect, backgroundRect, squidwardHouseRect, spongebobHouseRect, patrickHouseRect,
            backArrowRect, squidwardRect, clarinetRect, cursorRect, garyRect, spongebobRect, rockRect, patrickFallingRect, patrickFallenRect;
        float seconds;
        bool squidSleeping = true, caughtGary = false, patrickFalling = false;
        SpriteFont textFont;
        Screen screen; // This variable will keep track of what screen/level we are on

        MouseState mouseState;
        MouseState prevMouseState;
        enum Screen
        {
            Intro,
            SpongebobHouse,
            SquidwardHouse,
            PatrickHouse
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Spongebob Universe";
            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 500; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions

            jellyFishSpeed = new Vector2(-5, 0);
            garySpeed = new Vector2(-2, 0);
            spongeSpeed = new Vector2(-3, 0);
            patrickFallingSpeed = new Vector2(2, 5);

            jellyFishOneRect = new Rectangle(50, 0, 50, 50);
            jellyFishTwoRect = new Rectangle(100, 100, 50, 50);
            backgroundRect = new Rectangle(0, 0, 800, 500);
            spongebobHouseRect = new Rectangle(590, 50, 210, 310);
            squidwardHouseRect = new Rectangle(260, 30, 190, 330);
            patrickHouseRect = new Rectangle(0, 200, 150, 200);
            backArrowRect = new Rectangle(0, 0, 100, 100);
            squidwardRect = new Rectangle(350, 325, 300, 150);
            clarinetRect = new Rectangle(425, 325, 100, 100);
            cursorRect = new Rectangle(0, 0, 100, 100);
            garyRect = new Rectangle(200, 375, 150, 100);
            spongebobRect = new Rectangle(700, 275, 150, 200);
            rockRect = new Rectangle(-70, 500, 750, 350);
            patrickFallingRect = new Rectangle(0, 0, 300, 250);
            patrickFallenRect = new Rectangle(175, 250, 400, 250);

            screen = Screen.Intro;
            IsMouseVisible = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bikiniBottomTexture = Content.Load<Texture2D>("SpongebobNeighbourhood");
            jellyFishTexture = Content.Load<Texture2D>("JellyFish");
            spongebobHouseTexture = Content.Load<Texture2D>("spongebobHouse");
            squidwardHouseTexture = Content.Load<Texture2D>("SquidwardHouse");
            patrickHouseTexture = Content.Load<Texture2D>("PatrickHouse");
            backArrowTexture = Content.Load<Texture2D>("backArrow");
            squidwardSleepingTexture = Content.Load<Texture2D>("SquidwardSleeping");
            SquidwardAngryTexture = Content.Load<Texture2D>("SquidwardAngry");
            clarinetTexture = Content.Load<Texture2D>("Clarinet");
            cursorTexture = Content.Load<Texture2D>("SpongebobHand");
            garyTexture = Content.Load<Texture2D>("Gary");
            spongebobTexture = Content.Load<Texture2D>("SpongeChasing");
            patrickRockTexture = Content.Load<Texture2D>("PatrickRock");
            patrickFallingTexture = Content.Load<Texture2D>("PatrickFalling");
            patrickFallenTexture = Content.Load<Texture2D>("PatrickFallen");

            textFont = Content.Load<SpriteFont>("Text");
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();


            //updating location for cursor
            cursorRect.X = mouseState.X - 30;
            cursorRect.Y = mouseState.Y;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Separates all of the logic for the different screens
            if (screen == Screen.Intro)
            {
                IntroScreenUpdate(gameTime, mouseState);

            }
            else if (screen == Screen.SpongebobHouse)
            {
                SpongeScreenUpdate(gameTime, mouseState);
            }
            else if (screen == Screen.SquidwardHouse)
            {
                SquidScreenUpdate(gameTime, mouseState);
            }
            else if (screen == Screen.PatrickHouse)
            {

                if (mouseState.LeftButton == ButtonState.Pressed && backgroundRect.Contains(mouseState.Position))
                {
                    if (rockRotation > -1f)
                    {
                        rockRotation -= 0.005f;
                    }
                }
                else if (rockRotation < -1f)
                {
                    patrickFalling = true;
                    if (patrickFalling && patrickFallingRect.Y > 250)
                    {
                        patrickFalling = false;
                    }
                    else
                    {
                        patrickFallingRect.X += (int)patrickFallingSpeed.X;
                        patrickFallingRect.Y += (int)patrickFallingSpeed.Y;
                    }
                }
            }

            //BackArrow to return to the intro screen
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                if (backArrowRect.Contains(mouseState.Position))
                { screen = Screen.Intro; }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //Intro screen
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(bikiniBottomTexture, backgroundRect, Color.White);
                _spriteBatch.Draw(jellyFishTexture, jellyFishOneRect, Color.White);
                _spriteBatch.Draw(jellyFishTexture, jellyFishTwoRect, Color.White);
            }
            //Spongebob house
            else if (screen == Screen.SpongebobHouse)
            {
                SpongeDrawUpdate(gameTime, mouseState);
            }
            //Squidward House 
            else if (screen == Screen.SquidwardHouse)
            {
                SquidDrawUpdate(gameTime, mouseState);
            }
            //Patricks House
            else if (screen == Screen.PatrickHouse)
            {
                _spriteBatch.Draw(patrickHouseTexture, backgroundRect, Color.White);
                if (patrickFalling)
                {
                    _spriteBatch.Draw(patrickFallingTexture, patrickFallingRect, Color.White);
                }
                else if (!patrickFalling && patrickFallingRect.Y > 250)
                {
                    _spriteBatch.Draw(patrickFallenTexture, patrickFallenRect, Color.White);

                }

                _spriteBatch.Draw(patrickRockTexture, rockRect, null, Color.White, rockRotation, new Vector2(0, patrickRockTexture.Height), SpriteEffects.None, 0f);
                _spriteBatch.Draw(backArrowTexture, backArrowRect, Color.White);
            }

            _spriteBatch.Draw(cursorTexture, cursorRect, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void IntroScreenUpdate(GameTime gameTime, MouseState mouseState)
        {
            //Resets squidward to sleeping
            squidSleeping = true;

            jellyFishOneRect.X += (int)jellyFishSpeed.X;
            jellyFishTwoRect.X += (int)jellyFishSpeed.X;

            if (jellyFishOneRect.Left < -50)
            {
                jellyFishOneRect.X = 850;
                jellyFishOneRect.Y = generator.Next(0, 300);
            }
            if (jellyFishTwoRect.Left < -50)
            {
                jellyFishTwoRect.X = 850;
                jellyFishTwoRect.Y = generator.Next(0, 300);
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (patrickHouseRect.Contains(mouseState.Position))
                {
                    screen = Screen.PatrickHouse;

                    //Resets evreything
                    rockRotation = 0f;
                    patrickFallingRect = new Rectangle(0, 0, 300, 250);
                }
                else if (squidwardHouseRect.Contains(mouseState.Position))
                {
                    screen = Screen.SquidwardHouse;

                    //Resets everything
                    clarinetRect = new Rectangle(425, 325, 100, 100);
                    squidSleeping = true;
                    seconds = 2;
                }
                else if (spongebobHouseRect.Contains(mouseState.Position))
                {
                    screen = Screen.SpongebobHouse;
                    
                    //Resets evreything
                    seconds = 2;
                    garyRect = new Rectangle(100, 375, 150, 100);
                    spongebobRect = new Rectangle(600, 275, 150, 200);
                    garySpeed = new Vector2(-2, 0);
                    spongeSpeed = new Vector2(-4, 0);
                    caughtGary = false;
                }
            }
        }

        public void SpongeScreenUpdate(GameTime gameTime, MouseState mouseState)
        {
            garyRect.X += (int)garySpeed.X;
            if (garyRect.X < -150)
            {
                garyRect.X = 750;
            }
            spongebobRect.X += (int)spongeSpeed.X;
            if (spongebobRect.X < -150)
            {
                spongebobRect.X = 800;
            }

            //When SB catches Gary
            if (spongebobRect.Left < garyRect.Right - 75 && spongebobRect.Intersects(garyRect))
            {
                garySpeed = new Vector2(0, 0);
                spongeSpeed = new Vector2(0, 0);
                caughtGary = true;
                seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (caughtGary && seconds < 0)
            {
                screen = Screen.Intro;
            }
        }

        public void SquidScreenUpdate(GameTime gameTime, MouseState mouseState)
        {

            if (mouseState.LeftButton == ButtonState.Pressed && squidwardRect.Contains(mouseState.Position))
            {
                squidSleeping = false;
            }
            if (!squidSleeping)
            {
                //Timer for squidward throwing clarinet, lasts 2 seconds
                seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                clarinetRect.Width += 2;
                clarinetRect.Height += 2;
                clarinetRect.Y -= 2;
                clarinetRect.X -= 2;
                clarinetRotation -= 0.1f;
            }
            //After Clarinet has been thrown, boots you out to intro screen and resets the clarinet
            if (seconds < 0)
            {
                screen = Screen.Intro;   
            }

        }

        

        public void SquidDrawUpdate(GameTime gameTime, MouseState mouseState)
        {
            //Drawing the background
            _spriteBatch.Draw(squidwardHouseTexture, backgroundRect, Color.White);
            _spriteBatch.Draw(backArrowTexture, backArrowRect, Color.White);
            //When he is sleeping (unclicked by user)
            if (squidSleeping)
            {
                _spriteBatch.Draw(squidwardSleepingTexture, squidwardRect, Color.White);
            }
            //When clicked by the user
            else if (!squidSleeping)
            {
                _spriteBatch.Draw(SquidwardAngryTexture, squidwardRect, Color.White);
                _spriteBatch.DrawString(textFont, "GET OUT!!", new Vector2(450, 275), Color.Black);
                _spriteBatch.Draw(clarinetTexture, clarinetRect, null, Color.White, clarinetRotation, new Vector2(clarinetTexture.Width/2, clarinetTexture.Height/2), SpriteEffects.None, 0f);
            }
        }

        public void SpongeDrawUpdate(GameTime gameTime, MouseState mouseState)
        {
            _spriteBatch.Draw(spongebobHouseTexture, backgroundRect, Color.White);
            _spriteBatch.Draw(backArrowTexture, backArrowRect, Color.White);
            _spriteBatch.Draw(garyTexture, garyRect, Color.White);
            _spriteBatch.Draw(spongebobTexture, spongebobRect, Color.White);
            if (caughtGary)
            {
                _spriteBatch.DrawString(textFont, "Caught You!", new Vector2(spongebobRect.X, spongebobRect.Y), Color.White);
            }

        }
    }
}