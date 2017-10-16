using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace pong
{
    class Ball
    {
        const int speed = 8;

        public int Width => texture == null ? 0 : texture.Width;
        public int Height => texture == null ? 0 : texture.Height;
        public Vector2 Position { get; set; }
        public Rectangle Bounds
        {
            get => new Rectangle((int)Position.X - CenterX,
                                 (int)Position.Y - CenterY,
                                 Width,
                                 Height);
        }

        int CenterX { get => Width / 2; }
        int CenterY { get => Height / 2; }
        readonly Texture2D texture;
        Vector2 velocity;

        public Ball(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("ball");
            velocity = Vector2.UnitX * speed;
        }

        internal void Update()
        {
            Position = new Vector2(Position.X + velocity.X, Position.Y + velocity.Y);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, 0f, new Vector2(CenterX, CenterY),
                             new Vector2(1f, 1f), SpriteEffects.None, 0);
        }

        internal bool IsOutOfRightSide()
        {
            return Bounds.X > PongGame.VirtualWidth;
        }

        internal bool IsOutOfLeftSide()
        {
            return Bounds.X + Width < 0;
        }

        internal void HandleCollision(Bar player)
        {
            float borderSize = player.Height / 6;
            Console.WriteLine("{0}, {1}", player.Position.Y, Position.Y);

            if ((player.Position.Y - Position.Y) > borderSize) // TODO: Arreglar este cálculo
            {
                Console.WriteLine("Borde de arriba");
                velocity.Y = -1;
                velocity.X = velocity.X * -1;
            }
            else if ((player.Position.Y - Position.Y) < borderSize)
            {
                Console.WriteLine("Borde de abajo");
                Console.WriteLine("{0} es menor a {1}", (player.Position.Y - Position.Y), borderSize);
				velocity.Y = 1;
				velocity.X = velocity.X * -1;
            }
            else
            {
                velocity.X = velocity.X * -1;
            }

        }
    }
}