using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;

namespace pong
{
    public class PongGame : Game
    {
        const int ScreenWidth = 960;
        const int ScreenHeight = 640;
        public const int VirtualWidth = 480;
        public const int VirtualHeight = 320;

        ScalingViewportAdapter viewportAdapter;
        readonly GraphicsDeviceManager graphics;
        Level level;
        SpriteBatch spriteBatch;

        public PongGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = ScreenWidth,
                PreferredBackBufferHeight = ScreenHeight,
                IsFullScreen = false
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            viewportAdapter = new ScalingViewportAdapter(GraphicsDevice, VirtualWidth, VirtualHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentManager contentManager = new ContentManager(Services, "Content");
            level = new Level(contentManager);
        }

        protected override void Update(GameTime gameTime)
        {
            level.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(),
                              samplerState: SamplerState.PointClamp);
            level.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
