using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectionRegistration;

namespace RegisterTest
{
    [TestClass]
    public class RegisterTests
    {
        [TestMethod]
        public void ArrayContainerTest()
        {
            var container = new SelectionContainer();
            var elements = new int[] {2, 4, 2, 5, 1 };
            var id = container.Register(elements);
            var select = container.Take<int>(id, 2, 2);

            var firstResult = select.ElementAt(0);
            var secondResult = select.ElementAt(1);

            Assert.AreEqual(2, firstResult);
            Assert.AreEqual(5, secondResult);
        }


        [TestMethod]
        public void ListContainerTest()
        {
            var container = new SelectionContainer();
            var elements = new List<char>() {'1', '4', '6', '2', '5', '3'};
            var id = container.Register(elements);
            var select = container.Take<char>(id, 1, 2);

            var result = select.ElementAt(0);

            Assert.AreEqual('6', result);
        }


        [TestMethod]
        public void RemoveFromContainerTest()
        {
            var container = new SelectionContainer();
            var firstElements = new int[] { 1, 2, 3, 4, 5, 6 };
            var secondElements = new int[] { 1, 2, 3, 4, 5, 6 };
            var firstId = container.Register(firstElements);
            var secondId = container.Register(secondElements);
            container.Remove(firstId);

            var select = container.Take<int>(secondId, 2, 2);
            var result = select.ElementAt(0);

            Assert.AreEqual(3, result);
            Assert.AreEqual(1, container.Count);
        }


        [TestMethod]
        public void ContainerIsClearTest()
        {
            var container = new SelectionContainer();
            var elements = new int[] { 1, 2, 3, 4 };
            var id = container.Register(elements);
            container.Clear();

            Assert.AreEqual(0, container.Count);
        }


        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void RegisteringNullTest()
        {
            var container = new SelectionContainer();
            IEnumerable elements = null;
            container.Register(elements);
        }


        [TestMethod]
        public void CollectionNotFound()
        {
            var container = new SelectionContainer();
            var elements = new int[] { 1, 2, 3 };
            container.Register(elements);

            Assert.ThrowsException<Exception>(() => container.Take<int>(Guid.NewGuid(), 1));
        }


        [TestMethod]
        public void RemoveUnexistingObject()
        {
            var container = new SelectionContainer();
            var elements = new int[] { 1, 2, 3 };
            container.Register(elements);

            Assert.ThrowsException<Exception>(() => container.Remove(Guid.NewGuid()));
        }
    }
}
