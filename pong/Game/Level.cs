﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    class Level
    {
        readonly Vector2 ballStartingPosition = new Vector2(PongGame.VirtualWidth / 2, PongGame.VirtualHeight / 2);
        readonly Vector2 playerOneScorePosition = new Vector2(184, 24);
        readonly Vector2 playerTwoScorePosition = new Vector2(253, 24);
        readonly Color green = new Color(43, 255, 22);

        int playerOneScore;
        int playerTwoScore;
        readonly Bar playerOne;
        readonly Bar playerTwo;
        readonly Ball ball;
        readonly Texture2D backgroundTexture;
        SpriteFont font;

        public Level(ContentManager contentManager)
        {
            backgroundTexture = contentManager.Load<Texture2D>("background");

            playerOne = new Bar(contentManager);
            playerOne.Position = new Vector2(playerOne.Width, PongGame.VirtualHeight / 2);
            playerTwo = new Bar(contentManager);
            playerTwo.Position = new Vector2(PongGame.VirtualWidth - playerTwo.Width, PongGame.VirtualHeight / 2);
            ball = new Ball(contentManager)
            {
                Position = ballStartingPosition
            };
            font = contentManager.Load<SpriteFont>("font");
        }

        internal void Update()
        {
            HandleInput();
            HandleCollisions();
            if (ball.IsOutOfLeftSide())
            {
                playerTwoScore++;
                ball.Position = ballStartingPosition;
            }
            else if (ball.IsOutOfRightSide())
            {
                playerOneScore++;
                ball.Position = ballStartingPosition;
            }
            ball.Update();
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Vector2(0f, 0f), Color.White);
            playerOne.Draw(spriteBatch);
            playerTwo.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.DrawString(font, playerOneScore.ToString(), playerOneScorePosition, green);
            spriteBatch.DrawString(font, playerTwoScore.ToString(), playerTwoScorePosition, green);
        }

        void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                playerOne.MoveUp();
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                playerOne.MoveDown();
            }
        }

        void HandleCollisions()
        {

            if (playerOne.Bounds.Intersects(ball.Bounds))
            {
                ball.HandleCollision(playerOne);
            }
            if (playerTwo.Bounds.Intersects(ball.Bounds))
            {
                ball.HandleCollision(playerTwo);
            }
        }
    }
}