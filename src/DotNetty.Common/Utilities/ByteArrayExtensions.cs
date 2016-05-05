// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Common.Utilities
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    ///     Extension methods used for slicing byte arrays
    /// </summary>
    public static class ByteArrayExtensions
    {
        public static readonly byte[] Empty = new byte[0];

        public static byte[] Slice(this byte[] array, int length)
        {
            Contract.Requires(array != null);

            if (length > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), $"length({length}) cannot be longer than Array.length({array.Length})");
            }
            return Slice(array, 0, length);
        }

        public static byte[] Slice(this byte[] array, int index, int length)
        {
            Contract.Requires(array != null);

            if (index + length > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), $"index: ({index}), length({length}) index + length cannot be longer than Array.length({array.Length})");
            }
            var result = new byte[length];
            Array.Copy(array, index, result, 0, length);
            return result;
        }

        public static void SetRange(this byte[] array, int index, byte[] src)
        {
            SetRange(array, index, src, 0, src.Length);
        }

        public static void SetRange(this byte[] array, int index, byte[] src, int srcIndex, int srcLength)
        {
            Contract.Requires(array != null);
            Contract.Requires(src != null);
            if (index + srcLength > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(srcLength), $"index: ({index}), srcLength({srcLength}) index + length cannot be longer than Array.length({array.Length})");
            }
            if (srcIndex + srcLength > src.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(srcLength), $"index: ({srcIndex}), srcLength({srcLength}) index + length cannot be longer than src.length({src.Length})");
            }

            Array.Copy(src, srcIndex, array, index, srcLength);
        }
    }
}