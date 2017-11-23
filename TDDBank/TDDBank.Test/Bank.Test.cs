using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TDDBank.Test
{
    [TestClass]
    public class BankManagerTest
    {
        BankManager manager;

        // Transaction Tests
        [TestMethod]
        public void Transaction_List_Grows_After_Every_Transaction()
        {
            // Arrange
            manager = new BankManager();
            var expected = 2;

            // Act            
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer) { AccountNumber = 1, Balance = 1500.0 });
            manager.CreateBankAccount(new BankAccount(customer) { AccountNumber = 2, Balance = 0.0 });
            manager.Transaction(1, 2, expected);
            manager.Transaction(2, 1, expected);
            var actual = manager.Transactions.Count;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Transaction_Moving_1000_Between_Accounts()
        {
            // Arrange
            manager = new BankManager();
            var expected = 1000.0;

            // Act            
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer) { AccountNumber = 1, Balance = 1500.0 });
            manager.CreateBankAccount(new BankAccount(customer) { AccountNumber = 2, Balance = 0.0 });
            manager.Transaction(1, 2, expected);
            var actual = manager.GetBalance(2);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Transaction_With_Insufficient_Balance()
        {
            // Arrange
            manager = new BankManager();

            // Act            
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer));
            manager.CreateBankAccount(new BankAccount(customer));
            manager.Transaction(1, 2, 1000.0);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Transaction_With_Null_Amount()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer));
            manager.CreateBankAccount(new BankAccount(customer));
            manager.Transaction(1, 2, null);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Transaction_To_Null_Account()
        {
            // Arrange
            manager = new BankManager();

            // Act 
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer));
            manager.Transaction(1, null, null);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Transaction_From_Null_Account()
        {
            // Arrange
            manager = new BankManager();

            // Act
            manager.Transaction(null, null, null);
        }

        // Basic Transaction Tests        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetAllTransactionsForAccount_With_Non_Existent_Id_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var result = manager.GetAllTransactionsForAccount(555);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetAllTransactionsForAccount_With_Id_Null_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var result = manager.GetAllTransactionsForAccount(null);
        }
        [TestMethod]
        public void GetAllTransactionsForAccount_Returns_List_With_Transactions()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var customer = manager.GetCustomer(1);
            var acc = new BankAccount(customer);
            manager.CreateBankAccount(acc);
            var result = manager.GetAllTransactionsForAccount(acc.AccountNumber);

            // Assert
            Assert.IsTrue(result is List<Transaction>);
        }
        [TestMethod]
        public void Transactions_List_Is_List_Of_Transactions()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var result = manager.Transactions;

            // Assert
            Assert.IsTrue(result is List<Transaction>);
        }
        [TestMethod]
        public void Transactions_List_Not_Null()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var result = manager.Transactions;

            // Assert
            Assert.IsNotNull(result);
        }

        // Single Account Balance Tests        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTotalBalance_With_Non_Existent_Owner_Throws_NullreferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var result = manager.GetTotalBalance(555);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTotalBalance_With_Id_Null_Throws_NullreferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var result = manager.GetTotalBalance(null);
        }
        [TestMethod]
        public void GetTotalBalance_Returns_Total_Balance_For_All_Accounts_Belonging_To_Customer()
        {
            // Arrange
            manager = new BankManager();
            var expected = 2000.0;

            // Act
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer) { Balance = 1000.0 });
            manager.CreateBankAccount(new BankAccount(customer) { Balance = 1000.0 });
            var result = manager.GetTotalBalance(customer.CustomerID);

            // Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void BankAccount_Balance_START_0_IN_1000_OUT_1000_END_0()
        {
            // Arrange
            manager = new BankManager();
            var expected = 0.0;

            // Act
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer) { AccountNumber = 1, Balance = 0.0 });
            manager.InsertToAccount(1, 1000.0);
            manager.WithdrawFromAccount(1, 1000.0);
            var result = manager.Accounts.FirstOrDefault(a => a.AccountNumber == 1);

            // Assert
            Assert.AreEqual(expected, result.Balance);
        }
        [TestMethod]
        public void BankAccount_Balance_START_0_IN_1000_END_1000()
        {
            // Arrange
            manager = new BankManager();
            var expected = 1000.0;

            // Act
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer) { AccountNumber = 1, Balance = 0.0 });
            manager.InsertToAccount(1, expected);
            var result = manager.Accounts[0];

            // Assert
            Assert.AreEqual(expected, result.Balance);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void BankAccount_Balance_Id_Not_In_List_Returns_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Act
            manager.GetBalance(555);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void BankAccount_Balance_Null_Id_Returns_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Act
            manager.GetBalance(null);
        }

        // Basic Account Tests         
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetAllAccounts_With_Non_Existent_Id_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            var result = manager.GetAllAccounts(555);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetAllAccounts_With_Id_Null_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            var result = manager.GetAllAccounts(null);
        }
        [TestMethod]
        public void GetAllAccounts_Returns_All_Accounts_For_Customer()
        {
            // Arrange
            manager = new BankManager();
            var expected = 2;

            // Assign
            var customer = manager.GetCustomer(1);
            manager.CreateBankAccount(new BankAccount(customer));
            manager.CreateBankAccount(new BankAccount(customer));
            var result = manager.GetAllAccounts(customer.CustomerID).Count();

            // Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetAllAccounts_Returns_List_With_BankAccounts()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            var customer = manager.GetCustomer(1);
            var result = manager.GetAllAccounts(customer.CustomerID);

            // Assert
            Assert.IsTrue(result is List<BankAccount>);
        }
        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void Unlocking_Already_Unlocked_BankAccount_Throws_Exception()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            var customer = manager.GetCustomer(1);
            var acc = new BankAccount(customer) { Locked = true };
            manager.CreateBankAccount(acc);
            manager.LockAccount(acc.AccountNumber);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void Unlocking_BankAccount_With_Non_Existent_Id_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            manager.UnlockAccount(555);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void Unlocking_BankAccount_With_Id_Null_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            manager.UnlockAccount(null);
        }
        [TestMethod]
        public void Unocking_BankAccount_Sets_Locked_Value_To_False()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            var customer = manager.GetCustomer(1);
            var acc = new BankAccount(customer) { Locked = true };
            manager.CreateBankAccount(acc);
            manager.UnlockAccount(acc.AccountNumber);
            var result = acc.Locked;

            // Assert
            Assert.IsFalse(result);
        }      
        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void Locking_Already_Locked_BankAccount_Throws_Exception()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            var customer = manager.GetCustomer(1);
            var acc = new BankAccount(customer) { Locked = true };
            manager.CreateBankAccount(acc);
            manager.LockAccount(acc.AccountNumber);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void Locking_BankAccount_With_Non_Existent_Id_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            manager.LockAccount(555);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void Locking_BankAccount_With_Id_Null_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            manager.LockAccount(null);
        }
        [TestMethod]
        public void Locking_BankAccount_Sets_Locked_Value_To_True()
        {
            // Arrange
            manager = new BankManager();

            // Assign
            var customer = manager.GetCustomer(1);
            var acc = new BankAccount(customer);
            manager.CreateBankAccount(acc);
            manager.LockAccount(acc.AccountNumber);
            var result = acc.Locked;

            // Assert
            Assert.IsTrue(result);
        }
        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void Create_New_Account_With_Existing_Id_Throws_Exeption()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var customer = new Customer();
            manager.CreateCustomer(customer);
            manager.CreateBankAccount(new BankAccount(customer) { AccountNumber = 1 });
            manager.CreateBankAccount(new BankAccount(customer) { AccountNumber = 1 });
        }
        [TestMethod]
        public void Create_New_BankAccount_Increases_List_Count()
        {
            // Arrange
            manager = new BankManager();
            var expected = 1;

            // Act
            var customer = new Customer();
            manager.CreateCustomer(customer);
            manager.CreateBankAccount(new BankAccount(customer));
            var result = manager.Accounts;       

            // Assert
            Assert.AreEqual(expected, result.Count);

        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void Create_Null_BankAccount_Throws_NullReferenceExeption()
        {
            // Arrange
            manager = new BankManager();

            // Act
            manager.CreateBankAccount(null);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void Create_New_BankAccount_With_Non_Existent_OwnerId_Throws_NullReferenceExeption()
        {
            // Arrange
            manager = new BankManager();

            // Act
            manager.CreateBankAccount(new BankAccount());
        }
        [TestMethod]
        public void BankAccounts_List_Of_BankAccounts()
        {
            manager = new BankManager();

            // Act
            var result = manager.Accounts;

            // Assert
            Assert.IsTrue(result is List<BankAccount>);
        }
        [TestMethod]
        public void BankAccounts_List_Not_Null()
        {
            manager = new BankManager();
            
            // Act
            var result = manager.Accounts;

            // Assert
            Assert.IsNotNull(result);
        }

        // Basic Customer Tests
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void GetCustomer_With_Id_Null_Throws_NullReferenceException()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var result = manager.GetCustomer(null);
        }
        [TestMethod]
        public void GetCustomer_Returns_Customer()
        {
            // Arrange
            manager = new BankManager();

            // Act
            var result = manager.GetCustomer(1);

            // Assert
            Assert.IsTrue(result is Customer);

        }
        [TestMethod]
        public void Customers_List_Of_Customers()
        {
            manager = new BankManager();

            // Act
            var result = manager.Customers;

            // Assert
            Assert.IsTrue(result is List<Customer>);
        }
        [TestMethod]
        public void Customers_List_Not_Null()
        {
            manager = new BankManager();

            // Act
            var result = manager.Customers;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
