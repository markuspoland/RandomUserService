using RandomUserService.Domain.ValueObjects;
using RandomUserService.Domain.ValueObjects.SubValueObjects;
using System;

namespace RandomUserService.Domain.Entities
{
    public class User
    {
        public User(
            Guid id,
            string gender,
            Name name,
            Location location,
            string email,
            DateOfBirth dateOfBirth,
            Registered registered,
            string phone,
            string cell,
            Id externalId,
            Picture picture,
            string nat,
            DateTime createdAt)
        {
            Id = id;
            Gender = gender;
            Name = name;
            Location = location;
            Email = email;
            DateOfBirth = dateOfBirth;
            Registered = registered;
            Phone = phone;
            Cell = cell;
            ExternalId = externalId;
            Picture = picture;
            Nat = nat;
            CreatedAt = createdAt;
        }

        protected User() { }

        public Guid Id { get; set; }
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public DateOfBirth DateOfBirth { get; set; }
        public Registered Registered { get; set; }
        public string Phone { get; set; }
        public string Cell {  get; set; }
        public Id ExternalId { get; set; }
        public Picture Picture { get; set; }
        public string Nat {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
