namespace Rollcall.Models{
    public class Group{
        public string Name{get;set;}
        public int Id{get;set;}
        public IEnumerable<Child>? Children{get;set;}
        public IEnumerable<GroupMask>? Masks{get;set;}
    }
}