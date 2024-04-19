using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using DatingSiteLibrary;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace DatingSiteLibrary
{
    public class Dating
    {
        DBConnect objDB = new DBConnect();

        SqlCommand objCommand = new SqlCommand();

        public int validateLogin(string username, string password)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetLogin"; //stored procedure for looking up an account via username and password

            SqlParameter para1 = new SqlParameter("@theUsername", username); //username input parameter
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.VarChar;
            para1.Size = 50;

            SqlParameter para2 = new SqlParameter("@thePassword", password); //password input parameter
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.VarChar;
            para2.Size = 50;

            //SqlParameter outputPara = new SqlParameter("@theCount", DbType.Int32);
            //outputPara.Direction = ParameterDirection.Output;

            SqlParameter returnPara = new SqlParameter("@theCount", DbType.Int32); //return parameter
            returnPara.Direction = ParameterDirection.ReturnValue;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            //objCommand.Parameters.Add(outputPara);
            objCommand.Parameters.Add(returnPara);

            objDB.GetDataSet(objCommand);

            int count;
            count = int.Parse(objCommand.Parameters["@theCount"].Value.ToString());

            return count;
        }

        public DataSet getUserInfo(string username, string password)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getUserInfo"; //stored procedure for looking up an account via username and password

            SqlParameter para1 = new SqlParameter("@loginUsername", username); //username input parameter
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.VarChar;
            para1.Size = 50;

            SqlParameter para2 = new SqlParameter("@loginPassword", password); //password input parameter
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.VarChar;
            para2.Size = 50;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getProfileInfo(int accountID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getDatingProfile"; //stored procedure for getting all dating profile info with accountID

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }
        public void createAccount(string username, string password, string firstname, string lastname, string email)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "createAccount"; //stored procedure for creating accounts (Username, Password, FirstName, LastName, Email)

            SqlParameter para1 = new SqlParameter("@Username", username);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.VarChar;
            para1.Size = 50;

            SqlParameter para2 = new SqlParameter("@Password", password);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.VarChar;
            para2.Size = 50;

            SqlParameter para3 = new SqlParameter("@FirstName", firstname);
            para3.Direction = ParameterDirection.Input;
            para3.SqlDbType = SqlDbType.VarChar;
            para3.Size = 50;

            SqlParameter para4 = new SqlParameter("@LastName", lastname);
            para4.Direction = ParameterDirection.Input;
            para4.SqlDbType = SqlDbType.VarChar;
            para4.Size = 50;

            SqlParameter para5 = new SqlParameter("@Email", email);
            para5.Direction = ParameterDirection.Input;
            para5.SqlDbType = SqlDbType.VarChar;
            para5.Size = 50;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objCommand.Parameters.Add(para3);
            objCommand.Parameters.Add(para4);
            objCommand.Parameters.Add(para5);

            objDB.GetDataSet(objCommand);
        }

        public void createProfile(int accountID, string email)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "createProfile"; //stored procedure for creating an empty profile keyed with accountID

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@email", email);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.VarChar;
            para2.Size = 50;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objDB.GetDataSet(objCommand);
        }

        public void updateProfile(int accountID, string address, string state, string city, string email, string gender, int age, string height, decimal weight, string profileImg, string commitment, string desc, string phonenumber, string occupation, bool visible, string name)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "updateProfile"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;


            SqlParameter para2 = new SqlParameter("@address", address);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.VarChar;
            para2.Size = 50;

            SqlParameter para3 = new SqlParameter("@state", state);
            para3.Direction = ParameterDirection.Input;
            para3.SqlDbType = SqlDbType.VarChar;
            para3.Size = 50;

            SqlParameter para4 = new SqlParameter("@city", city);
            para4.Direction = ParameterDirection.Input;
            para4.SqlDbType = SqlDbType.VarChar;
            para4.Size = 50;

            SqlParameter para5 = new SqlParameter("@email", email);
            para5.Direction = ParameterDirection.Input;
            para5.SqlDbType = SqlDbType.VarChar;
            para5.Size = 50;

            SqlParameter para6 = new SqlParameter("@gender", gender);
            para6.Direction = ParameterDirection.Input;
            para6.SqlDbType = SqlDbType.VarChar;
            para6.Size = 50;

            SqlParameter para7 = new SqlParameter("@age", age);
            para7.Direction = ParameterDirection.Input;
            para7.SqlDbType = SqlDbType.Int;

            SqlParameter para8 = new SqlParameter("@height", height);
            para8.Direction = ParameterDirection.Input;
            para8.SqlDbType = SqlDbType.VarChar;
            para8.Size = 50;

            SqlParameter para9 = new SqlParameter("@weight", weight);
            para9.Direction = ParameterDirection.Input;
            para9.SqlDbType = SqlDbType.Decimal;

            SqlParameter para10 = new SqlParameter("@commitment", commitment);
            para10.Direction = ParameterDirection.Input;
            para10.SqlDbType = SqlDbType.VarChar;
            para10.Size = 50;

            SqlParameter para11 = new SqlParameter("@desc", desc);
            para11.Direction = ParameterDirection.Input;
            para11.SqlDbType = SqlDbType.VarChar;
            para11.Size = 50;

            SqlParameter para12 = new SqlParameter("@phoneNumber", phonenumber);
            para12.Direction = ParameterDirection.Input;
            para12.SqlDbType = SqlDbType.VarChar;
            para12.Size = 50;

            SqlParameter para13 = new SqlParameter("@occupation", occupation);
            para13.Direction = ParameterDirection.Input;
            para13.SqlDbType = SqlDbType.VarChar;
            para13.Size = 50;

            SqlParameter para14 = new SqlParameter("@visible", visible);
            para14.Direction = ParameterDirection.Input;
            para14.SqlDbType = SqlDbType.Bit;

            SqlParameter para15 = new SqlParameter("@profileImg", profileImg);
            para15.Direction = ParameterDirection.Input;
            para15.SqlDbType = SqlDbType.VarChar;
            para15.Size = 50;

            SqlParameter para16 = new SqlParameter("@Name", name);
            para16.Direction = ParameterDirection.Input;
            para16.SqlDbType = SqlDbType.VarChar;
            para16.Size = 50;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objCommand.Parameters.Add(para3);
            objCommand.Parameters.Add(para4);
            objCommand.Parameters.Add(para5);
            objCommand.Parameters.Add(para6);
            objCommand.Parameters.Add(para7);
            objCommand.Parameters.Add(para8);
            objCommand.Parameters.Add(para9);
            objCommand.Parameters.Add(para10);
            objCommand.Parameters.Add(para11);
            objCommand.Parameters.Add(para12);
            objCommand.Parameters.Add(para13);
            objCommand.Parameters.Add(para14);
            objCommand.Parameters.Add(para15);
            objCommand.Parameters.Add(para16);

            objDB.GetDataSet(objCommand);

        }

        public void createInterests(int accountID) //creates a record tied to an account with their interests, will be empty at first so no interests until user updates their profile
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "createInterests"; //stored procedure for creating an empty interests record keyed with accountID

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objDB.GetDataSet(objCommand);
        }

        public DataSet getInterests(int accountID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getInterests"; //stored procedure for looking up an account's interests table 

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public void updateInterests(int accountID, bool movies, bool tv, bool anime, bool manga, bool books, bool games, bool sports, bool gym, bool cooking, bool martial, bool art, bool hiking, bool partying, bool music, bool dancing)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "updateInterests"; //stored procedure for updating an account's interests

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@movies", movies);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Bit;

            SqlParameter para3 = new SqlParameter("@tv", tv);
            para3.Direction = ParameterDirection.Input;
            para3.SqlDbType = SqlDbType.Bit;

            SqlParameter para4 = new SqlParameter("@anime", anime);
            para4.Direction = ParameterDirection.Input;
            para4.SqlDbType = SqlDbType.Bit;

            SqlParameter para5 = new SqlParameter("@manga", manga);
            para5.Direction = ParameterDirection.Input;
            para5.SqlDbType = SqlDbType.Bit;

            SqlParameter para6 = new SqlParameter("@books", books);
            para6.Direction = ParameterDirection.Input;
            para6.SqlDbType = SqlDbType.Bit;

            SqlParameter para7 = new SqlParameter("@videoGames", games);
            para7.Direction = ParameterDirection.Input;
            para7.SqlDbType = SqlDbType.Bit;

            SqlParameter para8 = new SqlParameter("@sports", sports);
            para8.Direction = ParameterDirection.Input;
            para8.SqlDbType = SqlDbType.Bit;

            SqlParameter para9 = new SqlParameter("@gym", gym);
            para9.Direction = ParameterDirection.Input;
            para9.SqlDbType = SqlDbType.Bit;

            SqlParameter para10 = new SqlParameter("@cooking", cooking);
            para10.Direction = ParameterDirection.Input;
            para10.SqlDbType = SqlDbType.Bit;

            SqlParameter para11 = new SqlParameter("@martialarts", martial);
            para11.Direction = ParameterDirection.Input;
            para11.SqlDbType = SqlDbType.Bit;

            SqlParameter para12 = new SqlParameter("@art", art);
            para12.Direction = ParameterDirection.Input;
            para12.SqlDbType = SqlDbType.Bit;

            SqlParameter para13 = new SqlParameter("@hiking", hiking);
            para13.Direction = ParameterDirection.Input;
            para13.SqlDbType = SqlDbType.Bit;

            SqlParameter para14 = new SqlParameter("@partying", partying);
            para14.Direction = ParameterDirection.Input;
            para14.SqlDbType = SqlDbType.Bit;

            SqlParameter para15 = new SqlParameter("@music", music);
            para15.Direction = ParameterDirection.Input;
            para15.SqlDbType = SqlDbType.Bit;

            SqlParameter para16 = new SqlParameter("@dancing", dancing);
            para16.Direction = ParameterDirection.Input;
            para16.SqlDbType = SqlDbType.Bit;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objCommand.Parameters.Add(para3);
            objCommand.Parameters.Add(para4);
            objCommand.Parameters.Add(para5);
            objCommand.Parameters.Add(para6);
            objCommand.Parameters.Add(para7);
            objCommand.Parameters.Add(para8);
            objCommand.Parameters.Add(para9);
            objCommand.Parameters.Add(para10);
            objCommand.Parameters.Add(para11);
            objCommand.Parameters.Add(para12);
            objCommand.Parameters.Add(para13);
            objCommand.Parameters.Add(para14);
            objCommand.Parameters.Add(para15);
            objCommand.Parameters.Add(para16);

            objDB.GetDataSet(objCommand);
        }



        public DataSet getAllOtherProfiles(int accountID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getAllOtherProfiles"; //stored procedure for updating an account's interests

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public int validateUniqueUsername(string username)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "checkUsername"; //stored procedure for looking up an account via username and password

            SqlParameter para1 = new SqlParameter("@username", username); //username input parameter
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.VarChar;
            para1.Size = 50;

            SqlParameter returnPara = new SqlParameter("@theCount", DbType.Int32); //return parameter
            returnPara.Direction = ParameterDirection.ReturnValue;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(returnPara);

            objDB.GetDataSet(objCommand);

            int count;
            count = int.Parse(objCommand.Parameters["@theCount"].Value.ToString());

            return count;
        }

        public DataSet getProfilesByAge(int accountID, int ageMin, int ageMax)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getProfilesByAge"; //stored procedure for looking up profiles with age filter

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@ageMin", ageMin);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            SqlParameter para3 = new SqlParameter("@ageMax", ageMax);
            para3.Direction = ParameterDirection.Input;
            para3.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objCommand.Parameters.Add(para3);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getProfilesByGender(int accountID, string gender)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getProfilesByGender"; //stored procedure for looking up profiles with gender filter

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@gender", gender);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.VarChar;
            para2.Size = 50;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getProfilesByCommitment(int accountID, string commitment)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getProfilesByCommitment"; //stored procedure for looking up profiles with commitment filter

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@commitment", commitment);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.VarChar;
            para2.Size = 50;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getProfilesByState(int accountID, string state)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getProfilesByState"; //stored procedure for looking up profiles with commitment filter

            SqlParameter para1 = new SqlParameter("@accountID", accountID);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@state", state);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.VarChar;
            para2.Size = 50;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            return objDB.GetDataSet(objCommand);
        }

        public void createLikeRequest(int initiator, int recipient, bool status)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "createLikeRequest"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@Initiator", initiator);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@Recipient", recipient);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            SqlParameter para3 = new SqlParameter("@Status", status);
            para3.Direction = ParameterDirection.Input;
            para3.SqlDbType = SqlDbType.Bit;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objCommand.Parameters.Add(para3);

            objDB.GetDataSet(objCommand);
        }

        public DataSet getSentLikes(int initiator)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getSentLikes"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@initiator", initiator);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            return objDB.GetDataSet(objCommand);
        }

        public DataSet getReceivedLikes(int recipient)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getReceivedLikes"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@recipient", recipient);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            return objDB.GetDataSet(objCommand);
        }

        public void deleteLikes(int initiator, int recipient)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "deleteLike"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@initiator", initiator);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@recipient", recipient);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            objDB.GetDataSet(objCommand);
        }

        public void createMatch(int initiator, int recipient)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "updateMatch"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@initiator", initiator);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@recipient", recipient);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            objDB.GetDataSet(objCommand);
        }

        public int checkMatch(int user1, int user2)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "checkIfMatched"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@user1", user1);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@user2", user2);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            SqlParameter returnPara = new SqlParameter("@theCount", DbType.Int32); //return parameter
            returnPara.Direction = ParameterDirection.ReturnValue;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objCommand.Parameters.Add(returnPara);

            objDB.GetDataSet(objCommand);

            int count;
            count = int.Parse(objCommand.Parameters["@theCount"].Value.ToString());

            return count;
        }

        public DataSet getMatches(int accountID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getMatches"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@accountID", accountID);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public void unMatch(int userID, int otherUserID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "deleteMatch"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@userID", userID);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@otherUserID", otherUserID);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            objDB.GetDataSet(objCommand);
        }

        public void dateRequest(int user1, int user2)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "createDate"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@user1", user1);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@user2", user2);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            objDB.GetDataSet(objCommand);
        }

        public DataSet getDates(int userID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getDates"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@userID", userID);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public void unMatchOnDate(int user1, int user2)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "deleteMatchOnDate"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@user1", user1);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@user2", user2);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            objDB.GetDataSet(objCommand);
        }

        public DataSet getReceivedDates(int userID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getReceivedDates"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@userID", userID);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public void deleteDate(int userID, int otherUserID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "deleteDate"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@userID", userID);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@otherUserID", otherUserID);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            objDB.GetDataSet(objCommand);
        }

        public void createDatePlans(int userID, int otherUser)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "createDatePlans"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@person1", userID);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@person2", otherUser);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            objDB.GetDataSet(objCommand);
        }

        public void updateDates(int userID, int otherUser)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "updateDates"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@user1", userID);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@user2", otherUser);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            objDB.GetDataSet(objCommand);
        }

        public DataSet getDatePlans(int userID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getDatePlans"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@userID", userID);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getDateDetails(int user1, int user2)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getDateDetails"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@user1", user1);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@user2", user2);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;


            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            return objDB.GetDataSet(objCommand);
        }

        public void updateDateDetails(int user1, int user2, string date, string time, string desc)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "updateDateDetails"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@user1", user1);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@user2", user2);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            SqlParameter para3 = new SqlParameter("@date", date);

            para3.Direction = ParameterDirection.Input;
            para3.SqlDbType = SqlDbType.VarChar;
            para3.Size = 50;

            SqlParameter para4 = new SqlParameter("@time", time);

            para4.Direction = ParameterDirection.Input;
            para4.SqlDbType = SqlDbType.VarChar;
            para4.Size = 50;

            SqlParameter para5 = new SqlParameter("@desc", desc);

            para5.Direction = ParameterDirection.Input;
            para5.SqlDbType = SqlDbType.VarChar;
            para5.Size = 50;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objCommand.Parameters.Add(para3);
            objCommand.Parameters.Add(para4);
            objCommand.Parameters.Add(para5);

            objDB.GetDataSet(objCommand);

        }

        public int getValidDate(int user1, int user2)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "checkForDate"; //stored procedure for updating a Profile

            SqlParameter para1 = new SqlParameter("@user1", user1);

            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@user2", user2);

            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            SqlParameter returnPara = new SqlParameter("@theCount", DbType.Int32); //return parameter
            returnPara.Direction = ParameterDirection.ReturnValue;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);
            objCommand.Parameters.Add(returnPara);

            objDB.GetDataSet(objCommand);

            int count;
            count = int.Parse(objCommand.Parameters["@theCount"].Value.ToString());

            return count;
        }
        //Added new dataset functions for search functionality change, kept the old ones incase something went wrong.
        public DataSet getProfilesByAge(int ageMin, int ageMax)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getProfilesByAge"; // Stored procedure for looking up profiles with age filter

            SqlParameter para1 = new SqlParameter("@ageMin", ageMin);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.Int;

            SqlParameter para2 = new SqlParameter("@ageMax", ageMax);
            para2.Direction = ParameterDirection.Input;
            para2.SqlDbType = SqlDbType.Int;

            objCommand.Parameters.Add(para1);
            objCommand.Parameters.Add(para2);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getProfilesByGender(string gender)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getProfilesByGender"; // Stored procedure for looking up profiles with gender filter

            SqlParameter para1 = new SqlParameter("@gender", gender);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.VarChar;
            para1.Size = 50;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getProfilesByCommitment(string commitment)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getProfilesByCommitment"; // Stored procedure for looking up profiles with commitment filter

            SqlParameter para1 = new SqlParameter("@commitment", commitment);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.VarChar;
            para1.Size = 50;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getProfilesByState(string state)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getProfilesByState"; // Stored procedure for looking up profiles with state filter

            SqlParameter para1 = new SqlParameter("@state", state);
            para1.Direction = ParameterDirection.Input;
            para1.SqlDbType = SqlDbType.VarChar;
            para1.Size = 50;

            objCommand.Parameters.Add(para1);

            return objDB.GetDataSet(objCommand);
        }

        public DataSet getAllOtherProfiles()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getAllOtherProfiles"; // Stored procedure for retrieving all other profiles

            return objDB.GetDataSet(objCommand);
        }


        public DataSet SearchProfiles(int? txtAgeMin, int? txtAgeMax, string ddlGender, string ddlState, string ddlCommitment)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "searchProfiles";


            objCommand.Parameters.AddWithValue("@txtAgeMin", txtAgeMin ?? (object)DBNull.Value);
            objCommand.Parameters.AddWithValue("@txtAgeMax", txtAgeMax ?? (object)DBNull.Value);
            objCommand.Parameters.AddWithValue("@ddlGender", string.IsNullOrEmpty(ddlGender) ? (object)DBNull.Value : ddlGender);
            objCommand.Parameters.AddWithValue("@ddlState", string.IsNullOrEmpty(ddlState) ? (object)DBNull.Value : ddlState);
            objCommand.Parameters.AddWithValue("@ddlCommitment", string.IsNullOrEmpty(ddlCommitment) ? (object)DBNull.Value : ddlCommitment);

            return objDB.GetDataSet(objCommand);
        }



    }
}
