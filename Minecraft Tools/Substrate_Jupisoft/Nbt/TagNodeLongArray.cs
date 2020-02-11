using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Substrate_Jupisoft.Nbt
{
    public sealed class TagNodeLongArray : TagNode
    {
        /// <summary>
        /// A workaround to be able to read partial groups of bits from the long values within the "BlockStates" long array, because sometimes bits from 2 long values need to be mixed to create a new value with varying number of bits in order to obtain a valid index of the block "Palette" array.
        /// </summary>
        internal bool[] Matriz_Bits = null;
        private long[] _data = null;

        /// <summary>
        /// Converts the node to itself.
        /// </summary>
        /// <returns>A reference to itself.</returns>
        public override TagNodeLongArray ToTagLongArray()
        {
            return this;
        }

        /// <summary>
        /// Gets the tag type of the node.
        /// </summary>
        /// <returns>The TAG_LONG_ARRAY tag type.</returns>
        public override TagType GetTagType()
        {
            return TagType.TAG_LONG_ARRAY;
        }

        /// <summary>
        /// Gets or sets a long array of tag data.
        /// </summary>
        public long[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Gets the length of the stored byte array.
        /// </summary>
        public int Length
        {
            get { return _data.Length; }
        }

        /// <summary>
        /// Constructs a new byte array node with a null data value.
        /// </summary>
        public TagNodeLongArray() { }

        /// <summary>
        /// Constructs a new byte array node.
        /// </summary>
        /// <param name="d">The value to set the node's tag data value.</param>
        public TagNodeLongArray(long[] d)
        {
            _data = d;
            Matriz_Bits = new bool[d.Length * 64];
        }

        /// <summary>
        /// Makes a deep copy of the node.
        /// </summary>
        /// <returns>A new int array node representing the same data.</returns>
        public override TagNode Copy()
        {
            long[] arr = new long[_data.Length];
            _data.CopyTo(arr, 0);

            return new TagNodeLongArray(arr);
        }

        /// <summary>
        /// Gets a string representation of the node's data.
        /// </summary>
        /// <returns>String representation of the node's data.</returns>
        public override string ToString()
        {
            return _data.ToString();
        }

        /// <summary>
        /// Gets or sets a single long at the specified index.
        /// </summary>
        /// <param name="index">Valid index within stored long array.</param>
        /// <returns>The long value at the given index of the stored byte array.</returns>
        public long this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        /// <summary>
        /// Converts a system long array to a long array node representing the same data.
        /// </summary>
        /// <param name="i">A int array.</param>
        /// <returns>A new long array node containing the given value.</returns>
        public static implicit operator TagNodeLongArray(long[] i)
        {
            return new TagNodeLongArray(i);
        }

        /// <summary>
        /// Converts a long array node to a system long array representing the same data.
        /// </summary>
        /// <param name="i">A long array node.</param>
        /// <returns>A system long array set to the node's data.</returns>
        public static implicit operator long[] (TagNodeLongArray i)
        {
            return i._data;
        }
    }
}
