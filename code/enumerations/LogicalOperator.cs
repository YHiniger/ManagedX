using System;


namespace ManagedX
{

	[Flags]
	internal enum LogicalOperators : int
	{
		
		/// <summary>No operator specified.</summary>
		None = 0x00000000,

		/// <summary>NOT operator; can be set with <see cref="And"/>, <see cref="Or"/>, or <see cref="Xor"/>.</summary>
		Not = 0x00000001,
		
		/// <summary>AND operator; can be set with <see cref="Not"/>.</summary>
		And = 0x00000002,

		/// <summary>OR operator; can be set with <see cref="Not"/>.</summary>
		Or = 0x00000004,

		/// <summary>Exclusive OR operator; can be set with <see cref="Not"/>.</summary>
		Xor = 0x00000008
		
	}


	/// <summary>Enumerates logical operators.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1027:MarkEnumsWithFlags" )]
	[Serializable]
	public enum LogicalOperator : int
	{

		/// <summary>Undefined operator (pass-through).</summary>
		None = LogicalOperators.None,

		/// <summary>NOT operator.</summary>
		Not = LogicalOperators.Not,

		/// <summary>AND operator.</summary>
		And = LogicalOperators.And,

		/// <summary>OR operator.</summary>
		Or = LogicalOperators.Or,

		/// <summary>Exclusive OR operator.</summary>
		Xor = LogicalOperators.Xor,

		/// <summary>NOT AND operator.</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nand" )]
		Nand = LogicalOperators.Not | LogicalOperators.And,
		
		/// <summary>NOT OR operator.</summary>
		Nor = LogicalOperators.Not | LogicalOperators.Or,

		/// <summary>Exclusive NOR (XNOR) operator.</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Equiv" )]
		Equiv = LogicalOperators.Not | LogicalOperators.Xor
		
	}

}