using Application.DTOsNS;
using Application.DTOsNS.UserType;
using Application.Repositories;
using Domain.UserType;

namespace Application.Services.UserInformation
{
    public class UserTypeInformationService : IUserInformationService<UserTypeDTO>
    {
        private readonly IUserInformationRepository _userInformationRepository;

        public UserTypeInformationService(IUserInformationRepository userInformationRepository)
        {
            this._userInformationRepository = userInformationRepository;
        }

        public async Task<UserTypeDTO> GetUserInformation(UserInfoRequestDTO userInfoRequest)
        {
            UserTypeDTO userTypeDTO = null;

            if (
                userInfoRequest.userType.Equals(
                    UserTypeEnum.Viewer.ToString(),
                    StringComparison.OrdinalIgnoreCase
                )
            )
            {
                userTypeDTO = await _userInformationRepository.GetViewerInfo(
                    userInfoRequest.userName
                );
                return userTypeDTO;
            }
            else
            {
                if (
                    userInfoRequest.userType.Equals(
                        UserTypeEnum.Customer.ToString(),
                        StringComparison.OrdinalIgnoreCase
                    )
                )
                {
                    userTypeDTO = await _userInformationRepository.GetCustomerInfo(
                        userInfoRequest.userName
                    );
                    return userTypeDTO;
                }
            }
            return userTypeDTO;
        }
    }
}
