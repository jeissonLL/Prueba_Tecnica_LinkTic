using Domain.Entities;
using Domain.Services.Interfaces;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CustomerServices : ICustomerService
    {
        public bool IsCustomerValid(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(
                customer.Name) || 
                customer.Name.Length < 3 || 
                customer.Name.Length > 100)
                return false;

            if (!Regex.IsMatch(customer.Name, @"^[a-zA-Z ]+$"))
                return false;

            if (string.IsNullOrWhiteSpace(
                customer.Email) || !Regex.IsMatch(
                    customer.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return false;

            return true;
        }
    }
}
