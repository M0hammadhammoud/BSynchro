﻿using BSynchro.RJP.Accounts.Application.Models.DTOs;

namespace BSynchro.RJP.Accounts.WebAPI.Models.Responses.Account
{
    public class GetAllCustomersResponse : BaseResponse
    {
        public List<CustomerDTO> Customers { get; set; } = new();
    }
}
