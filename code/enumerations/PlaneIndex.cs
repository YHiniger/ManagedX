namespace ManagedX
{

	/// <summary>Enumerates planes for use with <see cref="BoundingFrustum"/>.</summary>
	public enum PlaneIndex : int
	{

		/// <summary>The index of the near plane.</summary>
		Near,

		/// <summary>The index of the far plane.</summary>
		Far,

		/// <summary>The index of the left plane.</summary>
		Left,

		/// <summary>The index of the right plane.</summary>
		Right,

		/// <summary>The index of the top plane.</summary>
		Top,

		/// <summary>The index of the bottom plane.</summary>
		Bottom

	}

}