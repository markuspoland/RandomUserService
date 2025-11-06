using RandomUserService.Domain.Entities;
using RandomUserService.Domain.ValueObjects;
using RandomUserService.Domain.ValueObjects.SubValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomUserService.Tests.Builders
{
    public class UserBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _gender = "male";
        private Name _name = null;
        private Location _location = null;
        private string _email = "test.email@example.com";
        private DateOfBirth _dateOfBirth = null;
        private Registered _registered = null;
        private string _phone = "123123123";
        private string _cell = "2132312";
        private Id _externalId = null;
        private Picture _picture = null;
        private string _nat = "UK";
        private DateTime _createdAt = DateTime.Now;

        public UserBuilder WithName(string first, string last, string title)
        {
            _name = new Name
            {
                First = first,
                Last = last,
                Title = title
            };

            return this;
        }

        public UserBuilder WithLocation(Location location)
        {
            _location = location;

            return this;
        }

        public UserBuilder WithDateOfBirth(DateTime date, int age)
        {
            _dateOfBirth = new DateOfBirth
            {
                Date = date,
                Age = age
            };

            return this;
        }

        public UserBuilder WithExternalId(string name, string value)
        {
            _externalId = new Id
            {
                Name = name,
                Value = value
            };

            return this;
        }

        public UserBuilder WithPicture(string large, string medium, string thumbnail)
        {
            _picture = new Picture
            {
                Large = large,
                Medium = medium,
                Thumbnail = thumbnail
            };

            return this;
        }

        public User Build()
        {
            return new User(
                _id,
                _gender,
                _name,
                _location,
                _email,
                _dateOfBirth,
                _registered,
                _phone,
                _cell,
                _externalId,
                _picture,
                _nat,
                _createdAt);
        }
    }
}
