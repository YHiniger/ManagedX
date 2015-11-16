using System;


namespace ManagedX // .Diagnostics ?
{

	/// <summary>A performance counter.</summary>
	public sealed class PerformanceCounter
	{

		private TimeSpan lastUpdateTime;
		private TimeSpan elapsedTime;
		private int frameCount;
		private int lastFrameRate;


		/// <summary>Initializes a new <see cref="PerformanceCounter"/>.</summary>
		public PerformanceCounter()
		{
		}


		/// <summary>Gets the last measured tick rate.</summary>
		public int TickRate { get { return lastFrameRate; } }


		/// <summary>Updates the frame rate counter.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "High performance required." )]
		public void Tick( ref TimeSpan time )
		{
			frameCount++;
			elapsedTime += time - lastUpdateTime;
			
			var elapsed = elapsedTime.TotalSeconds;
			if( elapsed > 1.0 )
			{
				lastFrameRate = (int)( (double)frameCount / elapsed );
				frameCount = 0;
				elapsedTime = TimeSpan.Zero;
			}

			lastUpdateTime = time;
		}

	}

}