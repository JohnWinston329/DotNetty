// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Buffers
{
    using System;

    public sealed class SlicedByteBuffer : AbstractDerivedByteBuffer
    {
        readonly IByteBuffer buffer;
        readonly int adjustment;
        readonly int length;

        public SlicedByteBuffer(IByteBuffer buffer, int index, int length)
            : base(length)
        {
            if (index < 0 || index > buffer.Capacity - length)
            {
                throw new ArgumentOutOfRangeException("index", buffer + ".slice(" + index + ", " + length + ')');
            }

            var slicedByteBuf = buffer as SlicedByteBuffer;
            if (slicedByteBuf != null)
            {
                this.buffer = slicedByteBuf.buffer;
                this.adjustment = slicedByteBuf.adjustment + index;
            }
            else if (buffer is DuplicatedByteBuffer)
            {
                this.buffer = buffer.Unwrap();
                this.adjustment = index;
            }
            else
            {
                this.buffer = buffer;
                this.adjustment = index;
            }
            this.length = length;

            this.SetWriterIndex(length);
        }

        public override IByteBuffer Unwrap()
        {
            return this.buffer;
        }

        public override IByteBufferAllocator Allocator
        {
            get { return this.buffer.Allocator; }
        }

        public override ByteOrder Order
        {
            get { return this.buffer.Order; }
        }

        public override int Capacity
        {
            get { return this.length; }
        }

        public override IByteBuffer AdjustCapacity(int newCapacity)
        {
            throw new NotSupportedException("sliced buffer");
        }

        public override bool HasArray
        {
            get { return this.buffer.HasArray; }
        }

        public override byte[] Array
        {
            get { return this.buffer.Array; }
        }

        public override int ArrayOffset
        {
            get { return this.buffer.ArrayOffset + this.adjustment; }
        }

        protected override byte _GetByte(int index)
        {
            return this.buffer.GetByte(index + this.adjustment);
        }

        protected override short _GetShort(int index)
        {
            return this.buffer.GetShort(index + this.adjustment);
        }

        protected override int _GetInt(int index)
        {
            return this.buffer.GetInt(index + this.adjustment);
        }

        protected override long _GetLong(int index)
        {
            return this.buffer.GetLong(index + this.adjustment);
        }

        public override IByteBuffer Duplicate()
        {
            IByteBuffer duplicate = this.buffer.Slice(this.adjustment, this.length);
            duplicate.SetIndex(this.ReaderIndex, this.WriterIndex);
            return duplicate;
        }

        //public IByteBuffer copy(int index, int length)
        //{
        //    CheckIndex(index, length);
        //    return this.buffer.Copy(index + this.adjustment, length);
        //}

        public override IByteBuffer Copy(int index, int length)
        {
            this.CheckIndex(index, length);
            return this.buffer.Copy(index + this.adjustment, length);
        }

        public override IByteBuffer Slice(int index, int length)
        {
            this.CheckIndex(index, length);
            if (length == 0)
            {
                return Unpooled.Empty;
            }
            return this.buffer.Slice(index + this.adjustment, length);
        }

        public override IByteBuffer GetBytes(int index, IByteBuffer dst, int dstIndex, int length)
        {
            this.CheckIndex(index, length);
            this.buffer.GetBytes(index + this.adjustment, dst, dstIndex, length);
            return this;
        }

        public override IByteBuffer GetBytes(int index, byte[] dst, int dstIndex, int length)
        {
            this.CheckIndex(index, length);
            this.buffer.GetBytes(index + this.adjustment, dst, dstIndex, length);
            return this;
        }

        protected override void _SetByte(int index, int value)
        {
            this.buffer.SetByte(index + this.adjustment, value);
        }

        protected override void _SetShort(int index, int value)
        {
            this.buffer.SetShort(index + this.adjustment, value);
        }

        protected override void _SetInt(int index, int value)
        {
            this.buffer.SetInt(index + this.adjustment, value);
        }

        protected override void _SetLong(int index, long value)
        {
            this.buffer.SetLong(index + this.adjustment, value);
        }

        public override IByteBuffer SetBytes(int index, byte[] src, int srcIndex, int length)
        {
            this.CheckIndex(index, length);
            this.buffer.SetBytes(index + this.adjustment, src, srcIndex, length);
            return this;
        }

        public override IByteBuffer SetBytes(int index, IByteBuffer src, int srcIndex, int length)
        {
            this.CheckIndex(index, length);
            this.buffer.SetBytes(index + this.adjustment, src, srcIndex, length);
            return this;
        }
    }
}