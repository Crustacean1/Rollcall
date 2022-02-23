namespace Rollcall.Models{
    public class Group{
        public string? Name{get;set;}
        public int Id{get;set;}
        ICollection<Child>? Children{get;set;}
    }
}