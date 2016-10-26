using System;
using System.Collections.ObjectModel;


namespace ManagedX
{

	/// <summary>A generic collection of non-null, unique elements.</summary>
	/// <typeparam name="T">Type of elements in the collection.</typeparam>
	[Serializable]
	public class SafeCollection<T> : Collection<T>
		where T : class
	{


		/// <summary>Initializes a new <see cref="SafeCollection{T}"/>.</summary>
		public SafeCollection()
			: base()
		{
		}



		/// <summary>Inserts an element in the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the specified <paramref name="item"/> should be inserted.</param>
		/// <param name="item">The object to insert; must not be null.</param>
		/// <exception cref="ArgumentNullException"/>
		protected sealed override void InsertItem( int index, T item )
		{
			if( item == null )
				throw new ArgumentNullException( "item" );

			if( !base.Contains( item ) )
				base.InsertItem( index, item );
		}


		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new element; must not be null.</param>
		/// <exception cref="ArgumentNullException"/>
		protected sealed override void SetItem( int index, T item )
		{
			if( item == null )
				throw new ArgumentNullException( "item" );

			if( !base.Contains( item ) )
				base.SetItem( index, item );
		}

	}

}