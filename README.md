<strong>Overview</strong>

The Xamarin site currently has a beginner's tutorial on working with CocosSharp with Xamarin Studio on a Mac.  The Xamarin tutorial falls short on describing the necessary steps for someone interested in using Visual Studio 2015 as the IDE of choice for developing games that leverage the CocosSharp framework.

This tutorial is intended as entry point for people new to CocosSharp development in Visual Studio 2015.  It aligns with the existing tutorial found on Xamarin, but is tailored to working within the Visual Studio 2015 IDE.  Additionally, this tutorial describes the steps necessary to create a single solution which will deploy to Android, iPhone, and Windows Phone platforms.

<strong>Required Components</strong>
<ul>
	<li>Visual Studio 2015 (any edition)</li>
	<li>Licenses for Android/iPhone Xamarin development in Visual Studio depending on the platforms you are interested in deploying to</li>
	<li>Complete my previous tutorial &lt;CocosSharp in VS2015: Installing Xamarin's Default Templates&gt;</li>
</ul>
<strong>Topics Covered</strong>
<ul>
	<li>Creating a new CocosSharp project based on existing templates</li>
	<li>Adding common CocosSharp visual elements to the game logic</li>
	<li>Implementing a CCLayer class that will contain the main game objects and logic</li>
</ul>
<strong>1. Create a new solution</strong>

Open Visual Studio and select File -&gt; New -&gt; Project.  Select CocosSharp -&gt; Mobile -&gt; Portable -&gt; Empty Game - Mobile Portable.  Name the project PaddleBall.

<strong>Fig 1.1 - New Project Window</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig1.2_PclCreation.png" target="_blank"><img class="alignnone wp-image-254 size-large" src="https://jbeck.me/wp-content/uploads/2015/08/Fig1.2_PclCreation-1024x701.png" alt="Fig1.2_PclCreation" width="620" height="424" /></a>

Setup the Xamarin.iOS Build Host.  The solution will be created with default targets for Android, Windows, and iOS.  If you don't have a Mac build host available, you can still continue with the tutorial, but you won't be able to load and run the iOS project.

For more information on configuring the Mac host, see <a href="https://developer.xamarin.com/guides/ios/getting_started/installation/windows/">https://developer.xamarin.com/guides/ios/getting_started/installation/windows/</a>

<strong>Fig 1.2 - Solution Template</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig1.3_SolutionDefaultProjects.png" target="_blank"><img class="  aligncenter wp-image-255 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig1.3_SolutionDefaultProjects.png" alt="Fig1.3_SolutionDefaultProjects" width="371" height="351" /></a>

<strong>2. Build and run the template solution</strong>

Select Build -&gt; Build Solution to verify the projects build successfully with no errors.

<strong>Fig 2.1 - Build Solution</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig2.1_BuildSolution.png" target="_blank"><img class="alignnone wp-image-256 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig2.1_BuildSolution.png" alt="Fig2.1_BuildSolution" width="703" height="403" /></a>

With the emulators properly configured, at this point it is possible to run the solution on each target platform.  I prefer to run the majority of my testing on physical devices.  You can run the project on a physical device, or an emulator hosted on the Windows OS, or even an iPhone emulator hosted on your Mac XCode host.

In the solution explorer, right click on the project you want to run, select Set as Startup Project.

<strong>Fig 2.2 - Set Startup Project</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig2.2_SetAsStartupProject.png" target="_blank"><img class="alignnone wp-image-257 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig2.2_SetAsStartupProject.png" alt="Fig2.2_SetAsStartupProject" width="560" height="833" /></a>

In the toolbar, select the target device/emulator you want to run the application on.

<strong>Fig 2.3 - "Run" Menu Bar</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig2.3_RunSolution.png" target="_blank"><img class="alignnone wp-image-258 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig2.3_RunSolution.png" alt="Fig2.3_RunSolution" width="890" height="117" /></a>

Hit F5 to run the application.  Below is a screenshot of the game running on a Galaxy S6 device with no modifications

<strong>Fig 2.4 - Running Default Template</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig2.4_RunningOnGalaxyS6.png"><img class="alignnone wp-image-259 size-large" src="https://jbeck.me/wp-content/uploads/2015/08/Fig2.4_RunningOnGalaxyS6-1024x576.png" alt="Fig2.4_RunningOnGalaxyS6" width="620" height="349" /></a>

<strong>3. Remove the floating menu in the Android project</strong>

Depending on the version of Xamarin you have installed, sometimes a floating menu is displayed when applications are run in full-screen mode.  To disable this floating menu, using the Solution Explorer, open the PaddleBall.Droid -&gt; Properties -&gt; AndroidManifest.xml file.

