using DesktopApplication.DbModel;
using System.Linq;
using System.Windows;

namespace DesktopApplication.Services
{
    public static class Authorizator
    {
        public static User CurrentUser { get => _currentUser; }
        public static Roles CurrentRole { get => _currentRole; }
        public static bool IsAuthorized { get => _isAuthorized; }

        private static User _currentUser;
        private static Roles _currentRole;
        private static bool _isAuthorized = false;

        public static bool LoginCheck(string login, string password)
        {
            try
            {
                var result = manufacturingEntities.GetContext().LoginChecker(login, password).FirstOrDefault();
                if (result != null)
                {
                    _currentUser = manufacturingEntities.GetContext().Users.Where(x => x.Login == login).First();
                    _isAuthorized = true;
                    _currentRole = (Roles)CurrentUser.Role1.Id;
                }
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                MessageBox.Show("Не удалось подключиться к БД");
            }
            return IsAuthorized;
        }
    }
}
