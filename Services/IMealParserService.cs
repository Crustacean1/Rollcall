using Rollcall.Models;
namespace Rollcall.Services
{
    public interface IMealParserService
    {
        public int FromDto(Dictionary<string,bool>? meals, Dictionary<string,int>? schemas);
        public Dictionary<string,bool> ToDto(int mealData, Dictionary<string,int>? schemas);
    }
}