Change the value &lt;uses-sdk /&gt; to &lt;uses-sdk android:minSdkVersion="17" /&gt;

Re-run the android project and verify the floating menu has dissapeared.

<strong>4. Create the GameLayer class.</strong>

Add a new class called GameLayer to the PaddleBall (Portable) PCL.

<strong>Fig 4.1 - Add New Class</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig4.1_AddNewFile.png"><img class="alignnone wp-image-260 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig4.1_AddNewFile.png" alt="Fig4.1_AddNewFile" width="929" height="651" /></a>

Replace the code in GameLayer with the following code.

<strong>GameLayer.cs New Code:</strong>
<pre lang="csharp" escaped="true">using System.Collections.Generic;
using CocosSharp;
namespace PaddleBall
{
	public class GameLayer : CCLayer
	{
		CCSprite paddleSprite;
		public GameLayer()
		{
			// "paddle" refers to the paddle.png image
			paddleSprite = new CCSprite( "paddle" );
			paddleSprite.PositionX = 100;
			paddleSprite.PositionY = 100;
			AddChild( paddleSprite );
		}
		protected override void AddedToScene()
		{
			base.AddedToScene();
			// Use the bounds to layout the positioning of our drawable assets
			CCRect bounds = VisibleBoundsWorldspace;
			// Register for touch events
			var touchListener = new CCEventListenerTouchAllAtOnce();
			touchListener.OnTouchesEnded = OnTouchesEnded;
			AddEventListener( touchListener, this );
		}
		void OnTouchesEnded( List touches, CCEvent touchEvent )
		{
			if( touches.Count &gt; 0 )
			{
				// Perform touch handling here
			}
		}
	}
}
</pre>
Update the AppDelegate.cs code by replacing the reference to the IntroLayer class with the newly added GameLayer class.

<strong>AppDelegate.cs Old Code:</strong>
<pre lang="csharp" escaped="true">...
var scene = new CCScene(mainWindow);
var introLayer = new IntroLayer();

scene.AddChild(introLayer);

mainWindow.RunWithScene(scene);
...</pre>
<strong>AppDelegate.cs New Code:</strong>
<pre lang="csharp" escaped="true">...
var scene = new CCScene(mainWindow);
var gameLayer = new GameLayer();

scene.AddChild(gameLayer);

mainWindow.RunWithScene(scene);
...</pre>
<strong>5. Add the paddle image.</strong>

Download and unzip the Content.zip file located &lt;here&gt;.  Extract the contents to a location on your machine.  Add the "paddle.png" file to the Android, iPhone, and Windows Phone projects.

Note: The default template includes folders for fonts, sounds, as well as high-definition and low-definition graphics files.  We won't be leveraging these in this simple tutorial, so we will just add the graphics to the root of each project's Content folder.

In the Android project, right-click on the Assets -&gt; Content folder and select Add -&gt; Existing Item.  Add the paddle.png file that is located in the Content folder you previously downloaded and unzipped.

<strong>Fig 5.1 - Add paddle.png to PaddleBall.Droid</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig5.3.1_PaddleGraphicInAndroidProject.png" target="_blank"><img class="alignnone wp-image-261 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig5.3.1_PaddleGraphicInAndroidProject.png" alt="Fig5.3.1_PaddleGraphicInAndroidProject" width="344" height="345" /></a>

In the iPhone project, right click on the Content folder and select Add -&gt; Existing Item.  Add the paddle.png file that is located in the Content folder you previously downloaded and unzipped.

<strong>Fig 5.2 - Add paddle.png to PaddleBall.iOS</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig5.3.2_PaddleGraphicIniPhoneProject.png"><img class="alignnone wp-image-262 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig5.3.2_PaddleGraphicIniPhoneProject.png" alt="Fig5.3.2_PaddleGraphicIniPhoneProject" width="349" height="373" /></a>

In the Windows Phone project, right click on the Content folder and select Add -&gt; Existing Item.  Add the paddle.png file that is located in the Content folder you previously downloaded and unzipped.

<strong>Fig 5.3 - Add paddle.png to PaddleBall.WinPhone</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig5.3.3_PaddleGraphicInWinPhoneProject.png" target="_blank"><img class="alignnone wp-image-263 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig5.3.3_PaddleGraphicInWinPhoneProject.png" alt="Fig5.3.3_PaddleGraphicInWinPhoneProject" width="344" height="327" /></a>

<strong>6. Update the projects to run in portrait mode</strong>

