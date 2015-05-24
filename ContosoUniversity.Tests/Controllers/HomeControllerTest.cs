using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContosoUniversity;
using ContosoUniversity.Controllers;
using System.Data.Entity;
using ContosoUniversity.Models;
using Moq;
using ContosoUniversity.DAL;

namespace ContosoUniversity.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {

            var students = new List<Student>
            {
                new Student() { ID = 1, FirstMidName = "Jan", LastName = "Nowak", EnrollmentDate = DateTime.Now},
                new Student() { ID = 2, FirstMidName = "Adam", LastName = "Grochmal", EnrollmentDate = DateTime.Now}
            }.AsQueryable();

            var mockStudents = new Mock<DbSet<Student>>();
            mockStudents.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            mockStudents.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            mockStudents.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            mockStudents.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator()); 
            
            var mockContext = new Mock<SchoolContext>();
            mockContext.Setup(m => m.Students).Returns(mockStudents.Object);

            // Arrange
            HomeController controller = new HomeController(mockContext.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
