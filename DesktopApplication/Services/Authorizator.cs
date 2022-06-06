using DesktopApplication.DbModel;
using System;
using System.Linq;
using System.Windows;

namespace DesktopApplication.Services
{
    public static class Authorizator
    {
        #region Private Fields
        private static User _currentUser;
        private static Roles _currentRole;
        private static bool _isAuthorized = false;
        #endregion

        public static User CurrentUser { get => _currentUser; }
        public static Roles CurrentRole { get => _currentRole; }
        public static bool IsAuthorized { get => _isAuthorized; }
        public static string RoleString => Enum.GetName(typeof(Roles), CurrentRole);

        public static string GetCaption()
        {
            return $"{CurrentUser.Login} : {RoleString}";
        }
        public static bool LoginCheck(string login, string password)
        {
            try
            {
                using (var ctx = new manufacturingEntities())
                {

                    var result = ctx.LoginChecker(login, password).FirstOrDefault();
                    if (result != null)
                    {
                        _currentUser = ctx.Users.Where(x => x.Login == login).First();
                        _isAuthorized = true;
                        _currentRole = (Roles)CurrentUser.Role1.Id;
                    }

                } }
            catch (System.Data.Entity.Core.EntityException)
            {
                MessageBox.Show("Не удалось подключиться к БД");
            }
            return IsAuthorized;
        }
        public static void LogOff()
        {
            _currentUser = null;
            _currentRole = Roles.Неавторизированный;
            _isAuthorized = false;
        }

        /// <param name="user">Пользователь, которого нужно найти в контексте</param>
        /// <param name="context">Контекст, в котором нужно искать</param>
        /// <returns>Пользователя в контексте</returns>
        public static User UserInContext(manufacturingEntities context, User user = null)
        {
            if (user == null) user = CurrentUser;
            return context.Users.FirstOrDefault(usr => usr.Login == user.Login);
        }
    }
}
