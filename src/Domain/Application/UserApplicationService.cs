﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Application.Models;
using Domain.Domain.Users;

namespace Domain.Application
{
    public class UserApplicationService {
        private readonly IUserRepository userRepository;
        private readonly UserService userService;

        public UserApplicationService(IUserRepository userRepository) {
            this.userRepository = userRepository;
            userService = new UserService(userRepository);
        }

        public void RegisterUser(string username, string firstname, string familyname) {
            var user = new User(
                new UserName(username),
                new FullName(firstname, familyname)
            );
            if (userService.IsDuplicated(user)) {
                throw new Exception("重複しています");
            } else {
                userRepository.Save(user);
            }
        }

        public void ChangeUserInfo(string id, string username, string firstname, string familyname) {
            var targetId = new UserId(id);
            var target = userRepository.Find(targetId);
            if (target == null) {
                throw new Exception("not found. target id:" + id);
            }
            var newUserName = new UserName(username);
            target.ChangeUserName(newUserName);
            if(userService.IsDuplicated(target))
            {
                throw new Exception("重複しています");
            }

            var newName = new FullName(firstname, familyname);
            target.ChangeName(newName);
            userRepository.Save(target);
        }

        public void RemoveUser(string id) {
            var targetId = new UserId(id);
            var target = userRepository.Find(targetId);
            if (target == null) {
                throw new Exception("not found. target id:" + id);
            }
            userRepository.Remove(target);
        }

        public UserModel GetUserInfo(string id) {
            var userId = new UserId(id);
            var target = userRepository.Find(userId);
            if (target == null) {
                return null;
            }

            return new UserModel(target);
        }

        public List<UserSummaryModel> GetUserList() {
            var users = userRepository.FindAll();
            return users.Select(x => new UserSummaryModel(x)).ToList();
        }
    }

}
