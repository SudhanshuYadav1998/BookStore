using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public AddAddress AddAddress(AddAddress addAddress, int userId);
        public AddressModel UpdateAddress(AddressModel addressModel, int userId);
        public string DeleteAddress(int addressId, int userId);
        public List<AddressModel> GetAllAddresses(int userId);


    }
}
