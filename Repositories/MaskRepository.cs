using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class MaskRepository : RepositoryBase
    {
        public MaskRepository(RepositoryContext context) : base(context) { }

        public void SetMask(Mask mask)
        {
            var prevMask = _context.Masks.Where(m => m == mask).SingleOrDefault();
            if (prevMask != null)
            {
                prevMask.Meals = mask.Meals;
                return;
            }
            _context.Masks.Add(mask);
        }

        public IEnumerable<Mask> GetMasks(int groupId, int year, int month, int day)
        {
            return _context.Masks.Where(m =>
            m.GroupId == groupId && m.Date.Year == year && m.Date.Month == month && (m.Date.Day == day || day == 0));
        }
    }
}