In the PCL project, update the AppDelegate.cs file.  In the ApplicationDidFinishLaunching method, swap the values for width and height.

<strong>AppDelegate.cs - Old Code:</strong>
<pre lang="csharp" escaped="true">...
var desiredWidth = 1024.0f;
var desiredHeight = 768.0f;
...</pre>
<strong>AppDelegate.cs - New Code:</strong>
<pre lang="csharp" escaped="true">...
var desiredWidth = 768.0f;
var desiredHeight = 1024.0f;
...</pre>
Update the Android project to only run in Portrait mode.  In the Solution Explorer, open the PaddleBall.Droid -&gt; Program.cs file.  Change the line that reads ScreenOrientation = ScreenOrientation.SensorLandscape to read ScreenOrientation = ScreenOrientation.Portrait

<strong>Program.cs - Old Code:</strong>
<pre lang="csharp" escaped="true">...
[Activity(Label = "PaddleBall.Droid"
    , MainLauncher = true
    , Icon = "@drawable/icon"
    , Theme = "@style/Theme.Splash"
    , AlwaysRetainTaskState = true
    , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
    , ScreenOrientation = ScreenOrientation.SensorLandscape
    , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
...</pre>
<strong>Program.cs - New Code:
</strong>
<pre lang="csharp" escaped="true">...
[Activity(Label = "PaddleBall.Droid"
    , MainLauncher = true
    , Icon = "@drawable/icon"
    , Theme = "@style/Theme.Splash"
    , AlwaysRetainTaskState = true
    , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
    , ScreenOrientation = ScreenOrientation.Portrait
    , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
...</pre>
Update the iOS project to only run in Portrait mode.  In the Solution Explorer, open the  PaddleBall.iOS -&gt; Properties file. Under iOS Application, deselect Landscape Left and Landscape Right from the Supported Device Orientations.
<strong>Fig 6.1 - Old iOS Device Orientation Configuration</strong>
<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig6.3.1_iOSLandscapeSelection.png" target="_blank"><img class="alignnone wp-image-264 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig6.3.1_iOSLandscapeSelection.png" alt="Fig6.3.1_iOSLandscapeSelection" width="564" height="215" /></a>

<strong>Fig 6.2 - New iOS Device Orientation Configuration</strong>
<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig6.3.2_iOSLandscapeSelection.png" target="_blank"><img class="alignnone wp-image-265 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig6.3.2_iOSLandscapeSelection.png" alt="Fig6.3.2_iOSLandscapeSelection" width="562" height="205" /></a>

Update the Windows Phone project to only run in Portrait mode.  In the Solution Explorer, open the PaddleBall.WinPhone -&gt; Package.appxmanifest file.  In the Application tab, under Supported rotations, check the checkbox for Portrait.

<strong>Fig 6.3 - Win Phone Device Orientation Configuration</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig6.4_WinPhonePortrait.png" target="_blank"><img class="alignnone wp-image-266 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig6.4_WinPhonePortrait.png" alt="Fig6.4_WinPhonePortrait" width="732" height="133" /></a>

Run the application and verify the paddle displays in portrait mode only

<strong>Fig 6.4 - Running Current Project</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig6.5_PaddleScreenshot.png" target="_blank"><img class="alignnone wp-image-267 size-large" src="https://jbeck.me/wp-content/uploads/2015/08/Fig6.5_PaddleScreenshot-576x1024.png" alt="Fig6.5_PaddleScreenshot" width="576" height="1024" /></a>

<strong>7. Adding the Ball sprite</strong>

Add the "ball.png" file to the Android, iPhone, and Windows Phone projects.  In the Android project, right-click on the Assets -&gt; Content folder and select Add -&gt; Existing Item.  Add the ball.png file that is located in the Content folder you previously downloaded and unzipped.

<strong>Fig 7.1 - Adding ball.png to Droid Project</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig7.1.1_BallGraphicInAndroidProject.png" target="_blank"><img class="alignnone wp-image-268 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig7.1.1_BallGraphicInAndroidProject.png" alt="Fig7.1.1_BallGraphicInAndroidProject" width="368" height="368" /></a>

In the iPhone project, right click on the Content folder and select Add -&gt; Existing Item.  Add the ball.png file that is located in the Content folder you previously downloaded and unzipped.

<strong>Fig 7.2 - Adding ball.png to iOS Project</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig7.1.2_BallGraphicIniOsProject.png" target="_blank"><img class="alignnone wp-image-269 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig7.1.2_BallGraphicIniOsProject.png" alt="Fig7.1.2_BallGraphicIniOsProject" width="369" height="392" /></a>

In the Windows Phone project, right click on the Content folder and select Add -&gt; Existing Item.  Add the ball.png file that is located in the Content folder you previously downloaded and unzipped.

<strong>Fig 7.3 - Adding ball.png to Droid Project</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig7.1.3_BallGraphicInWinPhoneProject.png" target="_blank"><img class="alignnone wp-image-270 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig7.1.3_BallGraphicInWinPhoneProject.png" alt="Fig7.1.3_BallGraphicInWinPhoneProject" width="361" height="349" /></a>

Add the Ball CCSprite object to the GameLayer class by updating the GameLayer.cs code as follows:

<strong>GameLayer.cs Old Code:</strong>
<pre lang="csharp" escaped="true">...
CCSprite paddleSprite;
public GameLayer()
{
	// "paddle" refers to the paddle.png image
	paddleSprite = new CCSprite( "paddle" );
	paddleSprite.PositionX = 100;
	paddleSprite.PositionY = 100;
	AddChild( paddleSprite );
}
...</pre>
<strong>GameLayer.cs New Code:</strong>
<pre lang="csharp" escaped="true">...
CCSprite paddleSprite;
CCSprite ballSprite;
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
}
...</pre>
<strong>8. Add a font for the Score label</strong>

In the Solution Explorer, add the General\arial-22.xnb from the Content folder to the PaddleBall.Droid -&gt; Assets -&gt; Content -&gt; fonts folder and the PaddleBall.WinPhone -&gt; Content -&gt; fonts folder by selecting Add -&gt; Existing Item for each project's folder.

<strong>Fig 8.1 - Add General\arial-22.xnb to Droid and WinPhone</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig8.1_AddingDroidAndWinFontFile.png" target="_blank"><img class="alignnone wp-image-271 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig8.1_AddingDroidAndWinFontFile.png" alt="Fig8.1_AddingDroidAndWinFontFile" width="324" height="741" /></a>

iOS requires a specially processed font file to work.  In the Solution Explorer, add the iOS\arial-22.xnb file from the Content folder to the PaddleBall.iOS -&gt; Content -&gt; fonts folder by selecting Add -&gt; Existing Item on the folder.

<strong>Fig 8.2 - Add iOS\arial-22.xnb to iOS project</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig8.2_iOSFontFile.png" target="_blank"><img class="aligncenter wp-image-272 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig8.2_iOSFontFile.png" alt="Fig8.2_iOSFontFile" width="345" height="420" /></a>

The CocosSharp content pipeline requires the font files to be included as content in the projects, so update the file's Build Action property to Content for each font file.  The Android project is the one exception, this project should have the Build Action set to AndroidAsset.  Remember to update the build action for all 3 font files.

<strong>Fig 8.3 - Select font file's Properties</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig8.3.1_FontFilePropertyMenuItem.png" target="_blank"><img class="aligncenter wp-image-273 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig8.3.1_FontFilePropertyMenuItem.png" alt="Fig8.3.1_FontFilePropertyMenuItem" width="528" height="330" /></a><strong>Fig 8.4 - Update Build Action to Content/AndroidAsset</strong>

<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig8.3.2_FontFilePropertiesWindow.png" target="_blank"><img class="aligncenter wp-image-252 size-full" src="https://jbeck.me/wp-content/uploads/2015/08/Fig8.3.2_FontFilePropertiesWindow.png" alt="Fig8.3.2_FontFilePropertiesWindow" width="372" height="282" /></a>

Add the Font CCLabel object to the GameLayer class by updating the GameLayer.cs code as follows:

<strong>GameLayer.cs Old Code:
</strong>
<pre lang="csharp" escaped="true">...</pre>
<pre lang="csharp" escaped="true">CCSprite paddleSprite;
CCSprite ballSprite;
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
}</pre>
<pre lang="csharp" escaped="true">...</pre>
<strong>GameLayer.cs New Code:</strong>
<pre lang="csharp" escaped="true">...
CCSprite paddleSprite;
CCSprite ballSprite;
CCLabel scoreLabel;
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
    AddChild( scoreLabel );
}
...
</pre>
<strong>9. Implement the Game Loop</strong>
Add field members to track the ball's velocity, a gravity modifier, and a score tracking field.

<strong>GameLayer.cs New Field Members:</strong>
<pre lang="csharp" escaped="true">...
public class GameLayer : CCLayer
{
	CCSprite paddleSprite;
	CCSprite ballSprite;
	CCLabel scoreLabel;

	// New field members.
	float ballXVelocity;
	float ballYVelocity;
	// How much to modify the ball's y velocity per second:
	const float gravity = 140;
	int score;
...</pre>
Create the main game loop and schedule it in GameLayer's constructor.

<strong>GameLayer.cs RunGameLogic Method:</strong>
<pre lang="csharp" escaped="true">...
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
	bool isMovingDownward = ballYVelocity &lt; 0; 	if( doesBallOverlapPaddle &amp;&amp; isMovingDownward ) 	{ 		// First let's invert the velocity: 		ballYVelocity *= -1; 		// Then let's assign a random to the ball's x velocity: 		const float minXVelocity = -300; 		const float maxXVelocity = 300; 		ballXVelocity = CCRandom.GetRandomFloat( minXVelocity, maxXVelocity ); 		// Increment the score. 		score++; 		scoreLabel.Text = "Score: " + score; 	} 	// Collision detection with sides. 	// First let’s get the ball position:    	float ballRight = ballSprite.BoundingBoxTransformedToParent.MaxX; 	float ballLeft = ballSprite.BoundingBoxTransformedToParent.MinX; 	// Then let’s get the screen edges 	float screenRight = VisibleBoundsWorldspace.MaxX; 	float screenLeft = VisibleBoundsWorldspace.MinX; 	// Check if the ball is either too far to the right or left:     	bool shouldReflectXVelocity = 		( ballRight &gt; screenRight &amp;&amp; ballXVelocity &gt; 0 ) ||
		( ballLeft &lt; screenLeft &amp;&amp; ballXVelocity &lt; 0 );
	if( shouldReflectXVelocity )
	{
		ballXVelocity *= -1;
	}
}
...</pre>
Schedule the main game loop in the GameLayer's constructor.

<strong>GameLayer.cs Constructor Update:</strong>
<pre lang="csharp" escaped="true">...
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
	AddChild( scoreLabel );

	// New Code:
	Schedule( RunGameLogic );
}
...</pre>
<strong>10. Handle touch events</strong>
Add the event handler to test for touches.

<strong>GameLayer.cs HandleTouchesMoved Event Handler:</strong>
<pre lang="csharp" escaped="true">...
void HandleTouchesMoved( System.Collections.Generic.List touches, CCEvent touchEvent )
{
	// we only care about the first touch:
	var locationOnScreen = touches[ 0 ].Location;
	paddleSprite.PositionX = locationOnScreen.X;
}
...</pre>
Set the touch event handler to the OnTouchesMoved event.

<strong>GameLayer.cs Update AddedToScene Method:</strong>
<pre lang="csharp" escaped="true">...
protected override void AddedToScene()
{
	base.AddedToScene();
	// Use the bounds to layout the positioning of our drawable assets
	CCRect bounds = VisibleBoundsWorldspace;
	// Register for touch events
	var touchListener = new CCEventListenerTouchAllAtOnce();
	touchListener.OnTouchesEnded = OnTouchesEnded;

	// New Code: Register the custom event handler:
	touchListener.OnTouchesMoved = HandleTouchesMoved;

	AddEventListener( touchListener, this );
}
...</pre>
<strong>Fig 11.1 - Final Game Running</strong>
<a href="https://jbeck.me/wp-content/uploads/2015/08/Fig9_RunningGame.png" target="_blank"><img class="alignnone wp-image-253 size-large" src="https://jbeck.me/wp-content/uploads/2015/08/Fig9_RunningGame-576x1024.png" alt="Fig9_RunningGame" width="576" height="1024" /></a>

<strong>Further Reading</strong>

At this point you have successfully created (and potentially deployed) a simple but functional game to the 3-primary mobile platforms, all utilizing C#, Visual Studio 2015, and CocosSharp in Xamarin.  If you are interested in more infomration related to CocosSharp in general, I recommend visiting the following sites:
<ul>
	<li>CocosSharp on GitHub: <a href="https://github.com/mono/CocosSharp">https://github.com/mono/CocosSharp</a></li>
	<li>Samples written in C# for CocosSharp: <a href="https://github.com/mono/cocos-sharp-samples">https://github.com/mono/cocos-sharp-samples</a></li>
	<li>CocosSharp API Documentation: <a href="http://docs.go-mono.com/?link=root:/CocosSharp">http://docs.go-mono.com/?link=root:/CocosSharp</a></li>
	<li>Xamarin forums for CocosSharp: <a href="https://forums.xamarin.com/categories/cocossharp">https://forums.xamarin.com/categories/cocossharp</a></li>
</ul>
&nbsp;

Additionally, please check back soon for the next part in my CocosSharp series!
