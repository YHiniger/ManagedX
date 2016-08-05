using System;


namespace ManagedX
{

	/// <summary>Enumerates indicators used in comparison operators.</summary>
	/// <seealso cref="ComparisonOperator"/>
	[Flags]
	public enum ComparisonOperators : int
	{

		/// <summary>Undefined.</summary>
		None = 0x00000000,

		/// <summary>Equality comparison indicator (=); this indicator can be combined with either <see cref="Inferiority"/> or <see cref="Superiority"/>.</summary>
		Equality = 0x00000001,
		
		/// <summary>Inferiority comparison indicator (&lt;); this indicator can be combined with either <see cref="Equality"/> or <see cref="Superiority"/>.</summary>
		Inferiority = 0x00000002,
		
		/// <summary>Superiority comparison indicator (&gt;); this indicator can be combined with either <see cref="Equality"/> or <see cref="Inferiority"/>.</summary>
		Superiority = 0x00000004

	}


	/// <summary>Enumerates comparison operators.</summary>
	/// <seealso cref="ComparisonOperators"/>
	[Serializable]
	public enum ComparisonOperator : int
	{

		/// <summary>Undefined comparison operator.</summary>
		Undefined = ComparisonOperators.None,

		/// <summary>Equality comparison (=).</summary>
		Equality = ComparisonOperators.Equality,

		/// <summary>Inferiority comparison (&lt;).</summary>
		Inferiority = ComparisonOperators.Inferiority,

		/// <summary>Inferiority or equality comparison (≤).</summary>
		InferiorityOrEquality = ComparisonOperators.Inferiority | ComparisonOperators.Equality,

		/// <summary>Superiority comparison (&gt;).</summary>
		Superiority = ComparisonOperators.Superiority,

		/// <summary>Superiority or equality comparison (≥).</summary>
		SuperiorityOrEquality = ComparisonOperators.Superiority | ComparisonOperators.Equality,

		/// <summary>Inequality comparison (≠).</summary>
		Inequality = ComparisonOperators.Inferiority | ComparisonOperators.Superiority,

	}

}