using FakeFinancialInstitution.Domain.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FakeFinancialInstitution.Models.RequestModels;
using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Domain.UseCases.Accounts;
using Microsoft.Extensions.Logging;
using FakeFinancialInstitution.Models;
using FakeFinancialInstitution.Models.ResponseModels;
using FakeFinancialInstitution.Models.Constants;

namespace FakeFinancialInstitution.API.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly DomainContext domainContext;
		private readonly ILogger<AccountController> _logger;

		public AccountController(DomainContext domainContext, ILogger<AccountController> logger)
		{
			this.domainContext = domainContext;
			this._logger = logger;
		}

		[HttpGet("{id}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> GetAsync([FromRoute] string id)
		{
			if (id == null) 
			{
				var validation = new ServiceResponse<AccountResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.AccountNumbeNotNull
				};
				return BadRequest(validation);
			}

			var account = new GetOneAccount(domainContext)
			{
				AccountNumber = id
			};

			var results = await account.ExecuteAsync();

			if (results == null)
				return NotFound();

			return Ok(results);
		}

		[HttpPost()]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Create([FromBody] BankAccountForCustomerRequestModel request)
		{
			if (!ModelState.IsValid)
			{
				var validation = new ServiceResponse<AccountResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.InvalidRequestState
				};
				return BadRequest(validation);
			}

			var bankAccount = new CreateAccount(domainContext)
			{
				request = request
			};


			var result = await bankAccount.ExecuteAsync();

			if (!result.IsSuccess)
				return BadRequest(result);
			
			return Ok(result);
		}

		
	}
}
