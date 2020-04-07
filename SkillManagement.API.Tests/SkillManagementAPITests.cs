using System.Collections.Generic;
using System.Linq;
using LoggingService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SkillManagement.API.Controllers;
using SkillManagement.API.Models;
using SkillManagement.API.Models.Repositories;
using SkillManagement.API.Services;

namespace SkillManagement.API.Tests
{
    [TestClass]
    public class SkillManagementAPITests
    {

        List<User> ExpectedUsers;
        Mock<IGenericRepository<User>> mockUserRepository;
        UserController userController;
        Mock<IUnitOfWork> uow;
        IUserService service;


        public void InitializeTestData()
        {
            ExpectedUsers = GetUsers();
            mockUserRepository = new Mock<IGenericRepository<User>>();
            mockUserRepository.Setup(m => m.GetAll()).Returns(ExpectedUsers).Verifiable();
            uow = new Mock<IUnitOfWork>();
            uow.Setup(m => m.UserRepository).Returns(mockUserRepository.Object);
            service = new UserService(uow.Object);

        }
        [TestMethod]
        public void GetAllUser()
        {
            InitializeTestData();
            //Act
            var actual = service.GetAll();

            //Assert
            mockUserRepository.Verify();//verify that GetByID was called based on setup.
            Assert.IsNotNull(actual);//assert that a result was returned
            Assert.AreEqual(ExpectedUsers, actual);//assert that actual result was as expected


        }


        [TestMethod]
        public void GetUSer()
        {
            InitializeTestData();

            var actual = service.Get(1);

            mockUserRepository.Verify();//verify that GetByID was called based on setup.
            Assert.IsNotNull(actual);//assert that a result was returned
            Assert.AreEqual(ExpectedUsers, actual);//assert that actual result was as expected

        }

        [TestMethod]
        public void AddUser()
        {
            var userCount = service.GetAll().Count();

            Assert.AreEqual(4, userCount);

            ////Prepare
            User user = GetNewUser("firat name ", "last name", "email@gmail.com");
            ////Act
            service.Add(user);


            userCount = service.GetAll().Count(); ;

            ////Assert
            Assert.AreEqual(5, userCount);
        }

        [TestMethod]
        public void UpdateUser()
        {
            User user = new User()
            {
                Id = 2,
                FirstName = "N22",//Changed Name
                LastName = "AP2",
                EmailId = "email2.eamil@email.com"
            };

            service.Update(user);


            // Verify the change
            Assert.AreEqual("N22", service.Get(2).FirstName);
        }

        [TestMethod]
        public void DeleteUser()
        {
            Assert.AreEqual(5, service.GetAll().Count());


            service.Delete(3);


            //// Verify the change
            Assert.AreEqual(1, service.GetAll().Count());
        }

                          
        private List<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Dove",
                     EmailId = "JDove@gmail.com"
                },
                new User()
                {
                    Id = 1,
                    FirstName = "John11",
                    LastName = "Dove11",
                     EmailId = "J1Dove11@gmail.com"
                },
                new User()
                {
                    Id = 1,
                    FirstName = "John131",
                    LastName = "Dove113",
                    EmailId = "J1Dov33e11@gmail.comk"
                }
            };
        }

        private User GetUser(int id)
        {
            return new User()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Dove",
                EmailId = "JDove@gmail.com"
            };

        }

        private User GetNewUser(string fname, string lname, string email)
        {
            return new User()
            {
                FirstName = fname,
                LastName = lname,
                EmailId = email

            };
        }
    }
}
