using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingSiteLibrary
{
    public class Profile
    {
        string address;
        string city;
        string state;
        string email;
        string phoneNumber;
        string occupation;
        string gender;
        int age;
        string height;
        decimal weight;
        string profileImg;
        List<string> likes;
        List<string> dislikes;
        string commitment;
        string description;
        bool visible;

        public Profile() { }

        public Profile(string address, string city, string state, string email, string phoneNumber, string occupation, string gender, int age, string height, decimal weight, string profileImg, List<string> likes, List<string> dislikes, string commitment, string description, bool visible)
        {
            this.address = address;
            this.city = city;
            this.state = state;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.occupation = occupation;
            this.gender = gender;
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.profileImg = profileImg;
            this.likes = likes;
            this.dislikes = dislikes;
            this.commitment = commitment;
            this.description = description;
            this.visible = visible;
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Height
        {
            get { return height; }
            set { height = value; }
        }

        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string ProfileImg
        {
            get { return profileImg; }
            set { profileImg = value; }
        }

        public List<string> Likes
        {
            get { return likes; }
            set { likes = value; }
        }

        public List<string> Dislikes
        {
            get { return dislikes; }
            set { dislikes = value; }
        }

        public string Commitment
        {
            get { return commitment; }
            set { commitment = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
    }
}
