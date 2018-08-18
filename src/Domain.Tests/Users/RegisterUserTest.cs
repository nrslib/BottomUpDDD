using System;
using Domain.Application;
using Domain.Domain.Users;
using InMemoryInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.Users {
    [TestClass]
    public class UserApplicationRegisterUserTest {
        [TestMethod]
        public void TestDuplicateFail() {
            var repository = new InMemoryUserRepository();
            var username = new UserName("ttaro");
            var fullname = new FullName("taro", "tanaka");
            repository.Save(new User(username, fullname));
            var app = new UserApplicationService(repository);
            bool isOk = false;
            try {
                app.RegisterUser("ttaro", "taro", "tanaka");
            } catch (Exception e) {
                if (e.Message.StartsWith("重複")) {
                    isOk = true;
                }
            }
            Assert.IsTrue(isOk);
        }

        [TestMethod]
        public void TestRegister() {
            var repository = new InMemoryUserRepository();
            var app = new UserApplicationService(repository);
            app.RegisterUser("ttaro", "taro", "tanaka");
        }
    }
}
