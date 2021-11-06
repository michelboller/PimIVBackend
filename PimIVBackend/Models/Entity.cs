using PimIVBackend.Models.Base;
using Validator;

namespace PimIVBackend.Models
{
    public abstract class Entity : ModelBase
    {
        protected Entity() { }
        public Entity(string name, string address, string cEP, string phone, string document, EntityDocType docType, bool act)
        {
            Guard.Validate(validator =>
                validator
                    .IsXGratterThanY(docType.GetHashCode(), 3, nameof(docType), $"{nameof(docType)} possui um valor maior que o permitido (3)")
                    .IsXGratterThanY(1, docType.GetHashCode(), nameof(docType), $"{nameof(docType)} possui um valor menor que o permitido (1)")
                    .NotNullOrEmptyString(name, nameof(name), $"{nameof(name)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(address, nameof(address), $"{nameof(address)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(cEP, nameof(cEP), $"{nameof(cEP)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(phone, nameof(phone), $"{nameof(phone)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(document, nameof(document), $"{nameof(document)} não possui valor ou é uma string em branco")
                    );

            Name = name;
            Address = address;
            CEP = cEP;
            Phone = phone;
            Document = document;
            DocType = docType;
            Act = act;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string CEP { get; private set; }
        public string Phone { get; private set; }
        public string Document { get; private set; }
        public EntityDocType DocType { get; private set; }
        public bool Act { get; private set; }
        public enum EntityDocType
        {
            RG = 1,
            CPF = 2,
            CNPJ = 3
        }

        public void ChangeName(string name)
        {
            Guard.Validate(validator =>
                validator
                    .NotNullOrEmptyString(name, nameof(name), $"{nameof(name)} não possui valor ou é uma string em branco"));

            Name = name;
        }

        public void ChangeAddress(string address)
        {
            Guard.Validate(validator =>
                validator
                    .NotNullOrEmptyString(address, nameof(address), $"{nameof(address)} não possui valor ou é uma string em branco"));

            Address = address;
        }

        public void ChangeCEP(string cep)
        {
            Guard.Validate(validator =>
                validator
                    .NotNullOrEmptyString(cep, nameof(cep), $"{nameof(cep)} não possui valor ou é uma string em branco"));

            CEP = cep;
        }

        public void ChangePhone(string phone)
        {
            Guard.Validate(validator =>
                validator
                    .NotNullOrEmptyString(phone, nameof(phone), $"{nameof(phone)} não possiu um valor ou é uma string em branco"));

            Phone = phone;
        }

        public void ChangeDocument(string document)
        {
            Guard.Validate(validator =>
                validator
                    .NotNullOrEmptyString(document, nameof(document), $"{nameof(document)} não possui um valor ou é uma string em branco"));

            Document = document;
        }

        public void ChangeAct(bool act)
        {
            Act = act;
        }

        public void ChangeDocType(EntityDocType docType)
        {
            if (this is EntityCompany && docType != EntityDocType.CNPJ)
                throw new System.Exception("O tipo do documento informado para a empresa não é válido");

            if (this is EntityGuest && docType == EntityDocType.CNPJ)
                throw new System.Exception("O tipo do documento informado para o/a hóspede é inválido");

            if(this is EntityGuest && docType.GetHashCode() >= 1 && docType.GetHashCode() <= 2)
                DocType = docType;
            else
                throw new System.Exception("O tipo do documento informado para o/a hóspede é inválido");
        }
    }
}
