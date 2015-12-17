﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Model;
using Project.View;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Project.Controller
{
    class GameController
    {
        // Model stuff.
        PlayerSimulation playerSimulation;

        // View stuff.
        PlayerView playerView;
        Level level;
        Camera camera;

        // Textures.
        Texture2D playerTexture;

        public GameController(ContentManager Content, GraphicsDeviceManager graphics)
        {

            camera = new Camera(graphics.GraphicsDevice.Viewport);
            level = new Level();

            Tiles.Content = Content;
            
            level.Generate(new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,0,1,0,0,0},
            }, 32);

            

            // If new game, load this player texture.
            playerTexture = Content.Load<Texture2D>("PlayerSquare");

            playerSimulation = new PlayerSimulation();
            playerView = new PlayerView(camera, playerSimulation);
        }
        

        public void Play()
        {

            
        }

        public void changePlayerTexture()
        {
            // If player press 1, 2, etc. load new texture here...
            // if(input == 2) playerTexture = Content.Load<Texture2D>("PlayerTriangle");
            // if(input == 3) playerTexture = Content.Load<Texture2D>("PlayerCircle");
            // etc...

            // playerView = new PlayerView(playerTexture);
        }

        public void Update(float gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            playerSimulation.UpdateMovement(currentKeyboardState);
            playerSimulation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerView.Draw(spriteBatch, playerTexture);
            level.Draw(spriteBatch);
        }
    }
}
