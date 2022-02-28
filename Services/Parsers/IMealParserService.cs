namespace Rollcall.Services
{
    public interface IAttendanceParserService
    {
        public Dictionary<string, bool> Parse(uint mask);
        public uint Marshall(Dictionary<string, bool> dto);

        public uint ChangeAttendance(uint meal, Dictionary<string, bool> newMeals);

        public uint GetFullAttendance();
        public uint GetEmptyAttendance();
    }
}