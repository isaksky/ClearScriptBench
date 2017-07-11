using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Horology;

namespace ClearscriptBench {
    public class FastAndDirtyConfig : ManualConfig {
        public FastAndDirtyConfig() {
            Add(DefaultConfig.Instance);

            Add(Job.Default
                .WithLaunchCount(1)
                .WithIterationTime(new TimeInterval(100, TimeUnit.Millisecond))
                .WithWarmupCount(3)
                .WithTargetCount(3)
            );
        }
    }

    class Program {
        static void Main(string[] args) {
            var summary = BenchmarkRunner.Run(typeof(JsonVsEmbed)); //, new FastAndDirtyConfig());
            Console.ReadLine();
        }
    }
}
