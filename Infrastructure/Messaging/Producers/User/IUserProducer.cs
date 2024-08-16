
using SGT.Application.DTOs.Tasks;
using SGT.Application.DTOs.Users;

namespace SGT.Infrastructure.Messaging.Producers.User
{
    public interface IUserProducer
    {
        void CreateAccountMessage(UserResponseDTO userCreated);
        void UpdatedAccountMessage(UserUpdateDTO userUpdateDTO);
        void GetTimeTask(UserResponseDTO userCreated);
    }
}
