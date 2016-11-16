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


		/// <summary>Initializes a new <see cref="SafeCollection{T}"/> from a list.</summary>
		/// <param name="list">The list to copy to this collection; must not be null.
		/// <para>Null references are ignored, as well as duplicates.</para>
		/// </param>
		/// <exception cref="ArgumentNullException"/>
		public SafeCollection( System.Collections.Generic.IList<T> list )
			: base()
		{
			if( list == null )
				throw new ArgumentNullException( "list" );

			for( var e = 0; e < list.Count; ++e )
			{
				var element = list[ e ];
				if( element != null && !base.Contains( element ) )
					base.Add( element );
			}
		}



		/// <summary>Inserts an element in the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the specified <paramref name="item"/> should be inserted.</param>
		/// <param name="item">The object to insert; must not be null.</param>
		/// <exception cref="ArgumentNullException"/>
		protected override void InsertItem( int index, T item )
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
		protected override void SetItem( int index, T item )
		{
			if( item == null )
				throw new ArgumentNullException( "item" );

			if( !base.Contains( item ) )
				base.SetItem( index, item );
		}

	}

}