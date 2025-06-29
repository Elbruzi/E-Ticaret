using ElbruzWebPj.Models.MVVM;

namespace ElbruzWebPj.Models
{

    public class Cls_User
    {

        private readonly AppDbContext _context;

        public Cls_User(AppDbContext context)
        {
            _context = context;
        }

        public string LoginControl(User user)
        {

            User usr = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (usr != null)
            {
                if (usr.IsAdmin == true)
                {
                    return ("Admin");
                }
                else
                {
                    return user.Email;
                }
            }
            else
            {
                return ("error");
            }
        }

        public bool RegisterCreate(User user)
        {

            user.IsAdmin = false;
            user.Active = true;

            try
            {
                _context.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public bool IsExists(string Email, string Telephone)
        {
            try
            {
                bool result = _context.Users.Any(u => u.Email == Email || u.Telephone == Telephone);

                return result;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsPhoneNumberInvalid(string PhoneNumber)
        {
            if (PhoneNumber.Length == 10 && PhoneNumber.All(char.IsDigit))
            {
                return false;
            }
            else
            {
            return true;
            }
        }

    }
}
