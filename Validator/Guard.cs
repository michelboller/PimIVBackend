using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Validator
{
    public class Guard
    {
        private readonly List<GuardResult> _validationResults = new List<GuardResult>();

        private Guard()
        {
        }

        public static void Validate(Action<Guard> action)
        {
            var validator = new Guard();
            action.Invoke(validator);

            if (validator._validationResults.Any())
                throw new GuardException(validator._validationResults.AsReadOnly());
        }

        public static IEnumerable<GuardResult> IsValidFor(Action<Guard> action)
        {
            var validator = new Guard();
            action.Invoke(validator);

            return validator._validationResults;
        }

        public Guard NotNull(object obj, string name, string message)
        {
            if (obj == null)
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard NotDefault<T>(T obj, string name, string message)
        {
            if (obj.Equals(default(T)))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard NotNullOrEmptyString(string obj, string name, string message)
        {
            if (string.IsNullOrWhiteSpace(obj))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard NotFalse(bool obj, string name, string message)
        {
            if (!obj)
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard IsNotNullAndNotEmpty(IEnumerable<object> list, string name, string message)
        {
            if (list == null || !list.Any())
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        /// <summary>
        /// Used ONLY for numbers
        /// </summary>
        /// <typeparam name="T">Any number type</typeparam>
        /// <param name="obj">Any number, maybe the type will be infered</param>
        /// <param name="name">Name of the parameter that contains the value</param>
        /// <param name="message">Message that will apear on exception</param>
        /// <returns></returns>
        public Guard IsGratterThanZero<T>(T obj, string name, string message) where T : struct, IComparable, IFormattable,
                                                                                            IConvertible, IComparable<T>, IEquatable<T>
        {
            dynamic dynamicObj = obj;

            if (dynamicObj < 0)
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard IsValidEmail(string email, string name, string message)
        {
            if (!new EmailAddressAttribute().IsValid(email))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard PasswordHasNumbers(string password, string name, string message)
        {
            var hasNumber = new Regex(@"[0-9]+");
            if (!hasNumber.IsMatch(password))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard PasswordHasUpper(string password, string name, string message)
        {
            var hasUpperChar = new Regex(@"[A-Z]+");
            if (!hasUpperChar.IsMatch(password))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }
        public Guard PasswordHasMiniMaxCharac(string password, string name, string message)
        {
            var hasMiniMaxChars = new Regex(@".{8,64}");
            if (!hasMiniMaxChars.IsMatch(password))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }
        public Guard PasswordHasLowerCharac(string password, string name, string message)
        {
            var hasLowerChar = new Regex(@"[a-z]+");
            if (!hasLowerChar.IsMatch(password))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }
        public Guard PasswordHasSymblos(string password, string name, string message)
        {
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            if (!hasSymbols.IsMatch(password))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard HasSpaces(string obj, string name, string message)
        {
            if (obj.Contains(" "))
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard IsXGratterThanY<T1, T2>(T1 obj1, T2 obj2, string name, string message) where T1 : struct, IComparable, IFormattable,
                                                                                            IConvertible, IComparable<T1>, IEquatable<T1>
                                                                                                 where T2 : struct, IComparable, IFormattable,
                                                                                            IConvertible, IComparable<T2>, IEquatable<T2>
        {
            dynamic dynamicObj1 = obj1;
            dynamic dynamicObj2 = obj2;

            if (dynamicObj1 > dynamicObj2)
                _validationResults.Add(new GuardResult(name, message));

            return this;
        }

        public Guard IsDifferentOfX(object obj1, object obj2, string name, string message)
        {
            if (obj1 != obj2)
            {
                _validationResults.Add(new GuardResult(name, message));
            }

            return this;
        }
    }
}
