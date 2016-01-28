using System.Linq;

namespace Socialease.Models
{
    public class SocialContextSeedData
    {
        private readonly SocialContext _context;

        public SocialContextSeedData(SocialContext context)
        {
            _context = context;
        }
        public void EnsureSeedData()
        {
            if (!_context.PingTypes.Any())
            {
                _context.PingTypes.Add(new PingType() {Name = "Telephone"});
                _context.PingTypes.Add(new PingType() {Name = "Meeting"});
            }
            if (!_context.People.Any())
            {
                _context.People.Add(new Person() {Name = "Arturo Bandini"});
            }
            _context.SaveChanges();
        }
    }
}
