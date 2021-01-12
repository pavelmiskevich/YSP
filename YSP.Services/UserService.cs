using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<User> CreateUser(User newUser)
        {
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        public async Task DeleteUser(User user)
        {
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users
                .GetByIdAsync(id);
        }

        public async Task UpdateUser(User userToBeUpdated, User user)
        {
            userToBeUpdated.Name = user.Name;
            userToBeUpdated.Email = user.Email;
            userToBeUpdated.Password = user.Password;
            userToBeUpdated.YandexLogin = user.YandexLogin;
            userToBeUpdated.YandexKey = user.YandexKey;
            userToBeUpdated.GoogleCx = user.GoogleCx;
            userToBeUpdated.GoogleKey = user.GoogleKey;
            userToBeUpdated.AvatarLink = user.AvatarLink;
            userToBeUpdated.Ip = user.Ip;
            userToBeUpdated.Birthday = user.Birthday;
            userToBeUpdated.LastVisitDate = user.LastVisitDate;
            userToBeUpdated.YandexLimit = user.YandexLimit;
            userToBeUpdated.GoogleLimit = user.GoogleLimit;
            userToBeUpdated.FreeLimit = user.FreeLimit;
            userToBeUpdated.IsActive = user.IsActive;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.Users
                .GetAllAsync();
        }
        /// <summary>
        /// Получение общего лимита Yandex
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> GetAllYandexLimit()
        {
            return await _unitOfWork.Users
                .GetAllYandexLimitAsync();
        }
        /// <summary>
        /// Получение общего лимита Google
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> GetAllGoogleLimit()
        {
            return await _unitOfWork.Users
                .GetAllGoogleLimitAsync();
        }
    }
}
