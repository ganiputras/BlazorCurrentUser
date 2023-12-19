using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using System.Security.Claims;
using WebApp.Ui.Extensions;

namespace WebApp.Ui.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

        public ClaimsPrincipal GetUser()
        {
            return _currentUser;
        }



        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string TenantId { get; set; }
        public string CompanyId { get; set; }
        public bool IsAuthenticated { get; set; }

        public void SetUser(ClaimsPrincipal user)
        {
            if (_currentUser != null && _currentUser != user)
            {
                _currentUser = user;
                UserId = _currentUser.GetUserId();
                UserName = _currentUser.GetDisplayName();
                DisplayName = _currentUser.GetDisplayName();
                TenantId = _currentUser.GetTenantId();
                CompanyId = _currentUser.GetCompanyId();

                if (_currentUser.Identity is { IsAuthenticated: true })
                {
                    IsAuthenticated = _currentUser.Identity.IsAuthenticated;
                }
            }
        }
    }


    internal sealed class BlazorUserCircuitHandler : CircuitHandler, IDisposable
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ICurrentUserService _userService;

        public BlazorUserCircuitHandler(
            AuthenticationStateProvider authenticationStateProvider,
            ICurrentUserService userService)
        {
            this._authenticationStateProvider = authenticationStateProvider;
            this._userService = userService;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit,
            CancellationToken cancellationToken)
        {
            _authenticationStateProvider.AuthenticationStateChanged += AuthenticationChanged;

            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        private void AuthenticationChanged(Task<AuthenticationState> task)
        {
            _ = UpdateAuthentication(task);

            async Task UpdateAuthentication(Task<AuthenticationState> task)
            {
                try
                {
                    var state = await task;
                    _userService.SetUser(state.User);
                }
                catch
                {
                }
            }
        }

        public override async Task OnConnectionUpAsync(Circuit circuit,
            CancellationToken cancellationToken)
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            _userService.SetUser(state.User);
        }

        public void Dispose()
        {
            _authenticationStateProvider.AuthenticationStateChanged -=
                AuthenticationChanged;
        }
    }
}
