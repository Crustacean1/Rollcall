using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class MaskEntity
    {
        public string Name { get; set; }
        public bool Masked { get; set; }
        public MealDate Date { get; set; }
    } 
}