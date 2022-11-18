using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AddressBL:IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public AddAddress AddAddress(AddAddress addAddress, int userId)
        {
            return this.addressRL.AddAddress(addAddress, userId);
        }
        public AddressModel UpdateAddress(AddressModel addressModel, int userId)
        {
            return this.addressRL.UpdateAddress(addressModel, userId);
        }
        public string DeleteAddress(int addressId, int userId)
        {
            return this.addressRL.DeleteAddress(addressId, userId);
        }
        public List<AddressModel> GetAllAddresses(int userId)
        { return this.addressRL.GetAllAddresses(userId); }
    }
}
