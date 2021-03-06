﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.View;
using Project.Controller;
using System;

namespace Project
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Project : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameController gameController;

        MouseState currentMouseState = Mouse.GetState();
        MouseState lastMouseState;

        // Menu stuff.
        MainMenuButton playButton;
        MainMenuButton exitButton;
        MainMenuButton instructionsButton;
        MainMenuButton resumeButton;
        MainMenuButton restartButton;
        MainMenuButton mainMenuButton;

        Texture2D mainMenuBg;
        Texture2D pausedBg;
        Texture2D playButtonTexture;
        Texture2D restartButtonTexture;
        Texture2D resumeButtonTexture;
        Texture2D instructionsButtonTexture;
        Texture2D exitButtonTexture;
        Texture2D mainMenuButtonTexture;

        enum GameState
        {
            MainMenu,
            Playing,
            Paused,
        }

        GameState currentGameState = GameState.MainMenu;
        GameState lastGameState;

        int screenWidth = 1024, screenHeight = 600;

        public Project()
        {
            graphics = new GraphicsDeviceManager(this);
            
            // Screen setup.
            graphics.PreferredBackBufferWidth = screenWidth; //21*32;
            graphics.PreferredBackBufferHeight = screenHeight; //10 * 32;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            
            IsMouseVisible = true;

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


            // GameControll stuff.
            gameController = new GameController(Content, graphics);

            // Menu stuff.
            mainMenuBg = Content.Load<Texture2D>("MainMenuBg");
            pausedBg = Content.Load<Texture2D>("PausedBackground");

                //Playbutton
            playButtonTexture = Content.Load<Texture2D>("PlayButton");
            playButton = new MainMenuButton(playButtonTexture, graphics.GraphicsDevice);
            playButton.setPosition(new Vector2((screenWidth / 2 - playButtonTexture.Width / 2), (screenHeight / 2 + playButtonTexture.Height)));

                // Instructions button
            instructionsButtonTexture = Content.Load<Texture2D>("InstructionsButton");
            instructionsButton = new MainMenuButton(instructionsButtonTexture, graphics.GraphicsDevice);
            instructionsButton.setPosition(new Vector2((screenWidth / 2 - instructionsButtonTexture.Width / 2), (screenHeight / 2 + instructionsButtonTexture.Height * 2)));

                // Exit button
            exitButtonTexture = Content.Load<Texture2D>("ExitButton");
            exitButton = new MainMenuButton(exitButtonTexture, graphics.GraphicsDevice);
            exitButton.setPosition(new Vector2((screenWidth / 2 - exitButtonTexture.Width / 2), (screenHeight / 2 + exitButtonTexture.Height * 3)));

                // ResumeButton
            resumeButtonTexture = Content.Load<Texture2D>("ResumeButton");
            resumeButton = new MainMenuButton(resumeButtonTexture, graphics.GraphicsDevice);
            resumeButton.setPosition(new Vector2((screenWidth / 2 - resumeButtonTexture.Width / 2), (screenHeight / 2 + resumeButtonTexture.Height)));

                //Restart button
            restartButtonTexture = Content.Load<Texture2D>("RestartButton");
            restartButton = new MainMenuButton(restartButtonTexture, graphics.GraphicsDevice);
            restartButton.setPosition(new Vector2((screenWidth / 2 - restartButtonTexture.Width / 2), (screenHeight / 2 + restartButtonTexture.Height * 2)));

                // Mainmenu button
            mainMenuButtonTexture = Content.Load<Texture2D>("MainMenuButton");
            mainMenuButton = new MainMenuButton(mainMenuButtonTexture, graphics.GraphicsDevice);
            mainMenuButton.setPosition(new Vector2((screenWidth / 2 - mainMenuButtonTexture.Width / 2), (screenHeight / 2 + mainMenuButtonTexture.Height * 3)));
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();


            switch(currentGameState)
            {
                case GameState.MainMenu:
                    if (playButton.isClicked && lastMouseState.LeftButton == ButtonState.Released)
                    {

                        // TODO: Start new game here! NOT continue on last one.
                        if(lastGameState == GameState.Paused)
                        {
                           // Test stuff. Worked like this. lastGameState is set when paused.
                            Console.WriteLine("Yep");
                        }
                        currentGameState = GameState.Playing;
                        playButton.isClicked = false;
                    }
                    if (exitButton.isClicked && lastMouseState.LeftButton == ButtonState.Released)
                    {
                        Exit();
                    }

                    playButton.Update(currentMouseState);
                    instructionsButton.Update(currentMouseState);
                    exitButton.Update(currentMouseState);
                    break;

                case GameState.Playing:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        currentGameState = GameState.Paused;
                    }


                    gameController.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;


                case GameState.Paused:
                    {
                        if (resumeButton.isClicked && lastMouseState.LeftButton == ButtonState.Released)
                        {
                            currentGameState = GameState.Playing;
                            resumeButton.isClicked = false;
                        }
                        if (restartButton.isClicked)
                        {
                            // TODO: Implement restart functionality!
                        }
                        if (mainMenuButton.isClicked && lastMouseState.LeftButton == ButtonState.Released)
                        {
                            lastGameState = currentGameState;
                            currentGameState = GameState.MainMenu;
                            mainMenuButton.isClicked = false;
                        }

                        resumeButton.Update(currentMouseState);
                        restartButton.Update(currentMouseState);
                        mainMenuButton.Update(currentMouseState);
                        break;
                    }
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

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();

                    spriteBatch.Draw(mainMenuBg, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    playButton.Draw(spriteBatch);
                    instructionsButton.Draw(spriteBatch);
                    exitButton.Draw(spriteBatch);

                    spriteBatch.End();
                    break;

                case GameState.Playing:
                    gameController.Draw(spriteBatch);
                    break;

                case GameState.Paused:

                    spriteBatch.Begin();

                    spriteBatch.Draw(pausedBg, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    resumeButton.Draw(spriteBatch);
                    restartButton.Draw(spriteBatch);
                    mainMenuButton.Draw(spriteBatch);

                    spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
