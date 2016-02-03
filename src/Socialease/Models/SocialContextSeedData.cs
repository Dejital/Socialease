using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Socialease.Models
{
    public class SocialContextSeedData
    {
        private readonly SocialContext _context;
        private UserManager<SocialUser> _userManager;

        public SocialContextSeedData(SocialContext context, UserManager<SocialUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("sergey@sergeyk.com") == null)
            {
                var testUser = new SocialUser
                {
                    UserName = "serge",
                    Email = "sergey@sergeyk.com"
                };
                await _userManager.CreateAsync(testUser, "P@ssw0rd!");
            }
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
