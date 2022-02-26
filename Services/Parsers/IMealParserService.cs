namespace Rollcall.Services
{
    public interface IMealParserService
    {
        public Dictionary<string, bool> ToDict(uint mask);
        public uint FromDict(Dictionary<string, bool> dto);
    }
}