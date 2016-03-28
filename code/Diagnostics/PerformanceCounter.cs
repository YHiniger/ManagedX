using System;


namespace ManagedX // .Diagnostics ?
{

	/// <summary>A performance counter.</summary>
	public sealed class PerformanceCounter
	{

		private TimeSpan lastUpdateTime;
		private TimeSpan elapsedTime;
		private uint tickCount;
		private int lastTickRate;



		/// <summary>Initializes a new <see cref="PerformanceCounter"/>.</summary>
		public PerformanceCounter()
		{
		}



		/// <summary>Gets the last measured tick rate.</summary>
		public int TickRate { get { return lastTickRate; } }


		/// <summary>Updates the tick rate counter.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "High performance required." )]
		public void Tick( ref TimeSpan time )
		{
			tickCount++;
			elapsedTime += time - lastUpdateTime;
			
			var elapsed = elapsedTime.TotalSeconds;
			if( elapsed >= 1.0 )
			{
				lastTickRate = (int)( (double)tickCount / elapsed );
				tickCount = 0;
				elapsedTime = TimeSpan.Zero;
			}

			lastUpdateTime = time;
		}


		/// <summary>Returns a string representing the <see cref="TickRate"/>.</summary>
		/// <returns>Returns a string representing the <see cref="TickRate"/>.</returns>
		public sealed override string ToString()
		{
			return lastTickRate.ToString( System.Globalization.CultureInfo.InvariantCulture );
		}

	}

}