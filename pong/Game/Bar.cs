using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace pong
{
    class Bar
    {
        const int speed = 5;

        public int Width => texture == null ? 0 : texture.Width;
        public int Height => texture == null ? 0 : texture.Height;
        public Rectangle Bounds
        {
            get => new Rectangle((int)Position.X - CenterX,
                                 (int)Position.Y - CenterY,
                                 Width,
                                 Height);
        }

		public Vector2 Position { get => position; set => position = value; }
		Vector2 position;

        int CenterX { get => Width / 2; }
        int CenterY { get => Height / 2; }
		readonly Texture2D texture;
        readonly int topLimit;
        readonly int bottomLimit;

        public Bar(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("bar");
            int margin = 16;
            topLimit = (texture.Height / 2) + margin;
            bottomLimit = (PongGame.VirtualHeight - texture.Height / 2) - margin;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, 0f, new Vector2(CenterX, CenterY),
                             new Vector2(1f, 1f), SpriteEffects.None, 0);

        }

        internal void MoveUp()
        {
            if (Position.Y > topLimit)
            {
                position.Y = Position.Y - speed;
            }
        }

        internal void MoveDown()
        {
            if (Position.Y < bottomLimit)
            {
                position.Y = Position.Y + speed;
            }
        }
    }
}