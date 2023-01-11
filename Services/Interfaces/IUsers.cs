using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using L4U_API_SOAP.SoapModels;

namespace L4U_API_SOAP.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor"
    // menu to change the interface name "IUsers" in both code and config file together.
    [ServiceContract]
    public interface IUsers
    {

        /// <summary>
        /// This Method adds a new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddNewUser(User user);


        /// <summary>
        /// Lists All Users from the Database
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<User> GetAllUsers();


        /// <summary>
        /// Deletes a User from the database
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        bool DeleteUser(User user);



        #region To implement
        /*
        /// <summary>
        /// 
        /// </summary>
        [OperationContract]
        void DoWork();
        */
        #endregion


    }
}
