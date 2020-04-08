using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SkillManagement.API.Controllers;
using SkillManagement.API.Core.Models;
using SkillManagement.API.Core.Repositories;
using SkillManagement.API.Core.Services;
using SkillManagement.API.Services;

namespace SkillManagement.API.Tests
{
    class SkillManagement_Skills_APITest
    {
        List<Skill> ExpectedSkills;
        Mock<IGenericRepository<Skill>> mockSkillRepository;
        SkillController skillController;
        Mock<IUnitOfWork> uow;
        ISkillService service;
        public void InitializeTestData()
        {
            ExpectedSkills = GetSkills();
            mockSkillRepository = new Mock<IGenericRepository<Skill>>();
            mockSkillRepository.Setup(m => m.GetAll()).Returns(ExpectedSkills).Verifiable();
            uow = new Mock<IUnitOfWork>();
            uow.Setup(m => m.SkillRepository).Returns(mockSkillRepository.Object);
            service = new SkillService(uow.Object);

        }

        private List<Skill> GetSkills()
        {
            return new List<Skill>()
            {
                new Skill()
                {
                    Id = 1,
                    SkillName = ".Net core	.Net",
                    Description = "	.Net"

                },
                new Skill()
                {
                    Id = 2,
                    SkillName = "Asp.Net core",
                    Description = ".Net"

                },
                new Skill()
                {
                    Id = 3,
                    SkillName = "Asp.Net core",
                    Description = ".Net"

                },
                new Skill()
                {
                    Id = 4,
                    SkillName = "Web api",
                    Description = ".Net"

                },
                new Skill()
                {
                    Id = 5,
                    SkillName = "EF Core",
                    Description = ".Net"

                },
                new Skill()
                {
                    Id = 6,
                    SkillName = "SQL Server",
                    Description = "SQL"

                },
                new Skill()
                {
                    Id = 7,
                    SkillName = "Angular",
                    Description = "Angular"

                }
            };
        }

    }
}
