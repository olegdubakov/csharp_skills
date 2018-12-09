using System;
using System.Linq;
using Common;
using Common.Entities;
using Common.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests
{
    [TestClass]
    public class RegistrationServiceTests
    {
        RegistrationService _registrationService;

        [TestInitialize]
        public void TestInitialize()
        {
            this._registrationService = new RegistrationService();
        }

        [TestMethod]
        [ExpectedException(typeof(NoUpcomingEventsException))]
        public void GetUpcomingEvents_NoEvent_NoUpcomingEventsException()
        {
            this._registrationService.GetUpcomingEvents();
        }

        [TestMethod]
        [ExpectedException(typeof(NoUpcomingEventsException))]
        public void GetUpcomingEvents_PassedEventExist_NoUpcomingEventsException()
        {
            //Arrange
            var person = this.BuildNewPerson("Tony", "Stark");
            var customeEvent = this.BuildCustomEvent("New-York battle", new DateTime(2010, 01, 01), this.BuildRundomGeoPosition());
            this._registrationService.CheckIn(person, customeEvent);

            //Arrange
            this._registrationService.GetUpcomingEvents();
        }

        [TestMethod]
        public void GetUpcomingEvents_EventExist_UpcomingEventsList()
        {
            //Arrange
            var person = this.BuildNewPerson("Tony", "Stark");
            var customeEvent = this.BuildCustomEvent("New-York battle", new DateTime(2020, 01, 01), this.BuildRundomGeoPosition());
            this._registrationService.CheckIn(person, customeEvent);

            //Arrange
            var upcomingEvents = this._registrationService.GetUpcomingEvents().ToList();

            //Assert
            Assert.AreEqual(upcomingEvents.First(), customeEvent);
        }

        [TestMethod]
        public void GetUpcomingEvents_EventsExist_UpcomingEventsSortedList()
        {
            //Arrange
            var person = this.BuildNewPerson("Tony", "Stark");
            var lastEvent= this.BuildCustomEvent("Sokovia battle", new DateTime(2021, 01, 01), this.BuildRundomGeoPosition());
            var firstEvent = this.BuildCustomEvent("New-York battle", new DateTime(2020, 01, 01), this.BuildRundomGeoPosition());
            this._registrationService.CheckIn(person, lastEvent);
            this._registrationService.CheckIn(person, firstEvent);

            //Arrange
            var upcomingEvents = this._registrationService.GetUpcomingEvents().ToList();

            //Assert
            Assert.AreEqual(upcomingEvents.First(), firstEvent);
            Assert.AreEqual(upcomingEvents.Last(), lastEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(EventNotExistException))]
        public void GetEventVisitors_EventNotExist_EventNotExistException()
        {
            //Arrange
            var customEvent = this.BuildCustomEvent("New-York battle", new DateTime(2020, 01, 01), this.BuildRundomGeoPosition());

            //Arrange
            this._registrationService.GetEventVisitors(customEvent);
        }

        [TestMethod]
        public void GetEventVisitors_EventExist_ListOfVisitors()
        {
            //Arrange
            var person1 = this.BuildNewPerson("Tony", "Stark");
            var person2 = this.BuildNewPerson("Thor", "Odinson");
            var firstEvent = this.BuildCustomEvent("New-York battle", new DateTime(2020, 01, 01), this.BuildRundomGeoPosition());
            this._registrationService.CheckIn(person1, firstEvent);
            this._registrationService.CheckIn(person2, firstEvent);

            //Arrange
            var eventVisitors = this._registrationService.GetEventVisitors(firstEvent).ToList();

            //Assert
            Assert.AreEqual(eventVisitors.First(), person1);
            Assert.AreEqual(eventVisitors.Last(), person2);
        }
        
        [TestMethod]
        public void CheckIn_AddUser_UserCheckedIn()
        {
            //Arrange
            var person = this.BuildNewPerson("Tony", "Stark");
            var customeEvent = this.BuildCustomEvent("New-York battle", DateTime.Now, this.BuildRundomGeoPosition());

            //Act
            this._registrationService.CheckIn(person, customeEvent);

            //Assert
            Assert.IsTrue(this._registrationService.GetEventVisitors(customeEvent).ToList().Contains(person));

        }

        [TestMethod]
        [ExpectedException(typeof(UserIsAlreadyCheckedInException))]
        public void CheckIn_AddSameUser_UserHaveAlreadyCheckedInException()
        {
            //Arrange
            var person1 = this.BuildNewPerson("Tony", "Stark");
            var person2 = this.BuildNewPerson("Tony", "Stark");
            var customeEvent = this.BuildCustomEvent("New-York battle", DateTime.Now, this.BuildRundomGeoPosition());

            //Act
            this._registrationService.CheckIn(person1, customeEvent);
            this._registrationService.CheckIn(person2, customeEvent);
        }


        [TestMethod]
        [ExpectedException(typeof(EventNotExistException))]
        public void CheckOut_EventNotExist_EventNotExistException()
        {
            //Arrange
            var person = this.BuildNewPerson("Tony", "Stark");
            var customeEvent = this.BuildCustomEvent("New-York battle", DateTime.Now, this.BuildRundomGeoPosition());

            //Act
            this._registrationService.CheckOut(person, customeEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotCheckedInException))]
        public void CheckOut_UserNotExist_UserNotCheckedInException()
        {
            //Arrange
            var person1 = this.BuildNewPerson("Tony", "Stark");
            var person2 = this.BuildNewPerson("Thor", "Odinson");
            var customeEvent = this.BuildCustomEvent("New-York battle", DateTime.Now, this.BuildRundomGeoPosition());
            this._registrationService.CheckIn(person1, customeEvent);

            //Act
            this._registrationService.CheckOut(person2, customeEvent);
        }

        [TestMethod]
        public void CheckOut_RemoveOneUser_UserRemovedEventExist()
        {
            //Arrange
            var person1 = this.BuildNewPerson("Tony", "Stark");
            var person2 = this.BuildNewPerson("Thor", "Odinson");
            var customeEvent = this.BuildCustomEvent("New-York battle", DateTime.Now, this.BuildRundomGeoPosition());
            this._registrationService.CheckIn(person1, customeEvent);
            this._registrationService.CheckIn(person2, customeEvent);

            //Act
            this._registrationService.CheckOut(person1, customeEvent);

            //Assert
            var checkedInUSer = this._registrationService.GetEventVisitors(customeEvent).ToList().First();

            Assert.AreEqual(person2, checkedInUSer);
        }

        [TestMethod]
        [ExpectedException(typeof(EventNotExistException))]
        public void CheckOut_RemoveAllUsers_EventNotExistException()
        {
            //Arrange
            var person1 = this.BuildNewPerson("Tony", "Stark");
            var customeEvent = this.BuildCustomEvent("New-York battle", DateTime.Now, this.BuildRundomGeoPosition());
            this._registrationService.CheckIn(person1, customeEvent);
            this._registrationService.CheckOut(person1, customeEvent);

            //Act
            this._registrationService.GetEventVisitors(customeEvent);
        }

        private Person BuildNewPerson(string name, string lastName)
        {
            return new Person
            {
                Name = name,
                LastName = lastName
            };
        }

        private CustomEvent BuildCustomEvent(string name, DateTime date, GeoPosition geoPositiony)
        {
            return new CustomEvent
            {
                EventDate = date,
                Name = name,
                Position = geoPositiony
            };
        }

        private GeoPosition BuildRundomGeoPosition()
        {
            return new GeoPosition
            {
                X = new Random().NextDouble(),
                Y = new Random().NextDouble()
            };
        }
    }
}
