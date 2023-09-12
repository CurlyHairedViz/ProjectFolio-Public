using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projectfolio.Controllers;
using Projectfolio.Data;
using Projectfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFolioTests
{
    [TestClass]
    public class ProjectsControllerTests
    {
        private ApplicationDbContext _context;
        private ProjectsController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            var technology = new Technology
            {
                TechnologyId = 1000,
                Name = "Test Tech",
                ReleaseYear = 2021
            };
            _context.Add(technology);
            
            for(int i = 1; i <=3; i++)
            {
                var Project = new Project
                {
                    TechnologyId = 1000,
                    ProjectName = "Test Project" + i.ToString(),
                    Description = "Project for unit testing",
                    StartDate = DateTime.Now,
                    CompletionDate = DateTime.Now,
                    Owner = "Test Owner" + i.ToString(),
                    ProjectStatus = "Not Started",
                    Technology = technology
                };
                _context.Add(Project);
            }
            _context.SaveChanges();

            controller = new ProjectsController(_context);
        }

        #region Create(GET)
        [TestMethod]
        public void CreateWillReturnCreateView()
        {
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        #region Create(POST)
        [TestMethod]
        public void CreateWithInvalidModelStateReturnsCreateView()
        {
            // arrange
            // In testInitialize

            _context.Projects.Find(1).ProjectStatus = "213Test";

            controller.ModelState.AddModelError("ProjectStatus", "Not Valid Project Status");

            // act
            var result = (ViewResult)controller.Create(_context.Projects.Find(1)).Result;

            // assert
            //Assert.AreEqual(_context.Projects.Find(1), result.Model);
            Assert.AreEqual("Create", result.ViewName);
        }


        [TestMethod]
        public void CreateWithValidDataReturnsIndexView()
        {
            // Changing the project Id because an entry with same Id exists in the database
            _context.Projects.Find(3).ProjectId = 15;

            var result = (RedirectToActionResult)controller.Create(_context.Projects.Find(3)).Result;

            Assert.AreEqual("Index", result.ActionName);
        }
        #endregion

        #region Delete(GET)
        [TestMethod]
        public void DeleteNoIdsLoads404()
        {
            var result = (ViewResult)controller.Delete(null).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteWithNoDataLoads404()
        {
            _context.Projects = null;

            var result = (ViewResult)controller.Delete(1).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteWithIncorrectIdLoads404()
        {
            var result = (ViewResult)controller.Delete(100).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteWithValidIdLoadsDelete()
        {
            var result = (ViewResult)controller.Delete(1).Result;

            Assert.AreEqual("Delete", result.ViewName);
        }
        #endregion

        #region Delete(POST)
        [TestMethod]
        public void DeleteComfirmedWithSuccessLoadsIndex()
        {
            var result = (RedirectToActionResult)controller.DeleteConfirmed(1).Result;

            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void DeleteConfirmedNullProjectEntityLoadsProblem()
        {
            _context.Projects = null;

            var result = (ObjectResult)controller.DeleteConfirmed(1).Result;

            // Returns Error code 500 for Problem occurance
            Assert.AreEqual(500,result.StatusCode);
        }

        #endregion

    }
}
