using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class MaskRepository : RepositoryBase
    {
        public MaskRepository(RepositoryContext context) : base(context) { }

        public void AddMask(Mask mask)
        {
            _context.Masks.Add(mask);
        }
        public void AddMasks(List<Mask> masks)
        {
            _context.Masks.AddRange(masks);
        }
        public IEnumerable<Mask> GetMasks(int groupId, int year, int month, int day, bool track = false)
        {
            var query = track ? _context.Masks : _context.Masks.AsNoTracking();
            return query.Where(m =>
            m.GroupId == groupId && m.Date.Year == year && m.Date.Month == month && (m.Date.Day == day || day == 0));
        }

        public Mask? GetMask(int groupId, int year, int month, int day, bool track = false)
        {
            var query = track ? _context.Masks : _context.Masks.AsNoTracking();
            return query.Where(m =>
            m.GroupId == groupId && m.Date.Year == year && m.Date.Month == month && m.Date.Day == day).FirstOrDefault();
        }
    }
}