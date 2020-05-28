using System;
using System.Collections.Generic;
using System.Text;

namespace PokerOddsPro.OddsEngine.PreComputedTables
{
	public static class CardMaskTable
	{
		/// <summary>
		/// This table is equivalent to 1UL left shifted by the index.
		/// The lookup is faster than the left shift operator.
		/// </summary>
		/// 		/// SAFEEEEEE
		public static readonly ulong[] CardMasksTable =
		{
			0x1,
			0x2,
			0x4,
			0x8,
			0x10,
			0x20,
			0x40,
			0x80,
			0x100,
			0x200,
			0x400,
			0x800,
			0x1000,
			0x2000,
			0x4000,
			0x8000,
			0x10000,
			0x20000,
			0x40000,
			0x80000,
			0x100000,
			0x200000,
			0x400000,
			0x800000,
			0x1000000,
			0x2000000,
			0x4000000,
			0x8000000,
			0x10000000,
			0x20000000,
			0x40000000,
			0x80000000,
			0x100000000,
			0x200000000,
			0x400000000,
			0x800000000,
			0x1000000000,
			0x2000000000,
			0x4000000000,
			0x8000000000,
			0x10000000000,
			0x20000000000,
			0x40000000000,
			0x80000000000,
			0x100000000000,
			0x200000000000,
			0x400000000000,
			0x800000000000,
			0x1000000000000,
			0x2000000000000,
			0x4000000000000,
			0x8000000000000,
		};
	}
}
