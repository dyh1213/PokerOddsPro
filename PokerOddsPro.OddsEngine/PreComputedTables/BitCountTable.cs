namespace PokerOddsPro.OddsEngine.PreComputedTables
{
	public static class BitCountTable
	{
		/// <summary>
		/// Bit count table from snippets.org
		/// </summary>
		private static readonly byte[] bits =
		{
			0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4,  /* 0   - 15  */
			1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,  /* 16  - 31  */
			1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,  /* 32  - 47  */
			2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,  /* 48  - 63  */
			1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,  /* 64  - 79  */
			2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,  /* 80  - 95  */
			2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,  /* 96  - 111 */
			3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,  /* 112 - 127 */
			1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,  /* 128 - 143 */
			2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,  /* 144 - 159 */
			2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,  /* 160 - 175 */
			3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,  /* 176 - 191 */
			2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,  /* 192 - 207 */
			3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,  /* 208 - 223 */
			3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,  /* 224 - 239 */
			4, 5, 5, 6, 5, 6, 6, 7, 5, 6, 6, 7, 6, 7, 7, 8   /* 240 - 255 */
		};


		/// <summary>
		/// Fast Bitcounting method (adapted from snippets.org)
		/// </summary>
		/// <param name="bitField">ulong to count</param>
		/// <returns>number of set bits in ulong</returns>
		/// 		/// SAFEEEEEE
		public static int BitCount(ulong bitField)
		{
			return
				bits[(int)(bitField & 0x00000000000000FFUL)] +
				bits[(int)((bitField & 0x000000000000FF00UL) >> 8)] +
				bits[(int)((bitField & 0x0000000000FF0000UL) >> 16)] +
				bits[(int)((bitField & 0x00000000FF000000UL) >> 24)] +
				bits[(int)((bitField & 0x000000FF00000000UL) >> 32)] +
				bits[(int)((bitField & 0x0000FF0000000000UL) >> 40)] +
				bits[(int)((bitField & 0x00FF000000000000UL) >> 48)] +
				bits[(int)((bitField & 0xFF00000000000000UL) >> 56)];
		}
	}
}
