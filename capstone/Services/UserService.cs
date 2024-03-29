using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using capstone.Data;
using capstone.Interfaces;
using capstone.Models;
using Microsoft.IdentityModel.Tokens;

namespace capstone.Services;

    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Context _context;

        public UserService(IUserRepository userRepository, Context context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public Iunifier Login(LoginInput loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.Password))
            {
                return null;
            }

            var user = _context.Users.SingleOrDefault(x => x.Username == loginUser.Username);
            var accountant = _context.Accountants.SingleOrDefault(x => x.Username == loginUser.Username);
            if (user != null)
            {

                if (user.Password != loginUser.Password)
                {
                    return null;
                }

                return user;
            }

            if (accountant != null)
            {
                if (accountant.Password != loginUser.Password)
                {
                    return null;
                }

                return accountant;
            }

            return null;
        }


    }
