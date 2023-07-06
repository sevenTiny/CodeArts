using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics;

namespace Demo.FluentValidation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Customer customer = new Customer();
            CustomerValidator validator = new CustomerValidator();

            ValidationResult results = validator.Validate(customer);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Trace.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }
        }
    }

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Surname).NotNull();
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public decimal Discount { get; set; }
        public string Address { get; set; }
    }
}