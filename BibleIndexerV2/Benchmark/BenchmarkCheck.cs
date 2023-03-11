using BenchmarkDotNet.Attributes;
using BibleIndexerV2.Services.Implementations;
using System.Threading.Tasks;

namespace BibleIndexerV2.Benchmark
{
    [MemoryDiagnoser]
    internal class BenchmarkCheck
    {
        private const string _query = "jesus";

        [Benchmark]
        public async Task SearchBibleV2()
        {
            var results = await BibleService.SearchBible(_query);
        }
    }
}
