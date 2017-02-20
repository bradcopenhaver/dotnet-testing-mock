using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Controllers;
using ToDoList.Models;
using Xunit;
using Moq;
using ToDoList.Models.Repositories;

namespace ToDoList.Tests.ControllerTests
{
    public class ItemsControllerTest
    {
        Mock<IItemRepository> mock = new Mock<IItemRepository>();
        EFItemRepository db = new EFItemRepository(new TestDbContext());
        private void DbSetup()
        {
            mock.Setup(m => m.Items).Returns(new Item[]
                {
                    new Item {ItemId = 1, Description = "Wash the dog" },
                    new Item {ItemId = 2, Description = "Do the dishes" },
                    new Item {ItemId = 3, Description = "Sweep the floor" }
                }.AsQueryable());
        }

        [Fact]
        public void Tests_Something_Or_Whatever()
        {
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);

            Assert.IsType<ViewResult>(controller.Index());
        }

        [Fact]
        public void Tests_Some_Other_Stuff()
        {
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);
            Item testItem = new Item();
            testItem.Description = "Wash the dog";
            testItem.ItemId = 1;
            
            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Item>;

            Assert.Contains<Item>(testItem, collection);
        }

        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            // Arrange
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
            testItem.Description = "TestDb Item";
            testItem.CategoryId = 1;

            // Act
            controller.Create(testItem);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Item>;

            // Assert
            Assert.Contains<Item>(testItem, collection);
        }
    }
}
