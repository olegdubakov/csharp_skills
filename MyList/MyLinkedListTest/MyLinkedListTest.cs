using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLinkedList;

namespace MyLinkedListTest
{
    [TestClass]
    public class MyLinkedListTest
    {
        [TestMethod]
        public void Add_Firs_Added()
        {
            //Arrange
            var testList = new MyLinkedList<int>();

            //Act
            testList.Add(33);

            //Assert
            Assert.AreEqual("33;", testList.ToString());
        }

        [TestMethod]
        public void AddAfter_ElementExist_Ok()
        {
            //Arrange
            var testList = new MyLinkedList<int>();

            testList.Add(33);
            testList.Add(44);

            //Act
            testList.AddAfter(33, 55);

            //Assert
            Assert.AreEqual("33;55;44;", testList.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddAfter_ElementNotExist_InvalidOperationException()
        {
            //Arrange
            var testList = new MyLinkedList<int>();

            testList.Add(22);
            testList.Add(44);

            //Act
            testList.AddAfter(33, 55);
        }

        [TestMethod]
        public void AddAfter_AddAfterLastElement_Added()
        {
            //Arrange
            var testList = new MyLinkedList<int>();

            testList.Add(22);
            testList.Add(44);
            
            //Act
            testList.AddAfter(44, 55);
        }

        [TestMethod]
        public void Remove_ElementExist_Removed()
        {
            //Arrange
            var testList = new MyLinkedList<int>();

            testList.Add(33);
            testList.Add(44);
            testList.Add(55);

            //Act
            testList.Remove(44);

            //Assert
            Assert.AreEqual("33;55;", testList.ToString());
        }

        [TestMethod]
        public void Remove_RemoveFirstElement_Removed()
        {
            //Arrange
            var testList = new MyLinkedList<int>();

            testList.Add(33);
            testList.Add(44);
            testList.Add(55);

            //Act
            testList.Remove(33);

            //Assert
            Assert.AreEqual("44;55;", testList.ToString());
        }

        [TestMethod]
        public void Remove_RemoveLastElement_Removed()
        {
            //Arrange
            var testList = new MyLinkedList<int>();

            testList.Add(33);
            testList.Add(44);
            testList.Add(55);

            //Act
            testList.Remove(55);

            //Assert
            Assert.AreEqual("33;44;", testList.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_ElementNotExist_InvalidOperationException()
        {
            //Arrange
            var testList = new MyLinkedList<int>();

            testList.Add(33);
            testList.Add(44);
            testList.Add(55);

            //Act
            testList.Remove(77);
        }
    }
}
