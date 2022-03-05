namespace Rollcall.Services
{
    public interface IMealParserService
    {
        public Dictionary<string, bool> MarshallMask(uint mask);
        public Dictionary<string, uint> MarshallMeal(uint mask);
        public uint Parse(Dictionary<string, bool> dto);

        public uint ChangeAttendance(uint meal, Dictionary<string, bool> newMeals);

        public uint GetFullAttendance();
        public uint GetEmptyAttendance();
    }
}