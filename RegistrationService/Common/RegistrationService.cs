namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Exceptions;

    public class RegistrationService
    {
        private readonly Dictionary<CustomEvent, HashSet<Person>> _events;

        public RegistrationService()
        {
            this._events = new Dictionary<CustomEvent, HashSet<Person>>();
        }

        public IEnumerable<CustomEvent> GetUpcomingEvents()
        {
            try
            {
                var upcomingEvent = this._events
                    .Keys
                    .Where(customEvent => customEvent.EventDate >= DateTime.Now)
                    .OrderBy(customEvent => customEvent.EventDate)
                    .ToList();

                if (upcomingEvent.Any())
                {
                    return upcomingEvent;
                }

                throw new NoUpcomingEventsException();
            }
            catch (NullReferenceException)
            {
                throw new NoUpcomingEventsException();
            }
        }

        public IEnumerable<Person> GetEventVisitors(CustomEvent customEvent)
        {
            this.ValidateEventExist(customEvent);
            return this._events[customEvent];
        }

        public void CheckIn(Person emp, CustomEvent customEvent)
        {
            if (this._events.ContainsKey(customEvent))
            {
                this.ValidateUserIsAlreadyCheckedIn(customEvent, emp);
                this._events[customEvent].Add(emp);
            }
            else
            {
                this._events.Add(
                    customEvent,
                    new HashSet<Person> { emp });
            }
        }

        public void CheckOut(Person emp, CustomEvent customEvent)
        {
            this.ValidateEventExist(customEvent);
            this.ValidateUserNotChekedId(customEvent, emp);

            this._events[customEvent].Remove(emp);

            if (this.IsEventWithoutPersons(customEvent))
            {
                this._events.Remove(customEvent);
            }
        }

        private void ValidateEventExist(CustomEvent customEvent)
        {
            if (!this._events.ContainsKey(customEvent))
            {
                throw new EventNotExistException();
            }
        }

        private void ValidateUserNotChekedId(CustomEvent customEvent, Person emp)
        {
            if (!this._events[customEvent].Contains(emp))
            {
                throw new UserNotCheckedInException();
            }
        }

        private void ValidateUserIsAlreadyCheckedIn(CustomEvent customEvent, Person emp)
        {
            if (this._events[customEvent].Contains(emp))
            {
                throw new UserIsAlreadyCheckedInException();
            }
        }

        private bool IsEventWithoutPersons(CustomEvent customEvent)
        {
            return !this._events[customEvent].Any();
        }
    }
}
