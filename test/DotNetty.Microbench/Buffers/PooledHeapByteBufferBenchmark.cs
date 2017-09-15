﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Microbench.Buffers
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Jobs;
    using DotNetty.Buffers;
    using DotNetty.Common;

    [CoreJob]
    [BenchmarkCategory("ByteBuffer")]
    public class PooledHeapByteBufferBenchmark
    {
        static PooledHeapByteBufferBenchmark()
        {
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;
        }

        PooledHeapByteBuffer buffer;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.buffer = (PooledHeapByteBuffer)PooledByteBufferAllocator.Default.HeapBuffer(8);
            this.buffer.WriteLong(1L);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            this.buffer.Release();
        }

        [Benchmark]
        public void CheckIndex() => this.buffer.CheckIndex(0, 8);

        [Benchmark]
        public byte GetByte() => this.buffer.GetByte(0);

        [Benchmark]
        public short GetShort() => this.buffer.GetShort(0);

        [Benchmark]
        public short GetShortLE() => this.buffer.GetShortLE(0);

        [Benchmark]
        public int GetMedium() => this.buffer.GetMedium(0);

        [Benchmark]
        public int GetMediumLE() => this.buffer.GetMediumLE(0);

        [Benchmark]
        public int GetInt() => this.buffer.GetInt(0);

        [Benchmark]
        public int GetIntLE() => this.buffer.GetIntLE(0);

        [Benchmark]
        public long GetLong() => this.buffer.GetLong(0);

        [Benchmark]
        public long GetLongLE() => this.buffer.GetLongLE(0);
    }
}
