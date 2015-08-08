using System.Collections.Generic;
using CocosSharp;
namespace PaddleBall
{
	public class GameLayer : CCLayer
	{
		CCSprite paddleSprite;
		CCSprite ballSprite;
		CCLabel scoreLabel;

		float ballXVelocity;
		float ballYVelocity;
		// How much to modify the ball's y velocity per second:
		const float gravity = 140;
		int score;

		public GameLayer()
		{
			// "paddle" refers to the paddle.png image
			paddleSprite = new CCSprite( "paddle" );
			paddleSprite.PositionX = 100;
			paddleSprite.PositionY = 100;
			AddChild( paddleSprite );

			// "ball" refers to the ball.png image
			ballSprite = new CCSprite( "ball" );
			ballSprite.PositionX = 320;
			ballSprite.PositionY = 600;
			AddChild( ballSprite );

			// Create a new font object and provide the font type, size, and text.
			scoreLabel = new CCLabel( "Score: 0", "fonts/arial", 22, CCLabelFormat.SpriteFont );
			scoreLabel.PositionX = 50;
			scoreLabel.PositionY = 1000;
			scoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
			scoreLabel.Color = CCColor3B.White;
			AddChild( scoreLabel );

			Schedule( RunGameLogic );
		}
		protected override void AddedToScene()
		{
			base.AddedToScene();
			// Use the bounds to layout the positioning of our drawable assets
			CCRect bounds = VisibleBoundsWorldspace;
			// Register for touch events
			var touchListener = new CCEventListenerTouchAllAtOnce();
			touchListener.OnTouchesEnded = OnTouchesEnded;

			touchListener.OnTouchesMoved = HandleTouchesMoved;

			AddEventListener( touchListener, this );
		}
		void OnTouchesEnded( List<CCTouch> touches, CCEvent touchEvent )
		{
			if( touches.Count > 0 )
			{
				// Perform touch handling here
			}
		}
		void HandleTouchesMoved( System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent )
		{
			// we only care about the first touch:
			var locationOnScreen = touches[ 0 ].Location;
			paddleSprite.PositionX = locationOnScreen.X;
		}
		void RunGameLogic( float frameTimeInSeconds )
		{
			// This is a linear approximation, so not 100% accurate
			ballYVelocity += frameTimeInSeconds * -gravity;
			ballSprite.PositionX += ballXVelocity * frameTimeInSeconds;
			ballSprite.PositionY += ballYVelocity * frameTimeInSeconds;

			// Collision detection with paddle.
			// Check if the two CCSprites overlap...
			bool doesBallOverlapPaddle = ballSprite.BoundingBoxTransformedToParent.IntersectsRect(
				paddleSprite.BoundingBoxTransformedToParent );
			// ... and if the ball is moving downward.
			bool isMovingDownward = ballYVelocity < 0;
			if( doesBallOverlapPaddle && isMovingDownward )
			{
				// First let's invert the velocity:
				ballYVelocity *= -1;
				// Then let's assign a random to the ball's x velocity:
				const float minXVelocity = -300;
				const float maxXVelocity = 300;
				ballXVelocity = CCRandom.GetRandomFloat( minXVelocity, maxXVelocity );
				// Increment the score.
				score++;
				scoreLabel.Text = "Score: " + score;
			}

			// Collision detection with sides.
			// First let’s get the ball position:   
			float ballRight = ballSprite.BoundingBoxTransformedToParent.MaxX;
			float ballLeft = ballSprite.BoundingBoxTransformedToParent.MinX;
			// Then let’s get the screen edges
			float screenRight = VisibleBoundsWorldspace.MaxX;
			float screenLeft = VisibleBoundsWorldspace.MinX;
			// Check if the ball is either too far to the right or left:    
			bool shouldReflectXVelocity =
				( ballRight > screenRight && ballXVelocity > 0 ) ||
				( ballLeft < screenLeft && ballXVelocity < 0 );
			if( shouldReflectXVelocity )
			{
				ballXVelocity *= -1;
			}
		}
	}
}