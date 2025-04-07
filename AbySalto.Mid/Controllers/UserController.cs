using AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordRequest;
using AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordReset;
using AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordVerifyCode;
using AbySalto.Mid.Application.UserCommands.Users.Commands.Create;
using AbySalto.Mid.Application.UserCommands.Users.Commands.Delete;
using AbySalto.Mid.Application.UserCommands.Users.Commands.Login;
using AbySalto.Mid.Application.UserCommands.Users.Commands.Logout;
using AbySalto.Mid.Application.UserCommands.Users.Commands.RenewToken;
using AbySalto.Mid.Application.UserCommands.Users.Commands.ResendVerificationEmail;
using AbySalto.Mid.Application.UserCommands.Users.Commands.Update;
using AbySalto.Mid.Application.UserCommands.Users.Commands.UpdateMe;
using AbySalto.Mid.Application.UserCommands.Users.Commands.VerifyEmail;
using AbySalto.Mid.Application.UserCommands.Users.Queries.GetAll;
using AbySalto.Mid.Application.UserCommands.Users.Queries.GetAllPaginated;
using AbySalto.Mid.Application.UserCommands.Users.Queries.GetOne;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Paging;
using AbySalto.Mid.Domain.DTOs.Request;
using AbySalto.Mid.Domain.DTOs.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseController
    {
        public UserController(ISender sender) :base(sender)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Me(CancellationToken cancellationToken = default)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var command = new GetOneUserQuery(currentUserId);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetOne(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new GetOneUserQuery(id);
                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new GetAllUsersQuery();
                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] PagedRequest<string> pagedQuery, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new GetAllUsersPaginatedQuery(pagedQuery);
                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPost]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> Create(UserRequestDto user, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new CreateUserCommand(
                    user.Username,
                    user.Name,
                    user.Email,
                    user.Password,
                    user.PasswordConfirm,
                    (int)user.Role,
                    (int)user.Status,
                    null);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto user, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new LoginCommand(
                    user.Email,
                    user.Password);

                var tokenResult = await _sender.Send(command, cancellationToken);

                if(tokenResult.IsFailure)
                {
                    return HandleFailure(tokenResult);
                }

                return Ok(tokenResult.Value);
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = GetCurrentUserId();

                var command = new LogoutCommand(userId);

                var tokenResult = await _sender.Send(command, cancellationToken);

                if(tokenResult.IsFailure)
                {
                    return HandleFailure(tokenResult);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RenewRefreshToken(TokenRequestDto tokenApiDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (tokenApiDto is null)
                {
                    return BadRequest("Invalid request");
                }

                var command = new RenewTokenCommand(
                    tokenApiDto.RefreshToken);

                var tokenResult = await _sender.Send(command, cancellationToken);

                if (tokenResult.IsFailure)
                {
                    return HandleFailure(tokenResult);
                }

                return Ok(tokenResult.Value);
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateMe(UserRequestDto user, CancellationToken cancellationToken = default)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var command = new UpdateMeCommand(
                    currentUserId,
                    user.Username,
                    user.Name,
                    user.Password,
                    user.PasswordConfirm);

                var result = await _sender.Send(command, cancellationToken);
                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(UserRequestDto user, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new UpdateUserCommand(
                    user.Id,
                    user.Username,
                    user.Name,
                    user.Email,
                    user.Password,
                    user.PasswordConfirm,
                    user.Role,
                    user.Status);

                var result = await _sender.Send(command, cancellationToken);
                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
        }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
}

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteUserCommand(id);

                var result = await _sender.Send(command, cancellationToken);
                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordRequest(ForgotPasswordRequestDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request is null)
                {
                    return BadRequest("Invalid request");
                }

                var command = new ForgotPasswordRequestCommand(request.Email);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordVerifyCode(ForgotPasswordVerifyCodeDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request is null)
                {
                    return BadRequest("Invalid request");
                }

                var command = new ForgotPasswordVerifyCodeCommand(request.Email, request.Code);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordReset(ForgotPasswordResetDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request is null)
                {
                    return BadRequest("Invalid request");
                }

                var command = new ForgotPasswordResetCommand(request.Email, request.Password, request.Code);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPost("{id}/{code}")]
        public async Task<IActionResult> VerifyEmail(int id, string code, CancellationToken cancellationToken = default)
        {
            try
            {
                if (code is null)
                {
                    return BadRequest("Invalid request");
                }

                var command = new VerifyEmailCommand(id, code);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ResendVerificationEmail(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new ResendVerificationEmailCommand(id);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return HandleFailure(result);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleFailure(Result.Failure(new Error("BadRequest", ex.Message)));
            }
        }
    }
}
