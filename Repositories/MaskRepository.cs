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
    }
}