using FakeFinancialInstitution.Domain.Contexts;
using FakeFinancialInstitution.Domain.UseCases.Transactions;
using FakeFinancialInstitution.Models;
using FakeFinancialInstitution.Models.Constants;
using FakeFinancialInstitution.Models.RequestModels;
using FakeFinancialInstitution.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.API.Controllers
{
	[Route("api/[controller]")]
	public class TransactionController : Controller
	{
		private readonly DomainContext domainContext;
		private readonly ILogger<TransactionController> _logger;

		public TransactionController(DomainContext domainContext, ILogger<TransactionController> logger)
		{
			this.domainContext = domainContext;
			this._logger = logger;
		}

		[HttpPost(nameof(Transfer))]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Transfer([FromBody] TransferRequestModel request)
		{
			if (!ModelState.IsValid)
			{
				var validation = new ServiceResponse<TransferResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.InvalidRequestState
				};
				return BadRequest(validation);
			}

			var transfer = new TranferFunds(domainContext)
			{
				request = request
			};

			var result = await transfer.ExecuteAsync();

			if (!result.IsSuccess)
				return BadRequest(result);

			return Ok(result);


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

			var account = new GetTransactions(domainContext)
			{
				AccountNumber = id
			};

			var results = await account.ExecuteAsync();

			if (results == null)
				return NotFound();

			return Ok(results);
		}
	}


}
